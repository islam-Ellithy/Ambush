using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RethinkDb.Driver;
using Newtonsoft.Json.Linq;

namespace Ambush.Models
{
    public class PlayerWeaponSkill
    {
        public static RethinkDB r = RethinkDB.R;
        public int id { get; set; }
        public int PlayerId { get; set; }
        public int WeaponTypeId { get; set; }
        public int Skill { get; set; }
        public bool IsActive { get; set; }

        public static bool AddPlayerWeaponSkill(PlayerWeaponSkill p)
        {
            try
            {
                var conn = r.Connection().Connect();
                var cnt = r.Db("Ambush").Table("PlayerWeaponSkill")
                    .Count().Run<int>(conn);

                p.id = cnt;

                r.Db("Ambush").Table("PlayerWeaponSkill")
                    .Insert(new PlayerWeaponSkill
                    {
                        id = p.id ,
                        PlayerId = p.PlayerId,
                        WeaponTypeId = p.WeaponTypeId,
                        Skill = p.Skill,
                        IsActive = p.IsActive
                    }).Run(conn);

                conn.Close();
                return true; 
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdatePlayerWeaponSkill(PlayerWeaponSkill w, int playerId , int weaponTypeId)
        {
            try
            {
                var conn = r.Connection().Connect();
                r.Db("Ambush").Table("PlayerWeaponSkill")
                    .Filter(r.Row("PlayerId").Eq(playerId))
                    .Filter(r.Row("WeaponTypeId").Eq(weaponTypeId))
                    .Update(new PlayerWeaponSkill
                    {
                        PlayerId = w.PlayerId,
                        WeaponTypeId = w.WeaponTypeId,
                        IsActive = w.IsActive,
                        Skill = w.Skill
                    }).Run(conn);
                conn.Close();
                return true; 
            }
            catch
            {
                return false;
            }
        }

        public static bool DeletePlayerWeaponSkill(int playerId,int weaponTypeId)
        {
            try
            {

                var conn = r.Connection().Connect();
                r.Db("Ambush").Table("PlayerWeaponSkill")
                    .GetAll(r.Array(playerId,weaponTypeId))
                    .OptArg("index", "PlayerWeapon")
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



        public static JObject GetPlayerWeaponSkill(int playerId, int weaponTypeId)
        {
            var conn = r.Connection().Connect();
            var result = r.Db("Ambush").Table("PlayerWeaponSkill")
                            .GetAll(r.Array(playerId, weaponTypeId))
                            .OptArg("index", "PlayerWeapon")
                            .Run(conn);


            JObject pw = new JObject();

            foreach(var pws in result)
            {
                pw = pws;
                break;
            }

            return pw ;
        }

        public static JArray GetPlayerWeaponSkills()
        {
            var conn = r.Connection().Connect();
            var result = r.Db("Ambush")
                          .Table("PlayerWeaponSkill")
                          .Run(conn);

            conn.Close();

            JArray list = new JArray();

            foreach (var pws in result)
            {
                list.Add(pws);
            }

            return list;
        }


    }
}