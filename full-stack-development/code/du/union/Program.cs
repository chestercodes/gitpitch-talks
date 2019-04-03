using System;
using System.Collections.Generic;

namespace union
{
    public class Email 
    {
        public string Value { get; }
        public Email (string value)
        {
            // plus validation
            Value = value;
        }
    }

    public class Phone 
    {
        public string Value { get; }
        public Phone(string value)
        {
            // plus validation
            Value = value;
        }
    }

    public abstract class Contact
    {
        public abstract T Match<T>(
            Func<union.Email, T> f, 
            Func<union.Phone, T> g, 
            Func<Tuple<union.Email, union.Phone>, T> h);
        private Contact() { } 

        public sealed class Email : Contact
        {
            public readonly union.Email Item;
            public Email(union.Email item) : base() { this.Item = item; }
            public override T Match<T>(
                Func<union.Email, T> f, 
                Func<union.Phone, T> g, 
                Func<Tuple<union.Email, union.Phone>, T> h)
            {
                return f(Item);
            }
        }

        public sealed class JustPhone : Contact
        {
            public readonly Phone Item;
            public JustPhone(Phone item) { this.Item = item; }
            public override T Match<T>(
                Func<union.Email, T> f, 
                Func<Phone, T> g,
                Func<Tuple<union.Email, Phone>, T> h)
            {
                return g(Item);
            }
        }

        public sealed class EmailAndPhone : Contact
        {
            public readonly Tuple<union.Email, Phone> Item;
            public EmailAndPhone(Tuple<union.Email, Phone> item) { 
                this.Item = item; 
            }
            public override T Match<T>(
                Func<union.Email, T> f, 
                Func<Phone, T> g, 
                Func<Tuple<union.Email, Phone>, T> h)
            {
                return h(Item);
            }
        }
    }

    public abstract class Union3<A, B, C>
    {
        public abstract T Match<T>(Func<A, T> f, Func<B, T> g, Func<C, T> h);
        // private ctor ensures no external classes can inherit
        private Union3() { } 

        public sealed class Case1 : Union3<A, B, C>
        {
            public readonly A Item;
            public Case1(A item) : base() { this.Item = item; }
            public override T Match<T>(Func<A, T> f, Func<B, T> g, Func<C, T> h)
            {
                return f(Item);
            }
        }

        public sealed class Case2 : Union3<A, B, C>
        {
            public readonly B Item;
            public Case2(B item) { this.Item = item; }
            public override T Match<T>(Func<A, T> f, Func<B, T> g, Func<C, T> h)
            {
                return g(Item);
            }
        }

        public sealed class Case3 : Union3<A, B, C>
        {
            public readonly C Item;
            public Case3(C item) { this.Item = item; }
            public override T Match<T>(Func<A, T> f, Func<B, T> g, Func<C, T> h)
            {
                return h(Item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var email = new Email("thing@thing.com");
            var phone = new Phone("01234 567 890");
            var contacts = new List<Contact>{
                new Contact.Email(email),
                new Contact.JustPhone(phone),
                new Contact.EmailAndPhone(new Tuple<Email, Phone>(email, phone))
            };
            foreach(var contact in contacts){
                var toPrint = contact.Match(
                    x => x.Value,
                    x => x.Value,
                    x => x.Item1.Value + " " + x.Item2.Value
                );
                Console.WriteLine(toPrint);
            }

            Union3<int, char, string>[] unions = new Union3<int,char,string>[]
                {
                    new Union3<int, char, string>.Case1(5),
                    new Union3<int, char, string>.Case2('x'),
                    new Union3<int, char, string>.Case3("Juliet")
                };

            foreach (Union3<int, char, string> union in unions)
            {
                string value = union.Match(
                    num => num.ToString(),
                    character => new string(new char[] { character }),
                    word => word);
                Console.WriteLine("Matched union with value '{0}'", value);
            }

            Console.ReadKey();
        }
    }
}
