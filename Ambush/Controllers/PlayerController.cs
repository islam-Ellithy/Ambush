using Ambush.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace Ambush.Controllers
{
    public class PlayerController : ApiController
    {
        // GET: api/Player
        public IHttpActionResult GetAllPlayers()
        {
            return Ok(Player.GetAllPlayers());
        }


        //SignIn
        // GET: api/Player?userName=Islam_E&password=1234
        public IHttpActionResult Get(string userName,string password)
        {

            password = Player.Encryption(password);

            Player player = Player.GetPlayerByUserName(userName);
            if (player.Name.Equals(userName))
            {
                if (player.Password.Equals(password))
                {
                    return Ok(player);
                }
                else
                {
                    return BadRequest();
                }
            }
            return NotFound();
        }


        //GET: api/Player?Id=1
        public IHttpActionResult GetPlayerById(int Id)
        {
            Player player = Player.GetPlayerById(Id);
         
                if(player!=null)
                {
                    return Ok(player);
                }
            return NotFound();
        }



        //SignUp
        // POST: api/Player
        public IHttpActionResult Post([FromBody]Player newPlayer)
        {
            string newpassword = Player.Encryption(newPlayer.Password);
            newPlayer.Password = newpassword;
            if (Player.AddNewPlayer(newPlayer))
            {
                return Ok(Player.AddNewPlayer(newPlayer));
            }
            return BadRequest();
        }

        // PUT: api/Player?id=3
        public IHttpActionResult Put(int id, [FromBody]Player newPlayer)
        {
            if(Player.UpdatePlayer(newPlayer, id))
            {
               return Ok(Player.GetPlayerByUserName(newPlayer.Name));
            }
            return BadRequest();
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {

        }
    }
}
