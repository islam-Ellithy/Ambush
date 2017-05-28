using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ambush.Models;
using Newtonsoft.Json.Linq;

namespace Ambush.Controllers
{
    public class WeaponTypeController : ApiController
    {
        // GET: api/WeaponType
        public IHttpActionResult Get()
        {
            if(WeaponType.GetAllWeaponType().HasValues)
                return Ok(WeaponType.GetAllWeaponType());
            return NotFound();
        }

        // GET: api/WeaponType?name=snipper
        public IHttpActionResult Get(string name)
        {
            JObject wt = WeaponType.GetWeaponTypeByName(name);
            if (wt.HasValues)
                return Ok(wt);
            return NotFound() ;
        }

        // POST: api/WeaponType
        public IHttpActionResult Post([FromBody]WeaponType value)
        {
            return Ok(WeaponType.AddWeaponType(value));
        }

        // PUT: api/WeaponType/5
        public IHttpActionResult Put(string weaponTypeName, [FromBody]WeaponType value)
        {
            return Ok(WeaponType.UpdateWeaponType(value, weaponTypeName));
        }

        // DELETE: api/WeaponType/5
        public IHttpActionResult Delete(string weaponTypeName)
        {
            return Ok(WeaponType.DeleteWeaponType(weaponTypeName));
        }
    }
}
