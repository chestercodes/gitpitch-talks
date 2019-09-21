﻿namespace Downloader.Api.Safe
{ 
    public class JohnAmountError
    {
        private JohnAmountError(){ }

        public class SftpUnauthorised : JohnAmountError { }

        public class FileDoesntExist : JohnAmountError { }
        
        public class FileDoesntParse : JohnAmountError { }
    }
}
