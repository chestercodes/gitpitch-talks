using Downloader.Api.Shared;

namespace Downloader.Api.Unsafe
{
    public class Downloader
    {
        public string GetFile(string fileName)
        {
            if(fileName == FileNames.UnauthorisedSftp)
            {
                throw new SftpUnauthorisedException();
            }

            if (fileName == FileNames.FileMissingOnSftp)
            {
                throw new FileDoesntExistException();
            }

            if(fileName == FileNames.FileDoesntParse)
            {
                return FileContent.InvalidFile;
            }

            return FileContent.ValidFile;
        }
    }
}
