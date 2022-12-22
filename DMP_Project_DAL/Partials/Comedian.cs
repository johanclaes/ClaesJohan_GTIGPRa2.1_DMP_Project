using DMP_Project_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_DAL
{
    public partial class Comedian   
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
    }
}
