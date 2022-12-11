using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_Models
{
    public class Comedian2
    {
        public string naam { get; set; }
        public string voornaam  { get; set; }
        public DateTime geboortedatum { get; set; }

        public Comedian2(string naam, string voornaam, DateTime geboortedatum)
        {
            this.naam = naam;
            this.voornaam = voornaam;
            this.geboortedatum = geboortedatum;
        }

        public Comedian2(string naam, string voornaam)
        {
            this.naam = naam;
            this.voornaam = voornaam;
        }

        public override string ToString()
        {
            return voornaam + " " + naam;
        }
    }
}
