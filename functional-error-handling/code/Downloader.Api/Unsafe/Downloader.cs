using Downloader.Api.Shared;

namespace Downloader.Api.Unsafe
{
    using System;

    public class Downloader
    {
        public string GetFile(string fileName)
        {
            if(fileName == FileNames.UnauthorisedSftp)
            {
                throw new Exception(Errors.UnauthorisedSftp);
            }

            if (fileName == FileNames.FileMissingOnSftp)
            {
                throw new Exception(Errors.FileMissingOnSftp);
            }

            if(fileName == FileNames.FileDoesntParse)
            {
                return FileContent.InvalidFile;
            }

            return FileContent.ValidFile;
        }
    }
}
