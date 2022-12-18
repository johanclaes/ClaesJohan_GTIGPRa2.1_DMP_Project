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
        List<Comedian> lijsteventComedians = new List<Comedian>();
        List<DatumUur> lijstdatums = new List<DatumUur>();

        private void VoegEventToe_Click(object sender, RoutedEventArgs e)
        {
            // er wordt dus eerst een event aangemaakt, en via de knoppen comedian toevoegen en datum toevoegen 
            // maak je het event compleet

        }

        private void btnDatum_UurToevoegen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNComedianToevoegen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNUpdateEvent_Click(object sender, RoutedEventArgs e)
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

            string sql = "SELECT EV.id, EV.naam, EV.cafeSetting, Ev.website, EV.prijs, EV.leeftijd, EV.rolstoel, EV.kaartenVrij, DA.id, DA.datumTijdstip   ";
            sql += " FROM Comedy.Event AS EV";
            sql += " INNER JOIN Comedy.EventLocatie AS EVLO ON EV.id = EVLO.eventId";
            sql += " INNER JOIN Comedy.Locatie AS LO ON  EVLO.locatieId = LO.id";
            sql += " INNER JOIN Comedy.DatumUur AS DA ON EV.id = DA.eventId";
            sql += " WHERE  LO.id = @locatieNr ";

            lijstevents = DatabaseOperations.MaakEventLijstOpbasisLocatieNr(sql, locatieNr2);

            
            datagridEvents.ItemsSource = lijstevents;

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulComboboxComedians();
            
        }

        private void DatagridEvents_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // als je een event selecteert, dan toont een messagebox de comedians die komen optreden

            Event eventje = (Event)datagridEvents.SelectedItem;
            if (eventje is null)
            {
                MessageBox.Show("er niet naast clicken..");
            }
            else
            {
                string eventnaam = eventje.naam;

                string sql = "SELECT CO.id, CO.naam, CO.voornaam  ";
                sql += " FROM Comedy.Comedian AS CO";
                sql += " INNER JOIN Comedy.EventComedian AS EVCO ON CO.id = EVCO.comedianId";
                sql += " INNER JOIN Comedy.Event AS EV ON  EVCO.eventId = EV.id";
                sql += " WHERE  EV.naam = @eventnaam ";

                string comediansDitEvent = null;

                lijsteventComedians = DatabaseOperations.OphalenComediansVan1Event(sql, eventnaam);
                foreach (var item in lijsteventComedians)
                {
                    string voornaamAchternaam = item.voornaam + " " + item.naam + System.Environment.NewLine;
                    comediansDitEvent += voornaamAchternaam;
                }
                MessageBox.Show(comediansDitEvent);

                // eveneens wordt de naam, de website, kaartjes vrij geladen zodat je deze nog kan updaten

                txtEventname.Text = eventje.naam;
                txtPrijs.Text = eventje.prijs.ToString();
                txtLeeftijd.Text = eventje.leeftijd;
                txtWebsite.Text = eventje.website;
                DateTime datumUur1 = eventje.DatumUurs.First().datumTijdstip;
                
                if ((bool)eventje.cafeSetting)
                {
                    rbCafesetting.IsChecked = true;
                    rbSchouwburg.IsChecked = false;
                }
                else
                {
                    rbCafesetting.IsChecked = false;
                    rbSchouwburg.IsChecked = true;
                }
                if ((bool)eventje.kaartenVrij )
                {
                    cbKaartenVrij.IsChecked = true;
                }
                else
                {
                    cbKaartenVrij.IsChecked = false;
                }
                if ((bool)eventje.rolstoel)
                {
                    cbRolstoel.IsChecked = true;
                }
                else
                {
                    cbRolstoel.IsChecked = false;
                }

                // de datum en uur zit in een andere tabel

                calDatum.SelectedDate = DateTime.Parse("12/12/2002");

                string sql2 = "SELECT DA.datumTijdstip  ";
                sql2 += " FROM Comedy.DatumUur AS DA";
                sql2 += " INNER JOIN Comedy.Event AS EV ON EV.id = DA.eventId";
                sql2 += " WHERE  EV.naam = @eventnaam ";

                // lijstdatums = DatabaseOperations.GeefDatumVanEvent(sql2, eventnaam);
                // calDatum.SelectedDate = lijstdatums.First().datumTijdstip;
                calDatum.SelectedDate = datumUur1;

                // DateTime timeOnly = new DateTime(lijstdatums.First().datumTijdstip.TimeOfDay.Ticks);
                string Timeonly = datumUur1.ToShortTimeString();
                txtTijdstip.Text = Timeonly;
            }


            
        }

        
    }
}
