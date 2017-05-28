using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RethinkDb.Driver;
using Newtonsoft.Json.Linq;
using RethinkDb.Driver.Net;
using System.Text;
using System.Security.Cryptography;

namespace Ambush.Models
{
    public class Player
    {
        public static RethinkDB r = RethinkDB.R;

        public int id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsHuman { get; set; }
        public int Coin { get; set; }
        public int Point { get; set; }
        public int WeaponOfChoice { get; set; }
        public string AmbushVictoryInsult { get; set; }
        public string AmbushEscapeInsult { get; set; }
        public string AmbushCounterAttackVictoryInsult { get; set; }
        public int TotalPlay { get; set; }
        public int TotalAmbushVictory { get; set; }
        public int TotalAmbushDefeat { get; set; }
        public int TotalAmbushEscape { get; set; }
        public int TotalAmbushCounterAttackVictory { get; set; }
        public bool IsActive { get; set;}


        public static bool AddNewPlayer(Player p)
        {
            try
            {
                var conn = r.Connection().Connect();


                var result = r.Db("Ambush").Table("Player").Count().Run<int>(conn);

                conn.Close();


                conn = r.Connection().Connect();

                int cnt = result;

                p.id = cnt;

                
                r.Db("Ambush").Table("Player")
                    .Insert(new Player
                    {
                        id = p.id ,
                        Name = p.Name,
                        Password = p.Password,
                        IsHuman = p.IsHuman ,
                        Coin = p.Coin,
                        Point = p.Point ,
                        WeaponOfChoice = p.WeaponOfChoice,
                        AmbushVictoryInsult = p.AmbushVictoryInsult,
                        AmbushEscapeInsult = p.AmbushEscapeInsult,
                        AmbushCounterAttackVictoryInsult = p.AmbushCounterAttackVictoryInsult,
                        TotalPlay = p.TotalPlay,
                        TotalAmbushVictory = p.TotalPlay,
                        TotalAmbushDefeat = p.TotalAmbushDefeat,
                        TotalAmbushEscape = p.TotalAmbushEscape ,
                        TotalAmbushCounterAttackVictory = p.TotalAmbushCounterAttackVictory,
                        IsActive = p.IsActive
                    }).Run(conn);

                conn.Close();

                return true ;

            }
            catch
            {
                return false ;
            }

        }
        public static bool UpdatePlayer(Player p, int id)
        {
            try
            {
                var conn = r.Connection().Connect();
                r.Db("Ambush").Table("Player")
                    .Get(id)
                    .Update(new Player
                    {
                        id = id ,
                        Name = p.Name,
                        Password = p.Password,
                        IsHuman = p.IsHuman,
                        Coin = p.Coin,
                        Point = p.Point,
                        WeaponOfChoice = p.WeaponOfChoice,
                        AmbushVictoryInsult = p.AmbushVictoryInsult,
                        AmbushEscapeInsult = p.AmbushEscapeInsult,
                        AmbushCounterAttackVictoryInsult = p.AmbushCounterAttackVictoryInsult,
                        TotalPlay = p.TotalPlay,
                        TotalAmbushVictory = p.TotalAmbushVictory,
                        TotalAmbushDefeat = p.TotalAmbushDefeat,
                        TotalAmbushEscape = p.TotalAmbushEscape,
                        TotalAmbushCounterAttackVictory = p.TotalAmbushCounterAttackVictory,
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

        public static JArray GetAllPlayers()
        {
            var conn = r.Connection().Connect();
            var result = r.Db("Ambush").Table("Player").Run(conn);
            conn.Close();

            JArray listOfPlayers = new JArray();

            foreach(var P in result)
            {
                listOfPlayers.Add(P);
            }

            return listOfPlayers;
        }

        public static string Encryption(string password)
        {
            //encrypt the password
            UnicodeEncoding uEncode = new UnicodeEncoding();

            byte[] bytPassword = uEncode.GetBytes(password);

            SHA512 sha = SHA512.Create();

            byte[] hash = sha.ComputeHash(bytPassword);

            password = Convert.ToBase64String(hash);

            return password;
        }
 

        public static Player GetPlayerByUserName(string userName)
        {
            var conn = r.Connection().Connect();

            var result = r.Db("Ambush").Table("Player")
                .GetAll(userName)
                .OptArg("index", "Name")
                .Run<Player>(conn);

            Player player = new Player();

            foreach (var p in result)
            {
                    player = p ;
                    break;
            }
            return player ; 
        }

        public static Player GetPlayerById(int id)
        {
            var conn = r.Connection().Connect();

            Player result = r.Db("Ambush").Table("Player")
                          .Get(id)
                          .Run<Player>(conn);
            conn.Close();
          
            return result;
        }
    }
}