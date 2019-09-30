using System;
using System.Collections.Generic;
using System.Linq;
using Downloader.Api.Other;
using Downloader.Api.Shared;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace Downloader.Api.Controllers
{
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

        public class JohnAmountError
        {
            private JohnAmountError() { }
            public class FileDoesntParse : JohnAmountError { }
            public class DownloadFailed : JohnAmountError
            {
                public DownloadFailed(DownloadError error)
                {
                    Error = error;
                }
                public DownloadError Error { get; }
            }
        }

        public class DownloadError
        {
            private DownloadError() { }
            public class SftpUnauthorised : DownloadError { }
            public class FileDoesntExist : DownloadError { }
        }

        public class Downloader
        {
            public Either<DownloadError, string> GetFile(string fileName)
            {
                if (fileName == FileNames.UnauthorisedSftp)
                {
                    return new DownloadError.SftpUnauthorised();
                }

                if (fileName == FileNames.FileMissingOnSftp)
                {
                    return new DownloadError.FileDoesntExist();
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
