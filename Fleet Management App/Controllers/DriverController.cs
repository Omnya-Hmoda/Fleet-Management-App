using FPro;
using managementclasses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fleet_Management_App.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DriverController : Controller
    {
        [HttpPost("add")]
        public IActionResult addDriver([FromBody] GVAR gVAR)
        {
            Driver v = new Driver();
            GVAR gVAR2 = new GVAR();
            try
            {
                if (gVAR == null)
                {
                    gVAR.DicOfDic["Tags"]["sts"] = "0";
                    return BadRequest(JsonConvert.SerializeObject(gVAR));
                }

                gVAR2 = v.AddDriver(gVAR);
                gVAR2.DicOfDic["Tags"]["sts"] = "1";
                return Ok(JsonConvert.SerializeObject(gVAR2));


            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gVAR));
            }
        }


        [HttpPut("update")]
        public IActionResult UpdateDriver([FromBody] GVAR gvar)
        {
            Driver v = new Driver();
            GVAR gvar2 = new GVAR();
            try
            {
                if (gvar == null)
                {
                    gvar.DicOfDic["Tags"]["sts"] = "0";
                    return BadRequest(JsonConvert.SerializeObject(gvar));
                }

                gvar2 = v.UpdateDriver(gvar);
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar));
            }
        }



        [HttpDelete("{DriverId}")]
        public IActionResult DeleteDriver(String DriverId)
        {
            Driver v = new Driver();

            try
            {
                v.DeleteDriver(DriverId);
                return Ok("Delete successfully");
            }
            catch
            {
                return BadRequest("There is an error");
            }
        }


        [HttpGet]
        public IActionResult AllDriver()
        {
            Driver v = new Driver();
            GVAR gvar = new GVAR();
            try
            {
               

                gvar = v.AllDriver();
                return Ok(JsonConvert.SerializeObject(gvar));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar));
            }
        }


    }
}
