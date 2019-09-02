namespace Downloader.Api.Shared
{
    public class FileNames
    {
        public const string UnauthorisedSftp = "UnauthorisedSftp";
        public const string FileMissingOnSftp = "FileMissingOnSftp";
        public const string FileDoesntParse = "FileDoesntParse";
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
