using System;
using System.Collections.Generic;
using System.Linq;
using Downloader.Api.Other;
using Downloader.Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Downloader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JohnAmountController : ControllerBase
    {
        [HttpGet("{fileName}")]
        public ActionResult Get(string fileName)
        {
            var downloader = new Downloader();
            var parser = new Parser();

            try
            {
                var content = downloader.GetFile(fileName);

                try
                {
                    var csvLines = parser.Parse(content);

                    var amount = csvLines
                        .Where(x => x.Name == "John")
                        .Sum(x => x.Amount);

                    return Ok(amount);
                }
                catch (FileDoesntParseException)
                {
                    return UnprocessableEntity();
                }
            } catch (SftpUnauthorisedException)
            {
                return Unauthorized();
            }
            catch (FileDoesntExistException)
            {
                return NotFound();
            }
        }

        public class SftpUnauthorisedException : Exception { }
        public class FileDoesntExistException : Exception { }
        public class FileDoesntParseException : Exception { }

        public class Downloader
        {
            public string GetFile(string fileName)
            {
                if (fileName == FileNames.UnauthorisedSftp)
                {
                    throw new SftpUnauthorisedException();
                }

                if (fileName == FileNames.FileMissingOnSftp)
                {
                    throw new FileDoesntExistException();
                }

                if (fileName == FileNames.FileDoesntParse)
                {
                    return FileContent.InvalidFile;
                }

                return FileContent.ValidFile;
            }
        }

        public class Parser
        {
            public IEnumerable<PersonAmount> Parse(string content)
            {
                var fileParser = new FileParser();

                try
                {
                    return fileParser.Parse(content);
                }
                catch (Exception)
                {
                    throw new FileDoesntParseException();
                }
            }
        }
    }
}
