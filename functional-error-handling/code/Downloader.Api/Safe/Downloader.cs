using Downloader.Api.Shared;

namespace Downloader.Api.Safe
{
    using Other;

    public class Downloader
    {
        public Result<string, IJohnAmountError> GetFile(string fileName)
        {
            if (fileName == FileNames.UnauthorisedSftp)
            {
                return FailWith(new SftpUnauthorised());
            }

            if (fileName == FileNames.FileMissingOnSftp)
            {
                return FailWith(new FileDoesntExist());
            }

            return Result<string, IJohnAmountError>.ToOk(FileContent.ValidFile);
        }

        private Result<string, IJohnAmountError> FailWith(IJohnAmountError error)
        {
            return Result<string, IJohnAmountError>.ToError(error);
        }
    }
}
