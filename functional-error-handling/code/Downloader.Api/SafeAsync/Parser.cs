using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Downloader.Api.Safe;
using LanguageExt;

namespace Downloader.Api.SafeAsync
{
    using Other;
    
    public class Parser
    {
        public Either<JohnAmountError, IEnumerable<PersonAmount>> Parse(string contents)
        {
            var fileParser = new FileParser();

            try
            {
                var personAmounts = fileParser.Parse(contents).ToList();

                return personAmounts;
            }
            catch
            {
                return new JohnAmountError.FileDoesntParse();
            }
        }
    }
}
