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

                foreach (var item in this.GetType().GetProperties())    //reflection ; hij gaat alle properties van de klasse af
                {
                    if (item.CanRead)
                    {
                        string fout = this[item.Name];
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
