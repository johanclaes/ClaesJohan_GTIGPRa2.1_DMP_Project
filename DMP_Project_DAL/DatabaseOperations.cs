﻿using Dapper;
using DMP_Project_Models;
using System;
using System.Collections.Generic;
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
            var result = _db.Connectie.Query<Event, EventComedian, Comedian, Event>(sqlQuery, (Event, EventComedian, Comedian) =>
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

                    // DatumUur datumuur5;
                    // datumuur5 = DatumUur;

                    Event event1;
                    event1 = Event;
                    event1.EventLocaties.Add(item: eventlocatie2);
                    // event1.DatumUurs.Add(datumuur5);
                    
                    return event1;
                }
             , param: new { naam3 = naamEventje }
             , splitOn: "id,id,id").Distinct().ToList();
                _db.Close();

                string eventLocatietje = result2.First().EventLocaties.First().Locatie.naam;
                string eventGemeente = result2.First().EventLocaties.First().Locatie.gemeente;
                string naamVerantwoordelijke = result2.First().EventLocaties.First().Locatie.LocatieContacts.First().naam + " ";
                naamVerantwoordelijke += result2.First().EventLocaties.First().Locatie.LocatieContacts.First().voornaam;
                string telefoonVerantwoordelijke = result2.First().EventLocaties.First().Locatie.LocatieContacts.First().telefoonNummer;
                // DateTime tijdstipEvent = result2.First().DatumUurs.First().datumTijdstip;
                DateTime tijdstipEvent = DateTime.Parse("15/05/2022");

                PlayListEvent gezelligeAvond = new PlayListEvent(naamEventje, eventLocatietje, eventGemeente, tijdstipEvent, naamVerantwoordelijke, telefoonVerantwoordelijke, (bool)comedyEvent1.cafeSetting);
                playlist.Add(gezelligeAvond);
            }

            return playlist;
        }


        public static List<Event2> ZoekEvents(string sqlQuery)
        {

            List<Event2> zoeklijst = new List<Event2>();
            DateTime dendatum = DateTime.Parse("15/12/2022 20:15");
            Event2 gezelligeAvond = new Event2("lachen", dendatum, "werft", "Geel",  true, 16);
            zoeklijst.Add(gezelligeAvond);
            gezelligeAvond = new Event2("moppen_tappen", dendatum, "het getouw", "Mol", true, 11);
            zoeklijst.Add(gezelligeAvond);

            return zoeklijst;
        }

        public static List<Boekingsburo> ZoekBuroOpBasisNaam(string sqlQuery,string naam2)
        {
            Start();
            var result1 = _db.Connectie.Query<Boekingsburo>(sqlQuery, param: new { naam2 = naam2 }).ToList();
            _db.Close();

            return result1;
        }




    }
}
