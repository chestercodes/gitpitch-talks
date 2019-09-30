using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using Downloader.Api.Other;
using Downloader.Api.Shared;
using LanguageExt;

namespace Downloader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JohnAmountSafeWaitController : ControllerBase
    {
        [HttpGet("{fileName}")]
        public ActionResult Get(string fileName)
        {
            var downloader = new Downloader();
            var parser = new Parser();

            return (downloader
                .GetFile(fileName)
                .Bind(parser.Parse)
                .Map(personAmounts =>
                {
                    return personAmounts
                        .Where(x => x.Name == "John")
                        .Sum(x => x.Amount);
                }))
                .Match<ActionResult>(
                    amount => this.Ok(amount),
                    error =>
                    {
                        switch (error)
                        {
                            case JohnAmountError.SftpUnauthorised _:
                                return Unauthorized();
                            case JohnAmountError.FileDoesntExist _:
                                return NotFound();
                            case JohnAmountError.FileDoesntParse _:
                                return this.UnprocessableEntity();
                            default:
                                throw new NotImplementedException($"Not implemented program error type {error.GetType().FullName}");
                        }
                    });
        }

        public class Downloader
        {
            public Either<JohnAmountError, string> GetFile(string fileName)
            {
                Thread.Sleep(2000);

                if (fileName == FileNames.UnauthorisedSftp)
                {
                    return new JohnAmountError.SftpUnauthorised();
                }

                if (fileName == FileNames.FileMissingOnSftp)
                {
                    return new JohnAmountError.FileDoesntExist();
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
}
