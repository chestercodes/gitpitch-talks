using System.Threading.Tasks;
using Downloader.Api.Safe;
using Downloader.Api.Shared;
using LanguageExt;

namespace Downloader.Api.SafeAsync
{
    public class Downloader
    {
        public async Task<Either<IJohnAmountError, string>> GetFile(string fileName)
        {
            await Task.Delay(2000);

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
