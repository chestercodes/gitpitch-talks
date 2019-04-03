using fs.Shared;

namespace cs
{
    class Program
    {
        static void Main(string[] args)
        {
            var myAddress = new Address("1 house", "2 lane", "BS2 2BS");

            var email = Email.NewEmail("a@b.com");

            var justEmail = Contact.NewJustEmail(email);

            var contactInfoAsString = Utils.contactInfoToString(justEmail);

            System.Console.WriteLine(contactInfoAsString);

            System.Console.ReadKey();
        }
    }
}
