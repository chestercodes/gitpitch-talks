
namespace Downloader.Api.Safe2
{
    using Other;
    
    public class Parser
    {
        public Either<IJohnError, IEnumerable<PersonAmount>> Parse(string contents)
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
