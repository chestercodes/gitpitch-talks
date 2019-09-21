using Downloader.Api.Shared;

namespace Downloader.Api.Safe
{
    using Other;

    public class Downloader
    {
        public Result<string, JohnAmountError> GetFile(string fileName)
        {
            if (fileName == FileNames.UnauthorisedSftp)
            {
                return Error(new JohnAmountError.SftpUnauthorised());
            }

            if (fileName == FileNames.FileMissingOnSftp)
            {
                return Error(new JohnAmountError.FileDoesntExist());
            }

            if (fileName == FileNames.FileDoesntParse)
            {
                return Ok(FileContent.InvalidFile);
            }

            return Ok(FileContent.ValidFile);
        }

        private static Result<string, JohnAmountError> Ok(string content)
        {
            return Result<string, JohnAmountError>.Ok(content);
        }

        private Result<string, JohnAmountError> Error(JohnAmountError error)
        {
            return Result<string, JohnAmountError>.Error(error);
        }
    }
}
