using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RethinkDb.Driver;
using Newtonsoft.Json.Linq;

namespace Ambush.Models
{
    public class Weapon
    {
        public static RethinkDB r = RethinkDB.R;
        public int id { get; set; }
        public int WeaponTypeId { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int DamagePointLow { get; set; }
        public int DamagePointHigh { get; set; }
        public int AccuracyModifier { get; set; }
        public bool IsActive { get; set; }

        public static bool AddWeapone(Weapon newWeapon)
        {
            try
            {

                var conn = r.Connection().Connect();

                var cnt = r.Db("Ambush").Table("Weapon")
                          .Count().Run<int>(conn);

                conn.Close();

                newWeapon.id = cnt;


                conn = r.Connection().Connect();

                    r.Db("Ambush").Table("Weapon").Insert(new Weapon
                    {
                        id = newWeapon.id ,
                        WeaponTypeId = newWeapon.WeaponTypeId ,
                        PlayerId = newWeapon.PlayerId,
                        Name = newWeapon.Name,
                        DamagePointLow = newWeapon.DamagePointLow ,
                        DamagePointHigh = newWeapon.DamagePointHigh,
                        AccuracyModifier = newWeapon.AccuracyModifier,
                        IsActive = true
                    }).Run(conn);

                conn.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteWeapone(string weaponName)
        {
            try
            {
                var conn = r.Connection().Connect();
                r.Db("Ambush")
                    .Table("Weapon")
                    .GetAll(weaponName)
                    .OptArg("index", "Name")
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

        public static JArray GetAllWeapons()
        {
            var conn = r.Connection().Connect();
            var result = r.Db("Ambush").Table("Weapon").Run(conn);

            JArray list = new JArray();
            foreach (JObject u in result)
            {
                list.Add(u);
            }
            return list;
        }

        public static JArray GetAllWeaponsByPlayerId(int playerId)
        {
            var conn = r.Connection().Connect();

            var result = r.Db("Ambush")
                .Table("Weapon")
                .GetAll(playerId)
                .OptArg("index", "PlayerId")
                .Run(conn);

            conn.Close();

            JArray listofweapon = new JArray();
            foreach(var w in result)
            {
                listofweapon.Add(w);
            }
            return listofweapon;
        }

        public static JObject GetWeapon(string weaponName)//done 
        {
            var conn = r.Connection().Connect();

            var result = r.Db("Ambush").Table("Weapon").Filter(w => w["Name"].Eq(weaponName)).Run(conn);

            conn.Close();

            JObject weapon = new JObject();

            foreach(var w in result)
            {
                weapon = w;
                break;
            }

            return weapon;
        }
    }
}