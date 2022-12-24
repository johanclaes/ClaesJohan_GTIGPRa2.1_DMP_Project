using DMP_Project_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_DAL
{
    public partial class Event : BasisKlasse
    {


        public Event(string naam, bool rolstoel, bool kaartenVrij, bool cafeSetting, float prijs, string website, string leeftijd)
        {
            this.naam = naam;
            this.rolstoel = rolstoel;
            this.kaartenVrij = kaartenVrij;
            this.cafeSetting = cafeSetting;
            this.prijs = (decimal?)prijs;
            this.website = website;
            this.leeftijd = leeftijd;
        }

        public override string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "naam":
                        if (string.IsNullOrWhiteSpace(naam))
                        {
                            result = "Event-Naam is een verplicht veld!";
                        }
                        break;
                    case "leeftijd":
                        if (string.IsNullOrWhiteSpace(leeftijd))
                        {
                            result = "Wat is de leeftijd-indicatie ?";
                        }
                        break;
                    case "prijs":
                        if (0 >= prijs)
                        {
                            result = "Hoeveel kost het ?";
                        }
                        break;

                }
                return result;
            }



        }

        public override string Error        // not used
        {
            get
            {
                return null;
            }
        }

        

        public override string ToString()
        {
            return base.ToString();
        }

        public override string Valideer(string propertynaam)
        {
            if (propertynaam == "naam" && string.IsNullOrWhiteSpace(naam))
            {
                return "Event-naam is een verplicht in te vullen veld!";
            }
            else if (propertynaam == "website" && string.IsNullOrWhiteSpace(website))
            {
                return "Wat is de website ?";
            }
            else if (propertynaam == "prijs" && prijs < 0)
            {
                return "Prijs moet groter of gelijk dan 0 zijn!";
            }
            else if (propertynaam == "leeftijd" && string.IsNullOrWhiteSpace(leeftijd))
            {
                return "Gelieve de leeftijdsgrens aan te geven !";
            }

            return "";
        }

    }
}
