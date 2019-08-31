using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .Match<decimal, IJohnAmountError, ActionResult>(
                amount =>
                {
                    return this.Ok(amount);
                },
                error =>
                {
                    switch (error)
                    {
                        case SftpUnauthorised _:
                            return Unauthorized();
                        case FileDoesntExist _:
                            return NotFound();
                        case FileDoesntParse _:
                            return this.BadRequest();
                        default:
                            return this.BadRequest();
                    }
                });
        }
    }
}
