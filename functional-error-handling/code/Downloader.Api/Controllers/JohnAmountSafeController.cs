using System;
using System.Linq;
using Downloader.Api.Other;
using Downloader.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using Result = Downloader.Api.Other.Result<System.Collections.Generic.IEnumerable<Downloader.Api.Other.PersonAmount>, Downloader.Api.Other.JohnAmountError>;

namespace Downloader.Api.Controllers
{
    using static Other.ResultExtensions;

    [Route("api/[controller]")]
    [ApiController]
    public class JohnAmountSafeController : ControllerBase
    {
        [HttpGet("{fileName}")]
        public ActionResult Get(string fileName)
        {
            var downloader = new Downloader();
            var parser = new Parser();

            return downloader
                .GetFile(fileName)
                .Bind(parser.Parse)
                .Map(personAmounts =>
                {
                    return personAmounts
                        .Where(x => x.Name == "John")
                        .Sum(x => x.Amount);
                })
                .Match<decimal, JohnAmountError, ActionResult>(
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
            public Result<string, JohnAmountError> GetFile(string fileName)
            {
                if (fileName == FileNames.UnauthorisedSftp)
                {
                    return Error(new JohnAmountError.SftpUnauthorised());
                }

                if (fileName == FileNames.FileMissingOnSftp)
                {
                    return Error(new JohnAmountError.FileDoesntExist());
                }

                if (fileName == FileNames.FileDoesntParse)
                {
                    return Ok(FileContent.InvalidFile);
                }

                return Ok(FileContent.ValidFile);
            }

            private static Result<string, JohnAmountError> Ok(string content)
            {
                return Result<string, JohnAmountError>.Ok(content);
            }

            private Result<string, JohnAmountError> Error(JohnAmountError error)
            {
                return Result<string, JohnAmountError>.Error(error);
            }
        }

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
                    return Result.Error(new JohnAmountError.FileDoesntParse());
                }
            }
        }
    }
}
