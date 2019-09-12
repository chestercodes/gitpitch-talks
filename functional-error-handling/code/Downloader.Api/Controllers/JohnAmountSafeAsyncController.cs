using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Downloader.Api.Other;
using Downloader.Api.Safe;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace Downloader.Api.Controllers
{
    using SafeAsync;

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
                            case SftpUnauthorised _:
                                return Unauthorized();
                            case FileDoesntExist _:
                                return NotFound();
                            case FileDoesntParse _:
                                return this.UnprocessableEntity();
                            default:
                                throw new NotImplementedException($"Not implemented program error type {error.GetType().FullName}");
                        }
                    });
        }
    }
}
