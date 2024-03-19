using Lab7.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    public class VersionedController : ControllerBase
    {
        private readonly IVersionedService _versionedService;

        public VersionedController(IVersionedService versionedService)
        {
            _versionedService = versionedService;
        }

        [HttpGet("/version1"), MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult GetVersion1()
        {
            int result = _versionedService.GetVersion1();
            return Ok(result);
        }

        [HttpGet("/version2"), MapToApiVersion("2.0")]
        [Authorize]
        public IActionResult GetVersion2()
        {
            string result = _versionedService.GetVersion2();
            return Ok(result);
        }

        [HttpGet("/version3"), MapToApiVersion("3.0")]
        [Authorize]
        public IActionResult GetVersion3()
        {
            byte[] excelFile = _versionedService.GetVersion3();
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "APIVersioning.xlsx");
        }
    }
}
