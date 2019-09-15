using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Downloader.Api.Controllers
{
    using Safe2;
    using static Other.ResultExtensions;

    [Route("api/[controller]")]
    [ApiController]
    public class JohnAmountSafe2Controller : ControllerBase
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
                .Match<decimal, IJohnAmountError, ActionResult>(
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
