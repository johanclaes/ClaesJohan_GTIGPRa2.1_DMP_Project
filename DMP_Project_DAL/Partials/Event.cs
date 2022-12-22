using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_DAL
{
    public partial class Event
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

        // public Event() { }

    }
}
