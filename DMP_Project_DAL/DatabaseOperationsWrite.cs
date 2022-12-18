using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMP_Project_Models;

namespace DMP_Project_DAL
{
    public class DatabaseOperationsWrite : BaseRepository

    {
        private static DatabaseConnectie _db;

        private static void Start()
        {
            _db = new DatabaseConnectie();
            _db.Open();
        }

        public  bool ComedianToevoegen(Comedian2 comedian2)
        {

            string sql = @"INSERT INTO Comedy.Comedian (naam, voornaam, geboortedatum)
                           VALUES (@comediannaam, @comedianvoornaam, @comediangeboortedatum)";

            var parameters = new
            {
                @comediannaam = comedian2.naam,
                @comedianvoornaam = comedian2.voornaam,
                @comediangeboortedatum = comedian2.geboortedatum,

            };

            using (IDbConnection db = new SqlConnection(ConnectionString))                  //   ***
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ComedianBuroInvullen(string comedianNaam,int buroNr)
        {
            string sql = @"UPDATE Comedy.Comedian 
                                SET boekingsburoid = @comedianburoid 
                                WHERE naam = @comediannaam  ";
            var parameters = new
            {
                @comediannaam = comedianNaam,
                @comedianburoid = buroNr,
            };
            using (IDbConnection db = new SqlConnection(ConnectionString))                  //   ***
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ComedianBuroVerwijderen(string comedianNaam)
        {
            string sql = @"UPDATE Comedy.Comedian 
                                SET boekingsburoid = NULL 
                                WHERE naam = @comediannaam  ";
            var parameters = new
            {
                @comediannaam = comedianNaam,
            };
            using (IDbConnection db = new SqlConnection(ConnectionString))                  //   ***
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ComedianStoptErmee(string comedianNaam)
        {
            // 1st id opvragen van die comedian     **************************************************

            string sql1 = "SELECT id FROM Comedy.Comedian";
            sql1 += " WHERE naam == @comedianNaam ";

            var parameters1 = new
            {
                @comediannaam = comedianNaam
            };

            int comedianid = 55555;



            // 2de  alle eventcomedian deleten waar comedianid 
            string sql2 = @"DELETE FROM Comedy.EventComedian
                           WHERE comedianid = @comedianid";

            var parameters2 = new
            {
                @comedianid = comedianid
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql2, parameters2);
            }


            // 3de ook comedian zelf delete

            string sql3 = @"DELETE FROM Comedy.Comedian
                           WHERE naam = @comediannaam";

            var parameters3 = new
            {
                @comediannaam = comedianNaam
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql3, parameters3);
                if (affectedRows >= 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool NewEventToevoegen(Comedian2 comedian2)
        {

            string sql = @"INSERT INTO Comedy.Comedian (naam, voornaam, geboortedatum)
                           VALUES (@comediannaam, @comedianvoornaam, @comediangeboortedatum)";

            var parameters = new
            {
                @comediannaam = comedian2.naam,
                @comedianvoornaam = comedian2.voornaam,
                @comediangeboortedatum = comedian2.geboortedatum,

            };

            using (IDbConnection db = new SqlConnection(ConnectionString))                  //   ***
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
