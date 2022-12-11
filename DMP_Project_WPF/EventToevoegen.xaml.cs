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
    /// Interaction logic for EventToevoegen.xaml
    /// </summary>
    public partial class EventToevoegen : Window
    {
        public EventToevoegen(int locatieNr)
        {
            InitializeComponent();
            VulListboxEventsUwLocatie(locatieNr);
        }

        List<Comedian> lijstcomedians = new List<Comedian>();
        List<Event> lijstevents = new List<Event>();
        List<string> stringlijstcomedians = new List<string>();

        private void VoegEventToe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNUpdateEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNComedianToevoegen_Click(object sender, RoutedEventArgs e)
        {

        }



        private void VulComboboxComedians()
        {
            // een lijst met alle comedians wordt opgehaald en in de combobox getoond.
            List<Comedian2> lijstcomedians2 = new List<Comedian2>();
            string sql = "SELECT * FROM Comedy.Comedian";
            sql += " ORDER BY naam";

            lijstcomedians = DatabaseOperations.OphalenComediansOpNaamGesorteerd(sql);
            //  de klasse comedian bevat geen ToString, dus lijstComedian copiëren naar lijstComedian2
            foreach (Comedian comedian1 in lijstcomedians)
            {
                Comedian2 comedian2 = new Comedian2(comedian1.naam, comedian1.voornaam);
                lijstcomedians2.Add(comedian2);
            }
        cmbComedians.ItemsSource = lijstcomedians2;
        }

        private void VulListboxEventsUwLocatie(int locatieNr2)
        {
            // in de listbox gaan we alle events tonen die verwijzen naar de locatie waar dit locatiecontact voor verantwoordelijk is

            string sql = "SELECT EV.id, EV.naam, EV.cafeSetting, Ev.website, EV.prijs, EV.leeftijd, EV.rolstoel, EV.kaartenVrij  ";
            sql += " FROM Comedy.Event AS EV";
            sql += " INNER JOIN Comedy.EventLocatie AS EVLO ON EV.id = EVLO.eventId";
            sql += " INNER JOIN Comedy.Locatie AS LO ON  EVLO.locatieId = LO.id";
            sql += " WHERE  LO.id = @locatieNr ";

            lijstevents = DatabaseOperations.MaakEventLijstOpbasisLocatieNr(sql, locatieNr2);

            
            datagridEvents.ItemsSource = lijstevents;

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulComboboxComedians();
            // VulListboxEventsUwLocatie(locatieNr);
        }

        private void DatagridEvents_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("hello");
        }
    }
}
