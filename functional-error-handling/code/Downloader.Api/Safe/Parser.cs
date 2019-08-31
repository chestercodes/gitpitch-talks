using Downloader.Api.Shared;

namespace Downloader.Api.Safe
{
    using Other;
    using System.Collections.Generic;
    
    public class Parser
    {
        public Result<IEnumerable<PersonAmount>, IJohnAmountError> Parse(string contents)
        {
            var fileParser = new FileParser();

            try
            {
                return Success(fileParser.Parse(contents));
            }
            catch
            {
                return FailWith(new FileDoesntParse());
            }
        }

        private Result<IEnumerable<PersonAmount>, IJohnAmountError> FailWith(IJohnAmountError error)
        {
            return Result<IEnumerable<PersonAmount>, IJohnAmountError>.ToError(error);
        }

        private Result<IEnumerable<PersonAmount>, IJohnAmountError> Success(IEnumerable<PersonAmount> result)
        {
            return Result<IEnumerable<PersonAmount>, IJohnAmountError>.ToOk(result);
        }
    }
}
