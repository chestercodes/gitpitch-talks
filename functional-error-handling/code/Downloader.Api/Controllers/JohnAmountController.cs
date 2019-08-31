using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Downloader.Api.Controllers
{
    using Unsafe;
    using Downloader.Api.Shared;

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
                catch (Exception ex) when (ex.Message == Errors.FileDoesntParse)
                {
                    return BadRequest();
                }
            } catch (Exception ex) when (ex.Message == Errors.UnauthorisedSftp)
            {
                return Unauthorized();
            }
            catch (Exception ex) when (ex.Message == Errors.FileMissingOnSftp)
            {
                return NotFound();
            }
        }
    }
}
