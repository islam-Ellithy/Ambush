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
    public class PlayerWeaponSkillController : ApiController
    {
        // GET: api/PlayerWeaponSkill
        public IHttpActionResult Get()
        {
            if(PlayerWeaponSkill.GetPlayerWeaponSkills().HasValues)
                return Ok(PlayerWeaponSkill.GetPlayerWeaponSkills());
            return NotFound();
        }

        // GET: api/PlayerWeaponSkill?playerId=1&weaponTypeId=2
        public IHttpActionResult Get(int playerId,int weaponTypeId)
        {
            JObject pw = PlayerWeaponSkill.GetPlayerWeaponSkill(playerId, weaponTypeId);
            if(pw.HasValues)
                return Ok(pw);
            return NotFound();
        }

        // POST: api/PlayerWeaponSkill
        public IHttpActionResult Post([FromBody]PlayerWeaponSkill newPlayerWeaponSkill)
        {
            return Ok(PlayerWeaponSkill.AddPlayerWeaponSkill(newPlayerWeaponSkill));
        }

        // PUT: api/PlayerWeaponSkill?playerId=1&weaponTypeId=2
        public IHttpActionResult Put(int playerId, int weaponTypeId, [FromBody]PlayerWeaponSkill updatedPlayerWeaponSkill)
        {
            return Ok(PlayerWeaponSkill.UpdatePlayerWeaponSkill(updatedPlayerWeaponSkill, playerId, weaponTypeId));

        }

        // DELETE: api/PlayerWeaponSkill?playerId=1&weaponTypeId=2
        public IHttpActionResult Delete(int playerId, int weaponTypeId)
        {
            return Ok(PlayerWeaponSkill.DeletePlayerWeaponSkill(playerId, weaponTypeId));
        }
    }
}
