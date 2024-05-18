using FPro;
using managementclasses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fleet_Management_App.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GeofencesController : Controller
    {


        [HttpGet("AllGeofences")]
        public IActionResult GetAllGeofences()
        {
            Geofences geofences = new Geofences();

            GVAR gvar2 = new GVAR();
            try
            {
                gvar2 = geofences.GetAllGeofences();
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar2));
            }
        }

        [HttpGet("AllCircularGeofences")]
        public IActionResult GetAllCircularGeofences()
        {
            Geofences geofences = new Geofences();

            GVAR gvar2 = new GVAR();
            try
            {
                gvar2 = geofences.GetAllCircularGeofences();
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar2));
            }
        }

        [HttpGet("AllRectangleGeofence")]
        public IActionResult GetAllRectangleGeofence()
        {
            Geofences geofences = new Geofences();

            GVAR gvar2 = new GVAR();
            try
            {
                gvar2 = geofences.GetAllRectangleGeofence();
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar2));
            }
        }

        [HttpGet("AllPolygonGeofence")]
        public IActionResult GetAllPolygonGeofence()
        {
            Geofences geofences = new Geofences();

            GVAR gvar2 = new GVAR();
            try
            {
                gvar2 = geofences.GetPolygonGeofence();
                return Ok(JsonConvert.SerializeObject(gvar2));
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject(gvar2));
            }
        }
    }
}
