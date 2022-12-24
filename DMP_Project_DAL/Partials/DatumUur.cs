using DMP_Project_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP_Project_DAL
{
    public partial class DatumUur : BasisKlasse
    {
        public DatumUur(DateTime datumTijdstip)
        {
            this.datumTijdstip = datumTijdstip;
        }

        public DatumUur()
        {
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
                DateTime volgendeWeekDag = DateTime.Today.AddDays(7);   // 1 week op voorhand inplannen
                switch (columnName)
                {
                    case "naam":
                        if (datumTijdstip > volgendeWeekDag)
                        {
                            result = "Enkel datums een week VOOR het event graag!";
                        }
                        break;
                }
                return result;
            }

        }



    }
}
