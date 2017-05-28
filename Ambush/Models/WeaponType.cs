using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RethinkDb.Driver;
using Newtonsoft.Json.Linq;

namespace Ambush.Models
{
    public class WeaponType
    {
        private static RethinkDB r = RethinkDB.R;
        public int id { get; set; }
        public string Name { get; set; }
        public int DamagePointLow { get; set; }
        public int DamagePointHigh { get; set; }
        public int AccuracyModifier { get; set; }
        public int CurrentPrice { get; set; }
        public bool IsActive { get; set; }

        public static bool AddWeaponType(WeaponType wt)
        {
            try
            {
                var conn = r.Connection().Connect();

                var cnt = r.Db("Ambush").Table("WeaponType")
                    .Count().Run<int>(conn);
                wt.id = cnt;

                r.Db("Ambush").Table("WeaponType")
                    .Insert(new WeaponType
                    {
                        id = wt.id ,
                        Name = wt.Name,
                        DamagePointLow = wt.DamagePointLow,
                        DamagePointHigh = wt.DamagePointHigh,
                        AccuracyModifier = wt.AccuracyModifier,
                        CurrentPrice = wt.CurrentPrice,
                        IsActive = wt.IsActive
                    }).Run(conn);
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool UpdateWeaponType(WeaponType wt,string name)
        {
            try
            {
                var conn = r.Connection().Connect();
                r.Db("Ambush").Table("WeaponType")
                    .GetAll(name)
                    .OptArg("index", "Name")
                    .Update(new WeaponType {
                        Name = wt.Name , 
                        AccuracyModifier = wt.AccuracyModifier ,
                        CurrentPrice = wt.CurrentPrice ,
                        DamagePointHigh = wt.DamagePointHigh ,
                        DamagePointLow = wt.DamagePointLow ,
                        IsActive = wt.IsActive
                    }).Run(conn);
                conn.Close();
                return true;

            }
            catch
            {
                return false;
            }
        }


        public static bool DeleteWeaponType(string weaponTypeName)
        {
            try
            {
                var conn = r.Connection().Connect();
                r.Db("Ambush").Table("WeaponType")
                    .GetAll(weaponTypeName)
                    .OptArg("index","Name")
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

        public static JArray GetAllWeaponType()
        {
            var conn = r.Connection().Connect();

            var result = r.Db("Ambush").Table("WeaponType").Run(conn);

            conn.Close();

            JArray weapontypelist = new JArray();

            foreach(var wt in result)
            {
                weapontypelist.Add(wt);
            }


            return weapontypelist;
        }

        public static JObject GetWeaponTypeByName(string name)
        {
            var conn = r.Connection().Connect();

            var result = r.Db("Ambush").Table("WeaponType")
                .GetAll(name)
                .OptArg("index","Name")
                .Run(conn);

            JObject w = new JObject();

            foreach(var wt in result)
            {
                w = wt;
                break;
            }
            conn.Close();
            
            return w;
        }
    }
}