﻿using Dapper;
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

        public  bool ComedianToevoegen(Comedian comedian2)      // er wordt comedian toegevoegd zonder buro
        {

            string sql = @"INSERT INTO Comedy.Comedian (naam, voornaam, geboortedatum)
                           VALUES (@comediannaam, @comedianvoornaam, @comediangeboortedatum)";

            var parameters = new
            {
                @comediannaam = comedian2.naam,
                @comedianvoornaam = comedian2.voornaam,
                @comediangeboortedatum = comedian2.geboortedatum,

            };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ComedianBuroInvullen(string comedianNaam,int buroNr)    // er wordt een link tussen comedian en boekingsbureau, FK op comedian invullen
        {
            string sql = @"UPDATE Comedy.Comedian 
                                SET boekingsburoid = @comedianburoid 
                                WHERE naam = @comediannaam  ";
            var parameters = new
            {
                @comediannaam = comedianNaam,
                @comedianburoid = buroNr,
            };
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var affectedRows = db.Execute(sql, parameters);
                if (affectedRows == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ComedianBuroVerwijderen(string comedianNaam)        // FK op comedian wordt op null gezet
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
            // 1st id opvragen van die comedian ******************

            string sql1 = @"SELECT id FROM Comedy.Comedian";
            sql1 += " WHERE naam = @comedianNaam ";

            Start();
            var result = _db.Connectie.Query<LocatieContact>(sql1, param: new { @comedianNaam = comedianNaam }).ToList();
            _db.Close();

            int comedianid = result.First().id;



            // 2de  alle eventcomedian deleten waar comedianid **********
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


            // 3de ook comedian zelf delete ****************

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

        public bool NewEventToevoegen(Event newevent2,int locationNr,DatumUur eventdatetime2)
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

            using (IDbConnection db = new SqlConnection(ConnectionString))                  
            {
                var identity = db.ExecuteScalar<int>(sql, parameters);
                
                // en vervolgens een eventLocatie dat het event met de eigen locatie verbindt
                string sql2 = @"INSERT INTO Comedy.EventLocatie (eventId, locatieId)
                           VALUES (@eventid, @locatieid)";
                var parameters2 = new
                {
                    @eventid = identity,
                    @locatieid = locationNr,
                };
                var affectedRows = db.Execute(sql2, parameters2);

                // en vervolgens een DatumUur dat het event met het tijdstip verbindt
                string sql3 = @"INSERT INTO Comedy.DatumUur (eventId, datumTijdstip)
                           VALUES (@eventid, @datumtijdstip)";
                var parameters3 = new
                {
                    @eventid = identity,
                    @datumtijdstip = eventdatetime2.datumTijdstip,
                };

                var affectedRows2 = db.Execute(sql3, parameters3);

                return true;                
            }

            // return false;
        }


        public bool ComedianToevoegenEvent(Event event2, Comedian comedian2)
        {

            //      blable-a

            string sql = @"INSERT INTO Comedy.EventComedian (eventId, comedianId)
                           VALUES (@eventId, @comedianId)";

            var parameters = new
            {
                @eventId = event2.id,
                @comedianId = comedian2.id,
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

        public bool NewVoorstellingToevoegen(Event event2, DateTime datum2)
        {
            //      blable-a

            string sql = @"INSERT INTO Comedy.DatumUur (eventId, datumTijdstip)
                           VALUES (@eventId, @datum)";

            var parameters = new
            {
                @eventId = event2.id,
                @datum = datum2,
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

        public bool EventUpdaten(Event event2, bool nogKaartjes3, float prijs3, string website3)
        {
            //      blable-a

            string sql = @"UPDATE Comedy.Event 
                                SET kaartenVrij = @kaartenVrij, prijs = @prijs, website = @website
                                WHERE id = @eventid  ";

            var parameters = new
            {
                @kaartenVrij = nogKaartjes3,
                @prijs = prijs3,
                @website = website3,
                @eventid = event2.id,
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
