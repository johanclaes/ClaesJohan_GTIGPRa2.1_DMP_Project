using Dapper;
using DMP_Project_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DMP_Project_DAL
{
    public static class DatabaseOperations 
    {
        private static DatabaseConnectie _db;

        private static void Start()
        {
            _db = new DatabaseConnectie();
            _db.Open();
        }

        public static List<Comedian> OphalenComediansOpNaamGesorteerd(string sqlQuery)
        {
            Start();
            var result = _db.Connectie.Query<Comedian>(sqlQuery).ToList();                          // query van 1 table = Comedian
            _db.Close();

            return result;
        }

        public static List<Comedian> ZoekComedianOpBasisGeboortedatum(string sqlQuery, DateTime geboortedatum2)
        {
            Start();
            var result = _db.Connectie.Query<Comedian>(sqlQuery, param: new { geboortedatum2 = geboortedatum2 }).ToList();
            _db.Close();

            return result;
        }

        public static List<Comedian> OphalenComediansVan1Buro(string sqlQuery, int id2)
        {
            Start();
            var result = _db.Connectie.Query<Comedian>(sqlQuery, param: new { id2 = id2 }).ToList();
            _db.Close();

            return result;
        }

        public static int OpzoekenLocatieOpBasisUsernamePassword(string sqlQuery, string username, string password)
        {
            // we geven de FK terug die verwijst naar locatie
            
            Start();
            var result = _db.Connectie.Query<LocatieContact>(sqlQuery, param: new { loginName = username  }).ToList();
            _db.Close();

            int locatieIdNr = -1;
            if (result.Count > 0)
            {
                locatieIdNr = result.First().locatieId;
            }
            
            

            return locatieIdNr;
        }

        public static List<PlayListEvent> MaakPlaylist(string sqlQuery)
        {

            List<PlayListEvent> playlist = new List<PlayListEvent>();
            DateTime dendatum = DateTime.Parse("15/12/2022 20:15");
            
            PlayListEvent gezelligeAvond = new PlayListEvent("lachen1", "werft1", "Geel", dendatum, "Paul Maes", "+32475891999",true);
            playlist.Add(gezelligeAvond);
            gezelligeAvond = new PlayListEvent("moppen_tappen", "het getouw1", "Mol", dendatum, "Maria Jansen","+3214123456",false);
            playlist.Add(gezelligeAvond);

            return playlist;                      
        }

        public static List<PlayListEvent> MaakPlaylist2(string sqlQuery,string naam3)
        {

            List<PlayListEvent> playlist = new List<PlayListEvent>();

            Start();
            var result = _db.Connectie.Query<Event, EventComedian, Comedian,  Event>(sqlQuery, (Event, EventComedian, Comedian) =>
            {
                Event event1;
                event1 = Event;
                return event1;
            }
             , param: new { naam3 = naam3 }
             , splitOn: "id,id").Distinct().ToList();
            _db.Close();


            foreach (var comedyEvent1 in result)
            {
                string naamEventje = comedyEvent1.naam;

                string sql2 = "SELECT EV.id, EV.naam, EVLO.id, LO.id, LO.naam, LO.gemeente, LOCO.id, LOCO.naam, LOCO.voornaam, LOCO.telefoonNummer  ";
                sql2 += " FROM Comedy.Event AS EV";
                sql2 += " INNER JOIN Comedy.DatumUur AS DA ON EV.id = DA.eventId";
                sql2 += " INNER JOIN Comedy.EventLocatie AS EVLO ON EV.id = EVLO.eventId";
                sql2 += " INNER JOIN Comedy.Locatie AS LO ON  EVLO.locatieId = LO.id";
                sql2 += " INNER JOIN Comedy.LocatieContact AS LOCO ON LO.id = LOCO.locatieId";
                sql2 += " WHERE  EV.naam = @naam3 ";

                Start();
                var result2 = _db.Connectie.Query<Event, EventLocatie, Locatie, LocatieContact, Event>(sql2, (Event, EventLocatie, Locatie, LocatieContact) =>
                {
                    LocatieContact locatiecontact4;
                    locatiecontact4 = LocatieContact;

                    Locatie locatie3;
                    locatie3 = Locatie;
                    locatie3.LocatieContacts.Add(item: locatiecontact4);

                    EventLocatie eventlocatie2;
                    eventlocatie2 = EventLocatie;
                    eventlocatie2.Locatie = locatie3;

                    Event event1;
                    event1 = Event;
                    event1.EventLocaties.Add(item: eventlocatie2);
                    
                    return event1;
                }
             , param: new { naam3 = naamEventje }
             , splitOn: "id,id,id").Distinct().ToList();
                _db.Close();

                string eventLocatietje = result2.First().EventLocaties.First().Locatie.naam;
                string eventGemeente = result2.First().EventLocaties.First().Locatie.gemeente;
                string naamVerantwoordelijke = result2.First().EventLocaties.First().Locatie.LocatieContacts.First().voornaam + " ";
                naamVerantwoordelijke += result2.First().EventLocaties.First().Locatie.LocatieContacts.First().naam;
                string telefoonVerantwoordelijke = result2.First().EventLocaties.First().Locatie.LocatieContacts.First().telefoonNummer;

                string sql3 = "SELECT EV.id, DA.id, DA.datumTijdstip  ";
                sql3 += " FROM Comedy.Event AS EV";
                sql3 += " INNER JOIN Comedy.DatumUur AS DA ON EV.id = DA.eventId";
                sql3 += " WHERE  EV.naam = @naam3 ";

                Start();
                var result3 = _db.Connectie.Query<Event, DatumUur, Event>(sql3, (Event, DatumUur) =>
                {
                    DatumUur datumuur5;
                    datumuur5 = DatumUur;

                    Event event1;
                    event1 = Event;
                    event1.DatumUurs.Add(datumuur5);

                    return event1;
                }
             , param: new { naam3 = naamEventje }
             , splitOn: "id,id,id").Distinct().ToList();
                _db.Close();


                DateTime tijdstipEvent = result3.First().DatumUurs.First().datumTijdstip;
                // DateTime tijdstipEvent = DateTime.Parse("15/05/2022");

                PlayListEvent gezelligeAvond = new PlayListEvent(naamEventje, eventLocatietje, eventGemeente, tijdstipEvent, naamVerantwoordelijke, telefoonVerantwoordelijke, (bool)comedyEvent1.cafeSetting);
                playlist.Add(gezelligeAvond);
            }

            return playlist;
        }


        public static List<Event2> ZoekEvents2(string sqlQuery,string provincie3)
        {

            List<Event2> zoeklijst = new List<Event2>();

            Start();
            var result3 = _db.Connectie.Query<Locatie, EventLocatie, Event, EventComedian, Comedian, Locatie>(sqlQuery, 
                (Locatie, EventLocatie, Event, EventComedian, Comedian) =>
            {
                Event event5;
                event5 = Event;

                EventLocatie eventlocatie2;
                eventlocatie2 = EventLocatie;
                eventlocatie2.Event = event5;

                Locatie locatie1;
                locatie1 = Locatie;
                locatie1.EventLocaties.Add(item: eventlocatie2);

                return locatie1;
            }
         , param: new { provincie = provincie3 }
         , splitOn: "id,id,id,id").Distinct().ToList();
            _db.Close();

            // 55555
            
            foreach (var comedyPlaats in result3)
            {
                string sql9 = "SELECT DA.id, DA.datumTijdstip  ";
                sql9 += " FROM Comedy.DatumUur AS DA";
                sql9 += " INNER JOIN Comedy.Event AS EV ON EV.id = DA.eventId";
                sql9 += " WHERE  EV.id = @id5 ";

                int id5 = (int)comedyPlaats.EventLocaties.First().Event.id;

                Start();
                var lijstTijdstippen = _db.Connectie.Query<DatumUur>(sql9, param: new { id5 = id5 }).ToList();
                _db.Close();

                foreach (var tijdstip in lijstTijdstippen)
                {
                    DateTime dendatum = tijdstip.datumTijdstip;
                    float prijs7;
                    if (comedyPlaats.EventLocaties.First().Event.prijs == null)
                    {
                        prijs7 = -1;
                    }
                    else
                    {
                        prijs7 = (float)comedyPlaats.EventLocaties.First().Event.prijs;
                    }
                    string eventNaam8 = comedyPlaats.EventLocaties.First().Event.naam;
                    Event2 gezelligeAvond = new Event2(eventNaam8, dendatum, comedyPlaats.naam, comedyPlaats.gemeente, true, prijs7);
                    zoeklijst.Add(gezelligeAvond);
                }

                
            }

            return zoeklijst;
        }

        public static List<Boekingsburo> ZoekBuroOpBasisNaam(string sqlQuery,string naam2)
        {
            Start();
            var result1 = _db.Connectie.Query<Boekingsburo>(sqlQuery, param: new { naam2 = naam2 }).ToList();
            _db.Close();

            return result1;
        }

        public static List<Event> MaakEventLijstOpbasisLocatieNr(string sqlQuery,int locatieNummer)
        {
            Start();
            var result7 = _db.Connectie.Query<Event, DatumUur, Event>(sqlQuery,
                (Event, DatumUur) =>
                {
                    Event event1;
                    event1 = Event;

                    DatumUur datumuur2;
                    datumuur2 = DatumUur;
                    datumuur2.Event = event1;
                    
                    event1.DatumUurs.Add(item: datumuur2);
                    
                    return event1;
                }
         , param: new { locatieNr = locatieNummer }
         , splitOn: "id").Distinct().ToList();
            _db.Close();


            return result7;
        }

        public static List<Comedian> OphalenComediansVan1Event(string sqlQuery, string eventnaam)
        {

            Start();
            var result2 = _db.Connectie.Query<Comedian>(sqlQuery, param: new { eventnaam = eventnaam }).ToList();
            _db.Close();


            return result2;
        }

        public static List<DatumUur> GeefDatumVanEvent(string sqlQuery, string eventnaam)
        {

            Start();
            var result2 = _db.Connectie.Query<DatumUur>(sqlQuery, param: new { eventnaam = eventnaam }).ToList();
            _db.Close();


            return result2;
        }


    }
}
