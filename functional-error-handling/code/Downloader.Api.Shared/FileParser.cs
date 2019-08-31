namespace Downloader.Api.Shared
{
    using System.Collections.Generic;
    using System.Linq;

    public class FileParser
    {
        public IEnumerable<PersonAmount> Parse(string contents)
        {
            var lines = contents.Split('\n');

            return lines.Select(ParseLine);
        }

        private PersonAmount ParseLine(string line)
        {
            var parts = line.Split(',');

            return new PersonAmount(parts[0], decimal.Parse(parts[1]));
        }
    }
}
