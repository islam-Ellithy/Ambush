using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ambush.Models;

namespace Ambush.Controllers
{
    public class AmbushIncidentController : ApiController
    {


        // GET: api/AmbushIncident
        public IHttpActionResult Get()
        {
            if(AmbushIncident.GetAllAmbushIncident().HasValues)
                return Ok(AmbushIncident.GetAllAmbushIncident()) ;
            return NotFound();
        }

        // GET: api/AmbushIncident?ambusherId=2&ambushedId=3
        public IHttpActionResult Get(int ambusherId, int ambushedId)
        {
            if(AmbushIncident.GetAmbushIncident(ambusherId, ambushedId).HasValues)
                return Ok(AmbushIncident.GetAmbushIncident(ambusherId,ambushedId)) ;
            return NotFound();
        }

        // POST: api/AmbushIncident
        public IHttpActionResult Post([FromBody]AmbushIncident value)
        {
               return Ok(AmbushIncident.AddAmbushIncident(value));
        }

        // PUT: api/AmbushIncident
        public void Put(int id, [FromBody]AmbushIncident value)
        {

        }

        // DELETE: api/AmbushIncident/5
        public IHttpActionResult Delete(int ambusherId,int ambushedId)
        {
            return Ok(AmbushIncident.DeleteAmbushIncident(ambusherId, ambushedId));
        }
    }
}
