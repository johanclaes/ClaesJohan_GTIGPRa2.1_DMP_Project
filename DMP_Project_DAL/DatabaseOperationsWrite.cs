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

        public bool NewEventToevoegen(NewEvent newevent2,int locationNr,DateTime eventdatetime2)
        {
            // eerst wordt er een event aangemaakt

            string sql = @"INSERT INTO Comedy.Event (naam, rolstoel, kaartenVrij, cafeSetting, prijs, website, leeftijd)
                            OUTPUT INSERTED.id
                           VALUES (@eventnaam, @eventrolstoel, @eventkaartenvrij, @eventcafesetting, @eventprijs, @eventwebsite, @eventleeftijd)";

            var parameters = new
            {
                @eventnaam = newevent2.naam,
                @eventrolstoel = newevent2.rolstoel,
                @eventkaartenvrij = newevent2.kaartenVrij,
                @eventcafesetting = newevent2.cafeSetting,
                @eventprijs = newevent2.prijs,
                @eventwebsite = newevent2.website,
                @eventleeftijd = newevent2.leeftijd,
            };

            using (IDbConnection db = new SqlConnection(ConnectionString))                  //   ***
            {
                // int identity;
                // var affectedRows = db.Execute(sql, parameters);
                var identity = db.ExecuteScalar<int>(sql, parameters);
                // return identity;
                // return identity;
                // var eventidinserted = db.Execute(sql, parameters).Single();

                // en vervolgens een eventLocatie dat het event met de eigen locatie verbindt
                string sql2 = @"INSERT INTO Comedy.EventLocatie (eventId, locatieId)
                           VALUES (@eventid, @locatieid)";
                var parameters2 = new
                {
                    @eventid = identity,
                    @locatieid = locationNr,
                };

                var affectedRows = db.Execute(sql2, parameters2);

                // en vervolgens een eventLocatie dat het event met de eigen locatie verbindt
                string sql3 = @"INSERT INTO Comedy.DatumUur (eventId, datumTijdstip)
                           VALUES (@eventid, @datumtijdstip)";
                var parameters3 = new
                {
                    @eventid = identity,
                    @datumtijdstip = eventdatetime2,
                };

                var affectedRows2 = db.Execute(sql3, parameters3);

                return true;

                
            }

            return false;
        }


        public bool ComedianToevoegenEvent(Comedian comedian2, Event event2)
        {

            //      HIER IS NOG WERK AAN

            string sql = @"INSERT INTO Comedy.EventComedian (naam, voornaam, geboortedatum)
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
