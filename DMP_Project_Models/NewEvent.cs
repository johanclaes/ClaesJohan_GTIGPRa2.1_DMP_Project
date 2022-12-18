using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_Models
{
    public class NewEvent : BasisKlasse
    {

        public string naam { get; set; }
        public bool rolstoel { get; set; }
        public bool kaartenVrij { get; set; }
        public bool cafeSetting { get; set; }
        public float prijs { get; set; }
        public string website { get; set; }
        public string leeftijd { get; set; }

        public NewEvent(string naam, bool rolstoel, bool kaartenVrij, bool cafeSetting, float prijs, string website, string leeftijd)
        {
            this.naam = naam;
            this.rolstoel = rolstoel;
            this.kaartenVrij = kaartenVrij;
            this.cafeSetting = cafeSetting;
            this.prijs = prijs;
            this.website = website;
            this.leeftijd = leeftijd;
        }

        public NewEvent()  {}

        public override string this[string columnName] => throw new NotImplementedException();

        public override string Error => throw new NotImplementedException();

        public override string Valideer(string propertynaam)
        {
            throw new NotImplementedException();
        }
    }
}
