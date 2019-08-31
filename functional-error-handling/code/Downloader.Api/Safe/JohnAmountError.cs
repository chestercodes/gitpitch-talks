namespace Downloader.Api.Safe
{ 
    public interface IJohnAmountError {}

    public class SftpUnauthorised : IJohnAmountError { }

    public class FileDoesntExist : IJohnAmountError { }
    
    public class FileDoesntParse : IJohnAmountError { }
}
