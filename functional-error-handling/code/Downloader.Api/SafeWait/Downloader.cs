using System.Threading;
using Downloader.Api.Safe;
using Downloader.Api.Shared;
using LanguageExt;

namespace Downloader.Api.SafeWait
{
    public class Downloader
    {
        public Either<IJohnAmountError, string> GetFile(string fileName)
        {
            Thread.Sleep(2000);

            if (fileName == FileNames.UnauthorisedSftp)
            {
                return new SftpUnauthorised();
            }

            if (fileName == FileNames.FileMissingOnSftp)
            {
                return new FileDoesntExist();
            }

            if (fileName == FileNames.FileDoesntParse)
            {
                return FileContent.InvalidFile;
            }

            return FileContent.ValidFile;
        }
    }
}
