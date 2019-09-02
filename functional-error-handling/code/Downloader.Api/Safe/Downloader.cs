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
                return Error(new SftpUnauthorised());
            }

            if (fileName == FileNames.FileMissingOnSftp)
            {
                return Error(new FileDoesntExist());
            }

            if (fileName == FileNames.FileDoesntParse)
            {
                return Ok(FileContent.InvalidFile);
            }

            return Ok(FileContent.ValidFile);
        }

        private static Result<string, IJohnAmountError> Ok(string content)
        {
            return Result<string, IJohnAmountError>.Ok(content);
        }

        private Result<string, IJohnAmountError> Error(IJohnAmountError error)
        {
            return Result<string, IJohnAmountError>.Error(error);
        }
    }
}
