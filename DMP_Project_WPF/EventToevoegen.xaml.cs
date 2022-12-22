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
            locatienummer = locatieNr;
            VulListboxEventsUwLocatie(locatienummer);
        }

        int locatienummer;
        List<Comedian> lijstcomedians = new List<Comedian>();
        List<Event> lijstevents = new List<Event>();
        // List<string> stringlijstcomedians = new List<string>();
        List<Comedian> lijsteventComedians = new List<Comedian>();
        List<DatumUur> lijstdatums = new List<DatumUur>();

        private void VoegEventToe_Click(object sender, RoutedEventArgs e)
        {
            // er wordt dus eerst een event aangemaakt, en via de knoppen comedian toevoegen en datum toevoegen 
            // maak je het event compleet

            string eventnaam5 = txtEventname.Text;
            bool eventrolstoel5 = (bool)cbRolstoel.IsChecked;
            bool eventkaartenVrij5 = (bool)cbKaartenVrij.IsChecked;
            bool eventcafesetting5 = (bool)rbCafesetting.IsChecked;
            float.TryParse(txtPrijs.Text, out float eventprijs5);
            string eventwebsite5 = txtWebsite.Text;
            string eventleeftijd5 = txtLeeftijd.Text;

            DateTime eventdatum5 = (DateTime)calDatum.SelectedDate;
            DateTime.TryParse(txtTijdstip.Text ,out DateTime eventtime5);
            DateTime eventdatumtijd5 = eventdatum5.Add(eventtime5.TimeOfDay);

            // ER MOET NOG DATAVALIDATIE GEBEUREN !!!

            Event newevent1 = new Event(eventnaam5,eventrolstoel5,eventkaartenVrij5,eventcafesetting5,eventprijs5,eventwebsite5,eventleeftijd5);

            DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();

            if (xyz.NewEventToevoegen(newevent1, locatienummer, eventdatumtijd5))
            {
                MessageBox.Show("Het Event werd toegevoegd");
                txtEventname.Text = "vul in";
                VulListboxEventsUwLocatie(locatienummer);
            }
            else
            {
                MessageBox.Show("Het event is nog niet toegevoegd, nogmaals ... ");
            }
        }

        private void btnDatum_UurToevoegen_Click(object sender, RoutedEventArgs e)
        {
            // hier wordt er telke male een extra row in datumuur table aangemaakt met foreign key verwijzend naar geselecteerd event
            DateTime eventdatum5 = (DateTime)calDatum.SelectedDate;
            DateTime.TryParse(txtTijdstip.Text, out DateTime eventtime5);
            DateTime eventdatumtijd5 = eventdatum5.Add(eventtime5.TimeOfDay);
            Event selectedEvent = (Event)datagridEvents.SelectedItem;

            DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();

            if (xyz.NewVoorstellingToevoegen(selectedEvent, eventdatumtijd5))
            {
                MessageBox.Show("Extra voorstelling werd toegevoegd !");
                txtEventname.Text = "vul in";
                txtTijdstip.Text = "13:30";
                VulListboxEventsUwLocatie(locatienummer);
            }
            else
            {
                MessageBox.Show("Extra voorstelling is nog niet toegevoegd, nogmaals ... ");
            }
        }

        private void BTNComedianToevoegen_Click(object sender, RoutedEventArgs e)
        {
            // hier wordt telke male een extra eventcomedian toegevoegd 
            Comedian selectedComedian = (Comedian)cmbComedians.SelectedItem;
            Event selectedEvent = (Event)datagridEvents.SelectedItem;
            if (selectedComedian is null || selectedEvent is null )
            {
                MessageBox.Show("Eerst comedian selecteren via combobox");
            }
            else
            {
                DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();

                if (xyz.ComedianToevoegenEvent(selectedEvent, selectedComedian ))
                {
                    MessageBox.Show("De comedian werd toegevoegd");
                    // txtEventname.Text = "vul in";
                    VulListboxEventsUwLocatie(locatienummer);
                    cmbComedians.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("De comedian is nog niet toegevoegd, nogmaals ... ");
                }
            }
        }

        private void BTNUpdateEvent_Click(object sender, RoutedEventArgs e)
        {
            // als de kaartjes op zijn of de prijs wordt aangepast of er wordt extra info via website gegeven
            Event selectedEvent = (Event)datagridEvents.SelectedItem;
            bool nogKaartjes3 = (bool)cbKaartenVrij.IsChecked;
            float.TryParse(txtPrijs.Text,out float prijs3);
            string website3 = txtWebsite.Text;

            if ( selectedEvent is null)
            {
                MessageBox.Show("Selecteer eerst te updaten event.");
            }
            else
            {
                DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();

                if (xyz.EventUpdaten(selectedEvent, nogKaartjes3, prijs3, website3))
                {
                    MessageBox.Show("Eventdata zijn aangepast.");
                    // txtEventname.Text = "vul in";
                    VulListboxEventsUwLocatie(locatienummer);
                    cmbComedians.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Eventdata nog niet aangepast ... ");
                }
            }



        }


        private void VulComboboxComedians()
        {
            // een lijst met alle comedians wordt opgehaald en in de combobox getoond.
            List<Comedian> lijstcomedians2 = new List<Comedian>();
            string sql = "SELECT * FROM Comedy.Comedian";
            sql += " ORDER BY naam";

            lijstcomedians = DatabaseOperations.OphalenComediansOpNaamGesorteerd(sql);
            
            cmbComedians.ItemsSource = lijstcomedians;
        }

        private void VulListboxEventsUwLocatie(int locatieNr2)
        {
            // in de listbox gaan we alle events tonen die verwijzen naar de locatie waar dit locatiecontact voor verantwoordelijk is

            string sql = "SELECT EV.id, EV.naam, EV.cafeSetting, EV.website, EV.prijs, EV.leeftijd, EV.rolstoel, EV.kaartenVrij, DA.id, DA.datumTijdstip   ";
            // string sql = "SELECT EV.id, EV.naam, EV.cafeSetting, EV.website, EV.prijs, EV.leeftijd, EV.rolstoel, EV.kaartenVrij   ";
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
                MessageBox.Show("Selecteer Event!");
            }
            else
            {
                string eventnaam = eventje.naam;

                string sql = "SELECT CO.id, CO.naam, CO.voornaam  ";
                sql += " FROM Comedy.Comedian AS CO";
                sql += " INNER JOIN Comedy.EventComedian AS EVCO ON CO.id = EVCO.comedianId";
                sql += " INNER JOIN Comedy.Event AS EV ON  EVCO.eventId = EV.id";
                sql += " WHERE  EV.naam = @eventnaam ";

                string comediansDitEvent = "Comedians: " + System.Environment.NewLine;

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
