using DMP_Project_DAL;
using DMP_Project_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DMP_Project_WPF
{
    /// <summary>
    /// Interaction logic for ZoekEvent.xaml
    /// </summary>
    public partial class ZoekEvent : Window
    {
        public ZoekEvent()
        {
            InitializeComponent();
        }

        List<Comedian> lijstcomedians = new List<Comedian>();
        List<string> stringlijstcomedians = new List<string>();
        List<string> provincies = new List<string>() { "antwerpen", "brabant", "limburg", "oost-vlaanderen", "west-vlaanderen" };
        List<string> maanden = new List<string>() { "januari", "februari", "maart", "april", "mei", "juni", "juli", "augustus", "september", "oktober", "november", "december" };
        List<Event2> lijstevents = new List<Event2>();

        private void BTNZoek_Click(object sender, RoutedEventArgs e)
        {

            string provincie1 = (string)cmbProvincie.SelectedItem;
            string comedian1 = (string)cmbComedian.SelectedItem;
            string maand1 = (string)cmbMaand.SelectedItem;
            bool kaartjesvrij1 = (bool)tgKaartenvrij.IsChecked;
            bool rolstoel1 = (bool)tgRolstoel.IsChecked;
            bool cafesetting1 = (bool)tgCafesetting.IsChecked;

            string sql = "SELECT LO.id, LO.naam, LO.gemeente, EVLO.id, EV.id, EV.naam, EV.prijs, EVCO.id, CO.id , EV.naam, EV.cafeSetting, LO.provincie, LO.adres ";
            sql += " FROM Comedy.Locatie AS LO";
            sql += " INNER JOIN Comedy.EventLocatie AS EVLO ON LO.id = EVLO.locatieId";
            sql += " INNER JOIN Comedy.Event AS EV ON  EVLO.eventId = EV.id";
            sql += " INNER JOIN Comedy.EventComedian AS EVCO ON EV.id = EVCO.eventId";
            sql += " INNER JOIN Comedy.Comedian AS CO ON EVCO.comedianId = CO.id";
            // sql += " WHERE  LO.provincie = @provincie AND CO.naam = 'Williams' ";
            if (kaartjesvrij1)
            {
                sql += " WHERE  EV.kaartenVrij = 'true'  ";
            }
            else
            {
                sql += " WHERE  EV.kaartenVrij = 'false' ";
            }
            if (rolstoel1)
            {
                sql += " AND  EV.rolstoel = 'true' ";
            }
            if (cafesetting1)
            {
                sql += " AND  EV.cafeSetting = 'true' ";
            }
            if (provincie1 != "")
            {
                sql += " AND  LO.provincie = @provincie " ;
            }
            if (!(comedian1 is null))
            {

            }
            if (!(maand1 is null))
            {

            }


            lijstevents = DatabaseOperations.ZoekEvents2(sql,provincie1);         

                   

            dataEvents.ItemsSource = lijstevents;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Comedian> lijstcomedians = new List<Comedian>();
            string sql = "SELECT * FROM Comedy.Comedian";
            sql += " ORDER BY naam";

            lijstcomedians = DatabaseOperations.OphalenComediansOpNaamGesorteerd(sql);
            foreach (Comedian comedian1 in lijstcomedians)
            {
                stringlijstcomedians.Add(comedian1.voornaam + " " + comedian1.naam);
            }
            cmbComedian.ItemsSource = stringlijstcomedians;

            cmbMaand.ItemsSource = maanden;

            cmbProvincie.ItemsSource = provincies;

        }
    }
}
