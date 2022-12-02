using Dapper;
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

            PlayListEvent gezelligeAvond = new PlayListEvent("lachen1", "werft1", "Geel", dendatum, "Paul Maes", "+32475891999");
            playlist.Add(gezelligeAvond);

            

            gezelligeAvond = new PlayListEvent("moppen_tappen", "het getouw1", "Mol", dendatum, "Maria Jansen","+3214123456");

            playlist.Add(gezelligeAvond);

            return playlist;                      
        }

        public static List<PlayListEvent> MaakPlaylist2(string sqlQuery)
        {

            List<PlayListEvent> playlist = new List<PlayListEvent>();

            var result = _db.Connectie.Query<   Event, EventComedian, Comedian, Event>(sqlQuery, (Event, EventComedian, Comedian) =>
             {
                 
                 Event event1 ;
                 event1 = Event;
                 return event1;
             }
             , splitOn: "id,id").Distinct().ToList();


            PlayListEvent gezelligeAvond = new PlayListEvent("lachen", "werft", "Geel");
            playlist.Add(gezelligeAvond);

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
