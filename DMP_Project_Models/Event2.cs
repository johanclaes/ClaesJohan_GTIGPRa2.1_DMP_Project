using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_Models
{
    public class Event2                     // deze event wordt gebruikt bij het zoeken naar events
    {
        public string EventNaam { get; set; }

        public DateTime DatumTijdstip { get; set; }

        public string LocatieNaam { get; set; }

        public string LocatieGemeente { get; set; }

        public bool KaartjesVrij { get; set; }

        public bool Cafesetting { get; set; }
        public float Prijs { get; set; }

        public Event2(string eventNaam, DateTime datumTijdstip, string locatieNaam, string locatieGemeente, bool kaartjesVrij,bool cafesetting, float prijs)
        {
            EventNaam = eventNaam;
            DatumTijdstip = datumTijdstip;
            LocatieNaam = locatieNaam;
            LocatieGemeente = locatieGemeente;
            KaartjesVrij = kaartjesVrij;
            Cafesetting = cafesetting;
            Prijs = prijs;
        }
    }
}
