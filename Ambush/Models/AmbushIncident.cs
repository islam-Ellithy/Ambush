using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RethinkDb.Driver;
using Newtonsoft.Json.Linq;

namespace Ambush.Models
{
    public class AmbushIncident
    {
        static RethinkDB r = RethinkDB.R;
        public int id { get; set; }
        public int AmbusherId { get; set; }
        public int AmbushedId { get; set; }
        public int Result { get; set; }
        public decimal AmbushLat { get; set; }
        public decimal AmbushLong { get; set; }
        
        public static bool AddAmbushIncident(AmbushIncident newAmbushIncident)
        {
            try
            {
                var conn = r.Connection().Connect();

                var cnt = r.Db("Ambush").Table("AmbushIncident")
                    .Count().Run<int>(conn);
                
                r.Db("Ambush").Table("AmbushIncident")
                .Insert(new AmbushIncident
                {
                    id = cnt ,
                    AmbushedId = newAmbushIncident.AmbushedId,
                    AmbusherId = newAmbushIncident.AmbusherId,
                    AmbushLat = newAmbushIncident.AmbushLat,
                    AmbushLong = newAmbushIncident.AmbushLong,
                    Result = newAmbushIncident.Result
                }).Run(conn);

                conn.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteAmbushIncident(int ambusherId , int ambushedId)
        {
            try
            {
                var conn = r.Connection().Connect();
                r.Db("Ambush").Table("AmbushIncident")
                    .Filter(r.Row("AmbusherId").Eq(ambusherId))
                    .Filter(r.Row("AmbushedId").Eq(ambushedId))
                    .Delete()
                    .Run(conn);
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static JObject GetAllAmbushIncident()
        {
            var conn = r.Connection().Connect();

            var result = r.Db("Ambush").Table("AmbushIncident")
                .Run(conn);

            conn.Close();

            JObject ambushincident = new JObject();

            foreach (var ai in result)
            {
                ambushincident = ai;
                break;
            }
            return ambushincident;
        }

        public static JObject GetAmbushIncident(int ambusherId, int ambushedId)
        {
                var conn = r.Connection().Connect();

                var result = r.Db("Ambush").Table("AmbushIncident")
                    .GetAll(r.Array(ambusherId,ambushedId))
                    .OptArg("index", "AmbushID")
                    .Run(conn);

                conn.Close();

            JObject ambushincident = new JObject();

            foreach(var ai in result)
            {
                ambushincident = ai;
                break;
            }
            return ambushincident;   
        }

    }
}