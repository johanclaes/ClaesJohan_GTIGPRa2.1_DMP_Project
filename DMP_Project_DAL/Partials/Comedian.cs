using DMP_Project_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_DAL
{
    public partial class Comedian   : BasisKlasse
    {
        

        public Comedian(int id, string naam, string voornaam)
        {
            this.id = id;
            this.naam = naam;
            this.voornaam = voornaam;
        }
        public Comedian(string naam, string voornaam, DateTime geboortedatum)
        {
            this.naam = naam;
            this.voornaam = voornaam;
            this.geboortedatum = geboortedatum;
        }

        public Comedian(string naam, string voornaam)
        {
            this.naam = naam;
            this.voornaam = voornaam;
        }


        public override string ToString()
        {
            return this.voornaam + " " + this.naam;
        }

        public override string Error        // not used
        {
            get
            {
                return null;
            }
        }

        public override string this[string columnName]
        {
            get
            {
                string result = null;
                DateTime leeftijdsgrens = new DateTime(2005, 01, 01);
                switch (columnName)
                {
                    case "naam":
                        if (string.IsNullOrWhiteSpace(naam))
                        {
                            result = "Naam is een verplicht in te vullen veld!";
                        }
                        break;
                    case "voornaam":
                        if (string.IsNullOrWhiteSpace(voornaam))
                        {
                            result = "Voornaam ook nog invullen!";
                        }
                        break;
                    case "geboortedatum":
                        if (geboortedatum > leeftijdsgrens)
                        {
                            result = "Comedians zijn voor 2005 geboren!";
                        }
                        break;


                }
                return result;
            }
            
            

        }

        public override string Valideer(string propertynaam)
        {
            DateTime leeftijdsgrens = new DateTime(2005, 01, 01);
            if (propertynaam == "naam" && string.IsNullOrWhiteSpace(naam))
            {
                return "Naam is een verplicht in te vullen veld!";
            }
            else if (propertynaam == "voornaam" && string.IsNullOrWhiteSpace(voornaam))
            {
                return "Voornaam ook nog invullen!";
            }
            else if (propertynaam == "geboortedatum" && geboortedatum > leeftijdsgrens )
            {
                return "Comedians zijn voor 2005 geboren!";
            }

            return "";
        }
    }
}
