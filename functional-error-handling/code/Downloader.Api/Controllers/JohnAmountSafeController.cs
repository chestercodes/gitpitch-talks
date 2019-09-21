using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Downloader.Api.Controllers
{
    using Safe;
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
    }
}
