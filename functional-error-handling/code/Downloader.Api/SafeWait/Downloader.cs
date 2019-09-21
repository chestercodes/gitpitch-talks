using System.Threading;
using Downloader.Api.Safe;
using Downloader.Api.Shared;
using LanguageExt;

namespace Downloader.Api.SafeWait
{
    public class Downloader
    {
        public Either<JohnAmountError, string> GetFile(string fileName)
        {
            Thread.Sleep(2000);

            if (fileName == FileNames.UnauthorisedSftp)
            {
                return new JohnAmountError.SftpUnauthorised();
            }

            if (fileName == FileNames.FileMissingOnSftp)
            {
                return new JohnAmountError.FileDoesntExist();
            }

            if (fileName == FileNames.FileDoesntParse)
            {
                return FileContent.InvalidFile;
            }

            return FileContent.ValidFile;
        }
    }
}
