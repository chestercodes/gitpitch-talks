using Downloader.Api.Shared;

namespace Downloader.Api.Unsafe
{
    using System;
    using System.Collections.Generic;

    public class Parser
    {
        public IEnumerable<PersonAmount> Parse(string content)
        {
            var fileParser = new FileParser();

            try
            {
                return fileParser.Parse(content);
            }
            catch
            {
                throw new Exception(Errors.FileDoesntParse);
            }
        }
    }
}
