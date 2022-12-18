using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_Models
{
    public abstract class BasisKlasse : IDataErrorInfo
    {
        public abstract string Valideer(string propertynaam);

        public bool IsGeldig()
        {
            return string.IsNullOrWhiteSpace(Foutmeldingen);            // hier wordt er naar foutmeldingen verwezen.

        }
        public string Foutmeldingen
        {
            get
            {
                string foutmeldingen = "";

                foreach (var item in this.GetType().GetProperties()) //reflection : voor alle properties gelinkt aan die klasse
                {
                    if (item.CanRead)                               // als er bij die property een getter bij staat.
                    {
                        string fout = Valideer(item.Name);
                        if (!string.IsNullOrWhiteSpace(fout))
                        {
                            foutmeldingen += fout + Environment.NewLine;
                        }
                    }
                }
                return foutmeldingen;
            }
        }

        public abstract string Error { get; }

        public abstract string this[string columnName] { get; }
    }
}
