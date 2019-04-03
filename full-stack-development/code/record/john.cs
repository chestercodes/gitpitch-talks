public class Person : IEquatable<Person> {
    public int Age { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }
    public Person(string name, int age, Address address) { 
        Name = name; Age = age; Address = address; 
    }
    public bool Equals(Person other) {
        return other.Age == Age && other.Name == Name
          && other.Address.Equals(Address);
    }
}

public class Address : IEquatable<Address> {
    public string Line1 { get; private set; }
    public string Line2 { get; private set; }
    public string PostCode { get; private set; }
    public Address(string line1, string line2, string postCode) {
        Line1 = line1; Line2 = line2; PostCode = postCode;
    }
    public bool Equals(Address other) {
        return other.Line1 == Line1 && other.Line2 == Line2
          && other.PostCode == PostCode;
    }
}

public class Program
{
    public static void Main()
    {
        var john = new Person(
            "John", 30,
            new Address("1 lane", "1 street", "BS11BS"));

        var sameJohn = new Person(
            "John", 30,
            new Address("1 lane", "1 street", "BS11BS"));

        Console.WriteLine($"Johns are equal - " + (john.Equals(sameJohn)));
        // Johns are equal - True

        var copyJohn = new Person(
          john.Name, john.Age,
          new Address(
            "2 lane", // moved next door
            john.Address.Line2,
            john.Address.PostCode)
          );
    }
}