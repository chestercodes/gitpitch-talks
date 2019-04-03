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

public class Contact 
{
    public Email Email { get; }
    public Phone Phone { get; }
    public Contact (Email email, Phone phone) 
    {
        if(email == null && phone == null) 
        {
            // Neither Email or Phone is Invalid
            throw new Exception("Need to specify email or phone");
        }
        Email = email;
        Phone = phone;
    }
}

public class Program 
{
    public static Main () 
    {
        var email = new Email("some@email.com");
        var phone = new Phone("01234 567890");
        
        var emailOnlyContact = new Contact(email, null);

        var phoneOnlyContact = new Contact(null, phone);
        
        var emailAndPhoneOnlyContact = new Contact(email, phone);
        
        // throws runtime exception
        var neitherEmailOrPhoneContact = new Contact(null, null);
    }
}