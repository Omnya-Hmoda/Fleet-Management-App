using FPro;
using managementclasses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fleet_Management_App.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RouteHistoryController : Controller
    {
        [HttpPost("add")]
        public IActionResult addvehicle([FromBody] GVAR gVAR)
        {
            RouteHistory rout = new RouteHistory();
            GVAR gVAR2 = new GVAR();
            try
            {
                if (gVAR == null)
                {
                    gVAR.DicOfDic["Tags"]["sts"] = "0";
                    return BadRequest(JsonConvert.SerializeObject(gVAR));
                }

                gVAR2 = rout.AddHistoricalPoint(gVAR);
                gVAR2.DicOfDic["Tags"]["sts"] = "1";
                return Ok(JsonConvert.SerializeObject(gVAR2));


            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gVAR));
            }
        }


        [HttpGet("{vehicleID}/{startEpoch}/{endEpoch}")]
        public IActionResult GetVehicleRouteHistory(string vehicleID, long startEpoch, long endEpoch)
        {
            RouteHistory vrh = new RouteHistory();
            GVAR gvar2 = new GVAR();
            try
            {
                gvar2 = vrh.GetVehicleRouteHistory(vehicleID, startEpoch, endEpoch);
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar2));
            }
        }
    }
}
