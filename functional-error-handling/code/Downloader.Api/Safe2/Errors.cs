namespace Downloader.Api.Safe2
{
    public class JohnAmountError
    {
        private JohnAmountError(){}
        
        public class FileDoesntParse : JohnAmountError { }


        public class DownloadFailed : JohnAmountError
        {
            public DownloadFailed(DownloadError error)
            {
                Error = error;
            }

            public DownloadError Error { get; }
        }    
    }

    public class DownloadError
    {
        private DownloadError(){}
        
        public class SftpUnauthorised : DownloadError { }

        public class FileDoesntExist : DownloadError { }
    }
}
