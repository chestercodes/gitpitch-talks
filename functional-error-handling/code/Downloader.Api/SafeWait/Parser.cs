using System.Collections.Generic;
using System.Linq;
using Downloader.Api.Safe;
using LanguageExt;

namespace Downloader.Api.SafeWait
{
    using Other;
    
    public class Parser
    {
        public Either<IJohnAmountError, IEnumerable<PersonAmount>> Parse(string contents)
        {
            var fileParser = new FileParser();

            try
            {
                var personAmounts = fileParser.Parse(contents).ToList();

                return personAmounts;
            }
            catch
            {
                return new FileDoesntParse();
            }
        }
    }
}
