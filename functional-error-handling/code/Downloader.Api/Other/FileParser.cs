using System;

namespace Downloader.Api.Other
{
    using System.Collections.Generic;
    using System.Linq;

    public class FileParser
    {
        public List<PersonAmount> Parse(string contents)
        {
            var lines = contents.Split('\n');

            return lines.Select(ParseLine).ToList();
        }

        private PersonAmount ParseLine(string line)
        {
            var parts = line.Split(',');

            if (parts.Length == 2)
            {
                var name = parts[0];
                if (decimal.TryParse(parts[1], out decimal val))
                {
                    return new PersonAmount(name, val);
                }
            }

            throw new Exception("File doesnt parse!");
        }
    }
}
