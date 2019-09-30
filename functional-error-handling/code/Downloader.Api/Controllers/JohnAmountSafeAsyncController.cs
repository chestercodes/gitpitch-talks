using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Downloader.Api.Other;
using Downloader.Api.Shared;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace Downloader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JohnAmountSafeAsyncController : ControllerBase
    {
        [HttpGet("{fileName}")]
        public async Task<ActionResult> Get(string fileName)
        {
            var downloader = new Downloader();
            var parser = new Parser();

            return (await downloader
                .GetFile(fileName)
                .BindAsync(parser.Parse)
                .MapAsync(personAmounts =>
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
            public async Task<Either<JohnAmountError, string>> GetFile(string fileName)
            {
                await Task.Delay(2000);

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
