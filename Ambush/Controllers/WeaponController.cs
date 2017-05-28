using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ambush.Models;

namespace Ambush.Controllers
{
    public class WeaponController : ApiController
    {
        // GET: api/Weapon
        public IHttpActionResult Get()//done
        {         
            if(Weapon.GetAllWeapons().HasValues)
                return Ok(Weapon.GetAllWeapons());
            return NotFound();
        }

        // GET: api/Weapon?playerId=2
        public IHttpActionResult Get(int playerId)
        {
            return Ok(Weapon.GetAllWeaponsByPlayerId(playerId)) ;
        }

        // GET: api/Weapon?weaponName=snipper
        public IHttpActionResult GetByName(string weaponName)
        {
            if(Weapon.GetWeapon(weaponName).HasValues)
                return Ok(Weapon.GetWeapon(weaponName));
            return NotFound();
        }


        // POST: api/Weapon
        public IHttpActionResult Post([FromBody]Weapon newWeapon)
        {
            if(Weapon.AddWeapone(newWeapon))
            {
                return Ok(true);
            }
            return BadRequest();
        }

        // PUT: api/Weapon/5
        public void Put(int id, [FromBody]Weapon value)
        {

        }

        // DELETE: api/Weapon/5
        public IHttpActionResult Delete(string name)
        {
            if(Weapon.DeleteWeapone(name))
            {
                return Ok(true);
            }
            return BadRequest();
        }


    }
}
