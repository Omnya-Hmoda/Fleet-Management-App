using Microsoft.AspNetCore.Mvc;
using FPro;
using Newtonsoft.Json;
using managementclasses;
using Npgsql;
using System.Data;
namespace Fleet_Management_App.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class vehiclecontroller : Controller
    {

        [HttpPost("add")]
        public IActionResult addvehicle([FromBody] GVAR gVAR)
        {
            Vehicle v = new Vehicle();
            GVAR gVAR2 = new GVAR();
            try
            {
                if (gVAR == null)
                {
                    gVAR.DicOfDic["Tags"]["sts"] = "0";
                    return BadRequest(JsonConvert.SerializeObject(gVAR));
                }

                gVAR2 = v.AddVehicle(gVAR);
                gVAR2.DicOfDic["Tags"]["sts"] = "1";
                return Ok(JsonConvert.SerializeObject(gVAR2));


            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gVAR));
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateVehicle([FromBody] GVAR gvar)
        {
            Vehicle v = new Vehicle();
            GVAR gvar2 = new GVAR();
            try
            {
                if (gvar == null)
                {
                    gvar.DicOfDic["Tags"]["sts"] = "0";
                    return BadRequest(JsonConvert.SerializeObject(gvar));
                }

                gvar2 = v.UpdateVehicle(gvar);
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar));
            }
        }

        [HttpDelete("{vehicleId}")]
        public IActionResult DeleteVehicle(String vehicleId)
        {
            Vehicle v = new Vehicle();
         
            try
            {
               v.DeleteVehicle(vehicleId);
                return Ok("Delete successfully");
            }
            catch
            {
                return BadRequest("There is an error");
            }
        }


  
        }



    }






