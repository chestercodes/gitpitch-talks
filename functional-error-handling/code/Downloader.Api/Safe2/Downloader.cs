using Downloader.Api.Shared;
using LanguageExt;

namespace Downloader.Api.Safe2
{
    public class Downloader
    {
        public Either<DownloadError, string> GetFile(string fileName)
        {
            if (fileName == FileNames.UnauthorisedSftp)
            {
                return new DownloadError.SftpUnauthorised();
            }

            if (fileName == FileNames.FileMissingOnSftp)
            {
                return new DownloadError.FileDoesntExist();
            }

            if (fileName == FileNames.FileDoesntParse)
            {
                return FileContent.InvalidFile;
            }

            return FileContent.ValidFile;
        }
    }
}
