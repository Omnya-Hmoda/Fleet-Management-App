using Microsoft.AspNetCore.Mvc;
using FPro;
using Newtonsoft.Json;
using managementclasses;

namespace Fleet_Management_App.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class VehicleInformationcontroller : Controller
    {

        [HttpPost("add")]
        public IActionResult addinfo([FromBody] GVAR gVAR)
        {
            VehicleInformation v = new VehicleInformation();
            GVAR gVAR2 = new GVAR();
            try
            {
                if (gVAR == null)
                {
                    gVAR.DicOfDic["Tags"]["sts"] = "0";
                    return BadRequest(JsonConvert.SerializeObject(gVAR));
                }

                gVAR2 = v.AddVehicleInformation(gVAR);
                gVAR2.DicOfDic["Tags"]["sts"] = "1";
                return Ok(JsonConvert.SerializeObject(gVAR2));


            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gVAR));
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateVehicleinfo([FromBody] GVAR gvar)
        {
            VehicleInformation v = new VehicleInformation();
            GVAR gvar2 = new GVAR();
            try
            {
                if (gvar == null)
                {
                    gvar.DicOfDic["Tags"]["sts"] = "0";
                    return BadRequest(JsonConvert.SerializeObject(gvar));
                }

                gvar2 = v.UpdateVehicleInformation(gvar);
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar));
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteVehicleinfo(String Id)
        {
            VehicleInformation v = new VehicleInformation();

            try
            {
                v.DeleteVehicleInformation(Id);
                return Ok("Delete successfully");
            }
            catch
            {
                return BadRequest("There is an error");
            }
        }


        [HttpGet("Allvehicle")]
        public IActionResult GetVehicleDetails()
        {
            VehicleInformation v = new VehicleInformation();
            GVAR gvar2 = new GVAR();
            try
            {
                gvar2 = v.AllVehicleDetails();
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar2));
            }

        }


        [HttpGet("{vehicleID}")]
        public IActionResult GetVehicleInformation(String vehicleID)
        {
            VehicleInformation v = new VehicleInformation();
            GVAR gvar2 = new GVAR();
            try
            {
                gvar2 = v.GetVehicleInformation(vehicleID);
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar2));
            }

        }

    }
}
