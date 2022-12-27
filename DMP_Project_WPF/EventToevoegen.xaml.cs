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
using System.Windows.Forms;
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
        List<DatumUur> lijstdatums = new List<DatumUur>();

        private void VoegEventToe_Click(object sender, RoutedEventArgs e)
        {
            // er wordt dus eerst een event aangemaakt op 1 datum met uur,
            // en via de buttons "Comedian Toevoegen" en "extra DatumUur Toevoegen", maak je het event compleet

            string eventnaam5 = txtEventname.Text;
            bool eventrolstoel5 = (bool)cbRolstoel.IsChecked;
            bool eventkaartenVrij5 = (bool)cbKaartenVrij.IsChecked;
            bool eventcafesetting5 = (bool)rbCafesetting.IsChecked;
            float eventprijs5 = 0;
            float.TryParse(txtPrijs.Text, out eventprijs5);
            string eventwebsite5 = txtWebsite.Text;
            string eventleeftijd5 = txtLeeftijd.Text;

            try
            {
                DateTime eventdatum5 = (DateTime)calDatum.SelectedDate;
                DateTime.TryParse(txtTijdstip.Text, out DateTime eventtime5);
                DateTime eventdatumtijd5 = eventdatum5.Add(eventtime5.TimeOfDay);
                DatumUur eventDatumX = new DatumUur(eventdatumtijd5);
                Event newevent1 = new Event(eventnaam5, eventrolstoel5, eventkaartenVrij5, eventcafesetting5, eventprijs5, eventwebsite5, eventleeftijd5);

                if (newevent1.IsGeldig() && eventDatumX.IsGeldig())
                {
                    DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();

                    if (xyz.NewEventToevoegen(newevent1, locatienummer, eventDatumX))
                    {
                        System.Windows.MessageBox.Show("Het Event werd toegevoegd");
                        txtEventname.Text = "vul in";
                        VulListboxEventsUwLocatie(locatienummer);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Het event is nog niet toegevoegd, nogmaals ... ");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(newevent1.Foutmeldingen + eventDatumX.Foutmeldingen);
                }
            }
            catch (Exception ex)
            {
                FileOperations.foutLoggen(ex, "VoegEventToe_Click");
                System.Windows.MessageBox.Show("Gelieve een datum en uur te selecteren.");
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
                System.Windows.MessageBox.Show("Extra voorstelling werd toegevoegd !");
                txtEventname.Text = "vul in";
                VulListboxEventsUwLocatie(locatienummer);
            }
            else
            {
                System.Windows.MessageBox.Show("Extra voorstelling is nog niet toegevoegd, nogmaals ... ");
            }
        }

        private void BTNComedianToevoegen_Click(object sender, RoutedEventArgs e)
        {
            // hier wordt telke male een extra rij in eventcomedian toegevoegd 
            Comedian selectedComedian = (Comedian)cmbComedians.SelectedItem;
            Event selectedEvent = (Event)datagridEvents.SelectedItem;
            if (selectedComedian is null || selectedEvent is null )
            {
                System.Windows.MessageBox.Show("Eerst comedian selecteren via combobox");
            }
            else
            {
                DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();

                if (xyz.ComedianToevoegenEvent(selectedEvent, selectedComedian ))
                {
                    System.Windows.MessageBox.Show("De comedian werd toegevoegd");
                    // txtEventname.Text = "vul in";
                    VulListboxEventsUwLocatie(locatienummer);
                    cmbComedians.SelectedItem = null;
                }
                else
                {
                    System.Windows.MessageBox.Show("De comedian is nog niet toegevoegd, nogmaals ... ");
                }
            }
        }

        private void BTNUpdateEvent_Click(object sender, RoutedEventArgs e)
        {
            // als de kaartjes op zijn of de prijs wordt aangepast of info van website aanpassen, andere properties kunnen NIET aangepast worden
            Event selectedEvent = (Event)datagridEvents.SelectedItem;
            bool nogKaartjes3 = (bool)cbKaartenVrij.IsChecked;
            float.TryParse(txtPrijs.Text,out float prijs3);
            string website3 = txtWebsite.Text;

            if ( selectedEvent is null)
            {
                System.Windows.MessageBox.Show("Selecteer eerst te updaten event.");
            }
            else
            {
                DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();

                if (xyz.EventUpdaten(selectedEvent, nogKaartjes3, prijs3, website3))
                {
                    System.Windows.MessageBox.Show("Eventdata zijn aangepast.");
                    VulListboxEventsUwLocatie(locatienummer);
                    cmbComedians.SelectedItem = null;
                }
                else
                {
                    System.Windows.MessageBox.Show("Eventdata nog niet aangepast ... ");
                }
            }
        }


        private void VulComboboxComedians()
        {
            // ***een lijst met alle comedians wordt opgehaald en in de combobox getoond.
            
            lijstcomedians = DatabaseOperations.OphalenComediansOpNaamGesorteerd();
            cmbComedians.ItemsSource = lijstcomedians;
        }

        private void VulListboxEventsUwLocatie(int locatieNr2)
        {
            // ***in de listbox gaan we alle events tonen die verwijzen naar de locatie waar dit locatiecontact voor verantwoordelijk is

            lijstevents = DatabaseOperations.MaakEventLijstOpbasisLocatieNr(locatieNr2);
            // we maken van de "geneste" property => gewone property zodat deze in datagrid kan getoond worden.
            foreach (var item in lijstevents)
            {
                item.datumUren = item.DatumUurs.First().datumTijdstip;
            }
            datagridEvents.ItemsSource = lijstevents;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulComboboxComedians();
        }

        private void DatagridEvents_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // *** als je een event selecteert, dan toont een messagebox de comedians die komen optreden

            Event eventje = (Event)datagridEvents.SelectedItem;
            if (eventje is null)
            {
                System.Windows.MessageBox.Show("Selecteer Event!");
            }
            else
            {
                string eventnaam = eventje.naam;
                lijstcomedians = DatabaseOperations.OphalenComediansVan1Event(eventnaam);
                string comediansDitEvent = "Comedians: " + System.Environment.NewLine;
                foreach (var item in lijstcomedians)
                {
                    string voornaamAchternaam = item.voornaam + " " + item.naam + System.Environment.NewLine;
                    comediansDitEvent += voornaamAchternaam;
                }
                System.Windows.MessageBox.Show(comediansDitEvent);

                // eveneens wordt de naam, de website, kaartjes vrij, prijs ... geladen zodat je deze nog kan updaten

                txtEventname.Text = eventje.naam;
                txtPrijs.Text = eventje.prijs.ToString();
                txtLeeftijd.Text = eventje.leeftijd;
                txtWebsite.Text = eventje.website;
                DateTime datumUur1 = eventje.DatumUurs.First().datumTijdstip;
                calDatum.SelectedDate = datumUur1;
                string Timeonly = datumUur1.ToShortTimeString();
                txtTijdstip.Text = Timeonly;

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

            }
        }
    }
}
