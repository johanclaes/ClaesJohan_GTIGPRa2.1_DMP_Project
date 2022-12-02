using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_Models
{
    public class PlayListEvent
    {
        public string EventNaam { get; set; }
        public string LocatieNaam { get; set; }

        public string LocatieGemeente { get; set; }

        public DateTime Tijdstip { get; set; }

        public string LocatieContact { get; set; }

        public string ContactTelefoon { get; set; }

        public PlayListEvent(string eventNaam, string locatieNaam, string locatieGemeente)
        {
            EventNaam = eventNaam;
            LocatieNaam = locatieNaam;
            LocatieGemeente = locatieGemeente;
        }

        public PlayListEvent(string eventNaam, string locatieNaam, string locatieGemeente, DateTime tijdstip, string locatieContact, string contactTelefoon) : this(eventNaam, locatieNaam, locatieGemeente)
        {
            EventNaam = eventNaam;
            LocatieNaam = locatieNaam;
            LocatieGemeente = locatieGemeente;
            Tijdstip = tijdstip;
            LocatieContact = locatieContact;
            ContactTelefoon = contactTelefoon;
        }
    }
}
