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
        public DateTime datumUren { get; set; }                     // extra property om table DatumUur (geneste property in te mappen)

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

    }
}
