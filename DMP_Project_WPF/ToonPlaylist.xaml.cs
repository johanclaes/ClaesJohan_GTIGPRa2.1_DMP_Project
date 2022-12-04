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
    /// Interaction logic for ToonPlaylist.xaml
    /// </summary>
    public partial class ToonPlaylist : Window
    {
        public ToonPlaylist(string naam3)
        {
            InitializeComponent();

            // MessageBox.Show(naam3);                          // toon de naam van de comedian
            // string sql = "SELECT * FROM Comedy.Comedian";
            // sql += " ORDER BY naam";

            // string sql = "SELECT DISTINCT Event.naam, DatumUur.datum, LEFT(convert(varchar, DatumUur.tijdstip),5), Locatie.naam, Locatie.gemeente,LocatieContact.naam,LocatieContact.telefoonNummer";
            string sql = "SELECT EV.id, EV.naam, EV.cafeSetting, EventComedian.id, CO.id, DA.datumtijdstip  ";
            sql += " FROM Comedy.Event AS EV";
            sql += " INNER JOIN Comedy.EventComedian ON EV.id = EventComedian.eventId";
            sql += " INNER JOIN Comedy.Comedian AS CO ON  EventComedian.comedianId = CO.id";
            sql += " INNER JOIN Comedy.DatumUur AS DA ON EV.id = DA.eventId";
            sql += " WHERE  CO.naam = @naam3 ";
            // sql += " ORDER BY DA.datumTijdstip";

            lijstplaylistevents = DatabaseOperations.MaakPlaylist2(sql,naam3);         // je kan niet zomaar type1 op type2 mappen

            dataComedians.ItemsSource = lijstplaylistevents;

            lblComedianName.Content = naam3;                                    // de naam van de comedian wordt bovenaan getoond
        }

        List<Comedian> lijstcomedians = new List<Comedian>();
        List<PlayListEvent> lijstplaylistevents = new List<PlayListEvent>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
