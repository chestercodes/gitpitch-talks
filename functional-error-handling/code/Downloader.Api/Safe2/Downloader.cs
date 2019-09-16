using Downloader.Api.Shared;
using LanguageExt;

namespace Downloader.Api.Safe2
{
    public class Downloader
    {
        public Either<IDownloadError, string> GetFile(string fileName)
        {
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
