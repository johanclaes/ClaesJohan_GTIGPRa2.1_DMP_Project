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

        public static List<Comedian> OphalenComediansOpNaamGesorteerd()
        {
            string sqlQuery = "SELECT * FROM Comedy.Comedian";
            sqlQuery += " ORDER BY naam";

            Start();
            var result = _db.Connectie.Query<Comedian>(sqlQuery).ToList();                          // query van 1 table = Comedian
            _db.Close();

            return result;
        }

        public static List<Comedian> OphalenComediansZonderBuro()
        {
            string sqlQuery = "SELECT * FROM Comedy.Comedian";
            sqlQuery += " WHERE boekingsburoid IS NULL ";                            // als boekingsburoid = NULL dan is comedian zonder buro
            sqlQuery += " ORDER BY naam";

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

        public static List<Comedian> OphalenComediansVan1Buro( int id2)
        {
            string sqlQuery = "SELECT * FROM Comedy.Comedian";
            sqlQuery += " WHERE boekingsburoid = @id2 ";                             // het boekingsburoid komt mee met het formulier
            sqlQuery += " ORDER BY naam";

            Start();
            var result = _db.Connectie.Query<Comedian>(sqlQuery, param: new { id2 = id2 }).ToList();
            _db.Close();

            return result;
        }

        public static int OpzoekenLocatieOpBasisUsernamePassword(string sqlQuery, string username, string password)
        {
            // we geven de FK terug die verwijst naar locatie, als natuurlijk username / password gevonden wordt.
            
            Start();
            var result = _db.Connectie.Query<LocatieContact>(sqlQuery, param: new { loginName = username  }).ToList();
            _db.Close();

            int locatieIdNr = -1;
            if (result.Count > 0)
            {
                if (result.First().loginPassword == password )
                {
                    locatieIdNr = result.First().locatieId;
                }
            }
           
            return locatieIdNr;
        }


        public static List<PlayListEvent> MaakPlaylist2(string naam3)
        {
            // string sqlQuery = "SELECT EV.id, EV.naam, EV.cafeSetting, EventComedian.id, CO.id, DA.datumtijdstip  ";
            string sqlQuery = "SELECT EV.id, EV.naam, EV.cafeSetting, EventComedian.id, CO.id  ";
            sqlQuery += " FROM Comedy.Event AS EV";
            sqlQuery += " INNER JOIN Comedy.EventComedian ON EV.id = EventComedian.eventId";
            sqlQuery += " INNER JOIN Comedy.Comedian AS CO ON  EventComedian.comedianId = CO.id";
            // sqlQuery += " INNER JOIN Comedy.DatumUur AS DA ON EV.id = DA.eventId";
            sqlQuery += " WHERE  CO.naam = @naam3 ";
            // sqlQuery += " ORDER BY DA.datumTijdstip";

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
                // sql2 += " INNER JOIN Comedy.DatumUur AS DA ON EV.id = DA.eventId";
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

                    foreach (var tijdstip in result3)
                    {
                        // DateTime tijdstipEvent = tijdstip.First().DatumUurs.First().datumTijdstip;
                        DateTime tijdstipEvent = tijdstip.DatumUurs.First().datumTijdstip;

                        PlayListEvent gezelligeAvond = new PlayListEvent(naamEventje, eventLocatietje, eventGemeente, tijdstipEvent, naamVerantwoordelijke, telefoonVerantwoordelijke, (bool)comedyEvent1.cafeSetting);
                        playlist.Add(gezelligeAvond);
                    }

                    
                

                
            }

            return playlist;
        }


        public static List<Event2> ZoekEvents(string provincie1, Comedian comedian1, bool kaartjesvrij1,bool rolstoel1,bool cafesetting1)
        {

            List<Event2> zoeklijst = new List<Event2>();
            List<Event> result5 = new List<Event>();

            string comedianNaam1;

            if (comedian1 != null)                  // als er selectie is op comedian
            {
                // als comedian is geselecteerd, dan ik wil een lijst met alle eventid waar de comedian optreedt
                string sqlQuery1 = "SELECT EV.id AS Id, EV.naam, EVCO.id AS Id, CO.id   ";
                sqlQuery1 += " FROM Comedy.Event AS EV";
                sqlQuery1 += " INNER JOIN Comedy.EventComedian AS EVCO ON EV.id = EVCO.eventId";
                sqlQuery1 += " INNER JOIN Comedy.Comedian AS CO ON EVCO.comedianId = CO.id";
                sqlQuery1 += " WHERE  CO.naam = @comediannaam ";
                comedianNaam1 = comedian1.naam;

                Start();
                result5 = _db.Connectie.Query<Event, EventComedian, Comedian, Event>(sqlQuery1, (Event, EventComedian, Comedian) =>
                {
                    Event event5;
                    event5 = Event;

                    return event5;

                }
                , param: new { comediannaam = comedianNaam1 } ).ToList();
                _db.Close();

                
                
            }
            else
            {
                // indien geen comedian geselecteerd, dan een lijst van alle event id
                string sqlQuery9 = "SELECT EV.id AS Id ";
                sqlQuery9 += " FROM Comedy.Event AS EV";

                Start();
                result5 = _db.Connectie.Query<Event>(sqlQuery9).ToList();                          // query van 1 table = Event
                _db.Close();

            }

            //  %%%%%%%%%%%%%%%%%%%%%%%%%%%

            // voor elke event gaan we zoeken of deze aan de voorwaardes van location voldoet

            foreach (var item in result5)
            {
                // we checken op provincie + event-voorwaarden
                int currentId = item.id;

                string sqlQuery6 = "SELECT EV.id, EV.naam, EV.prijs, EV.cafeSetting, EVLO.id, LO.id, LO.naam, LO.gemeente   ";
                sqlQuery6 += " FROM Comedy.Event AS EV";
                sqlQuery6 += " INNER JOIN Comedy.EventLocatie AS EVLO ON EV.id = EVLO.eventId";
                sqlQuery6 += " INNER JOIN Comedy.Locatie AS LO ON EVLO.locatieId = LO.id";
                sqlQuery6 += " WHERE EV.id = @id";
                if (kaartjesvrij1)
                {
                    sqlQuery6 += " AND  EV.kaartenVrij = 'true'  ";
                }
                else
                {
                    sqlQuery6 += " AND  EV.kaartenVrij = 'false'  ";
                }
                if (rolstoel1)
                {
                    sqlQuery6 += " AND  EV.rolstoel = 'true' ";
                }
                if (cafesetting1)
                {
                    sqlQuery6 += " AND  EV.cafeSetting = 'true' ";
                }
                else
                {
                    sqlQuery6 += " AND  EV.cafeSetting = 'false' ";
                }
                if (provincie1 != null)
                {
                    sqlQuery6 += " AND  LO.provincie = @provincie ";
                }


                Start();
                var result33 = _db.Connectie.Query<Event, EventLocatie, Locatie, Event>(sqlQuery6,
                    (Event, EventLocatie, Locatie) =>
                    {
                        Locatie locatie1;
                        locatie1 = Locatie;
                        // locatie1.EventLocaties.Add(item: eventlocatie2);

                        EventLocatie eventlocatie2;
                        eventlocatie2 = EventLocatie;
                        eventlocatie2.Locatie = locatie1;

                        Event event5;
                        event5 = Event;
                        event5.EventLocaties.Add(item: eventlocatie2);

                        return event5;
                    }
             , param: new { provincie = provincie1, id = currentId }
             , splitOn: "id,id,id,id").Distinct().ToList();
                _db.Close();

                // van alle events die aan de voorwaarden voldoen, gaan we nu nog alle data en tijdstippen opvragen

                foreach (var eventGebeuren in result33)
                {
                    string sql9 = "SELECT DA.id, DA.datumTijdstip  ";
                    sql9 += " FROM Comedy.DatumUur AS DA";
                    sql9 += " INNER JOIN Comedy.Event AS EV ON EV.id = DA.eventId";
                    sql9 += " WHERE  EV.id = @id5 ";

                    int id5 = (int)eventGebeuren.id;

                    Start();
                    var lijstTijdstippen = _db.Connectie.Query<DatumUur>(sql9, param: new { id5 = id5 }).ToList();
                    _db.Close();

                    foreach (var tijdstip in lijstTijdstippen)
                    {
                        DateTime dendatum = tijdstip.datumTijdstip;
                        float prijs7 = (float)eventGebeuren.prijs;
                        string eventNaam8 = eventGebeuren.naam;
                        Event2 gezelligeAvond = new Event2(eventNaam8, dendatum, eventGebeuren.EventLocaties.First().Locatie.naam, eventGebeuren.EventLocaties.First().Locatie.gemeente, kaartjesvrij1,cafesetting1, prijs7);
                        zoeklijst.Add(gezelligeAvond);
                    }
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

        public static List<Event> MaakEventLijstOpbasisLocatieNr(int locatieNummer)
        {
            string sqlQuery = "SELECT EV.id, EV.naam, EV.cafeSetting, EV.website, EV.prijs, EV.leeftijd, EV.rolstoel, EV.kaartenVrij, DA.id, DA.datumTijdstip   ";
            sqlQuery += " FROM Comedy.Event AS EV";
            sqlQuery += " INNER JOIN Comedy.EventLocatie AS EVLO ON EV.id = EVLO.eventId";
            sqlQuery += " INNER JOIN Comedy.Locatie AS LO ON  EVLO.locatieId = LO.id";
            sqlQuery += " INNER JOIN Comedy.DatumUur AS DA ON EV.id = DA.eventId";
            sqlQuery += " WHERE  LO.id = @locatieNr ";

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

        public static List<Comedian> OphalenComediansVan1Event(string eventnaam)
        {

            string sqlQuery = "SELECT CO.id, CO.naam, CO.voornaam  ";
            sqlQuery += " FROM Comedy.Comedian AS CO";
            sqlQuery += " INNER JOIN Comedy.EventComedian AS EVCO ON CO.id = EVCO.comedianId";
            sqlQuery += " INNER JOIN Comedy.Event AS EV ON  EVCO.eventId = EV.id";
            sqlQuery += " WHERE  EV.naam = @eventnaam ";

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
