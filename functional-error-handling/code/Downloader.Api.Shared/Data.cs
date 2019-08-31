namespace Downloader.Api.Shared
{
    public class Errors
    {
        public const string UnauthorisedSftp = "Unauthorised Sftp Message";
        public const string FileMissingOnSftp = "File missing on sftp";
        public const string FileDoesntParse = "File doesnt parse";
    }

    public class FileNames
    {
        public const string UnauthorisedSftp = "UnauthorisedSftp";
        public const string FileMissingOnSftp = "FileMissingOnSftp";
        public const string FileDoesntParse = "FileDoesntParse";
    }

    public class PersonAmount
    {
        public PersonAmount(string name, decimal amount)
        {
            this.Name = name;
            this.Amount = amount;
        }

        public string Name { get; }
        public decimal Amount { get; }
    }

    public class FileContent
    {
        public const string ValidFile = @"John,1
Chester,2
John,3
Dave,4";

        public const string InvalidFile = @"sdfdsf
dfsdfg";
    }
}
