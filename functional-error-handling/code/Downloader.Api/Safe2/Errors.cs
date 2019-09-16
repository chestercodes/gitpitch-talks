namespace Downloader.Api.Safe2
{ 
    public interface IJohnAmountError {}

    public class FileDoesntParse : IJohnAmountError { }


    public class DownloadError : IJohnAmountError
    {
        public DownloadError(IDownloadError error)
        {
            Error = error;
        }

        public IDownloadError Error { get; }
    }

    public interface IDownloadError { }

    public class SftpUnauthorised : IDownloadError { }

    public class FileDoesntExist : IDownloadError { }

}
