
using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace Downloader.Api.Safe2
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
