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
        List<string> provincies = new List<string>() { "antwerpen", "brabant", "limburg", "oost-vlaanderen", "west-vlaanderen" };
        List<Event2> lijstevents = new List<Event2>();

        private void BTNZoek_Click(object sender, RoutedEventArgs e)
        {
            // op basis van 6 velden wordt er in de volledige eventslist gezocht, en een lijst van model Event2 terug-gegeven

            string provincie1 = (string)cmbProvincie.SelectedItem;
            Comedian comedian1 = (Comedian)cmbComedian.SelectedItem;
            
            bool kaartjesvrij1 = (bool)tgKaartenvrij.IsChecked;
            bool rolstoel1 = (bool)tgRolstoel.IsChecked;
            bool cafesetting1 = (bool)tgCafesetting.IsChecked;

            lijstevents = DatabaseOperations.ZoekEvents(provincie1,comedian1,kaartjesvrij1,rolstoel1,cafesetting1);         
            dataEvents.ItemsSource = lijstevents;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 3 comboboxen worden opgevuld

            lijstcomedians = DatabaseOperations.OphalenComediansOpNaamGesorteerd();
            cmbComedian.ItemsSource = lijstcomedians;
            cmbProvincie.ItemsSource = provincies;
        }

        private void dataEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event2 eventje = (Event2)dataEvents.SelectedItem;
            if (!(eventje is null))
            {
                string eventnaam = eventje.EventNaam;
                lijstcomedians = DatabaseOperations.OphalenComediansVan1Event(eventnaam);
                string comediansDitEvent = "Comedians: " + System.Environment.NewLine;
                foreach (var item in lijstcomedians)
                {
                    string voornaamAchternaam = item.voornaam + " " + item.naam + System.Environment.NewLine;
                    comediansDitEvent += voornaamAchternaam;
                }
                System.Windows.MessageBox.Show(comediansDitEvent);
            }
            
        }
    }
}
