using System.Linq;
using Result = Downloader.Api.Other.Result<System.Collections.Generic.IEnumerable<Downloader.Api.Other.PersonAmount>, Downloader.Api.Safe.IJohnAmountError>;

namespace Downloader.Api.Safe
{
    using Other;
    
    public class Parser
    {
        public Result Parse(string contents)
        {
            var fileParser = new FileParser();

            try
            {
                var personAmounts = fileParser.Parse(contents).ToList();

                return Result.Ok(personAmounts);
            }
            catch
            {
                return Result.Error(new FileDoesntParse());
            }
        }
    }
}
