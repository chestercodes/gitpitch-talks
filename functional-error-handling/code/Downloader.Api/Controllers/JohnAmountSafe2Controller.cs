using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Downloader.Api.Controllers
{
    using Safe2;

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
                                        // DownloadError => JohnAmountError
                .MapLeft<JohnAmountError>(error => new JohnAmountError.DownloadFailed(error))
                .Bind(parser.Parse)
                .Map(personAmounts =>
                {
                    return personAmounts
                        .Where(x => x.Name == "John")
                        .Sum(x => x.Amount);
                })
                .Match<ActionResult>(
                    amount => this.Ok(amount),
                    error =>
                    {
                        switch (error)
                        {
                            case JohnAmountError.DownloadFailed downloadError:
                                switch (downloadError.Error)
                                {
                                    case DownloadError.SftpUnauthorised _:
                                        return Unauthorized();
                                    case DownloadError.FileDoesntExist _:
                                        return NotFound();

                                    default: throw UnhandledReturnType(downloadError);
                                }
                            case JohnAmountError.FileDoesntParse _:
                                return this.UnprocessableEntity();

                            default: throw UnhandledReturnType(error);
                        }
                    });
        }

        private static Exception UnhandledReturnType(object err)
        {
            return new NotImplementedException($"Not implemented program error type {err.GetType().FullName}");
        }
    }
}
