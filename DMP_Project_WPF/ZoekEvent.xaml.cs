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
        // List<string> stringlijstcomedians = new List<string>();
        List<string> provincies = new List<string>() { "antwerpen", "brabant", "limburg", "oost-vlaanderen", "west-vlaanderen" };
        List<string> maanden = new List<string>() { "januari", "februari", "maart", "april", "mei", "juni", "juli", "augustus", "september", "oktober", "november", "december" };
        List<Event2> lijstevents = new List<Event2>();

        private void BTNZoek_Click(object sender, RoutedEventArgs e)
        {
            // op basis van 6 velden wordt er in de volledige eventslist gezocht, en een lijst van model Event2 terug-gegeven

            string provincie1 = (string)cmbProvincie.SelectedItem;
            Comedian comedian1 = (Comedian)cmbComedian.SelectedItem;
            string maand1 = (string)cmbMaand.SelectedItem;
            bool kaartjesvrij1 = (bool)tgKaartenvrij.IsChecked;
            bool rolstoel1 = (bool)tgRolstoel.IsChecked;
            bool cafesetting1 = (bool)tgCafesetting.IsChecked;

            lijstevents = DatabaseOperations.ZoekEvents2(provincie1,comedian1,maand1,kaartjesvrij1,rolstoel1,cafesetting1);         
            dataEvents.ItemsSource = lijstevents;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 3 comboboxen worden opgevuld

            lijstcomedians = DatabaseOperations.OphalenComediansOpNaamGesorteerd();
            
            cmbComedian.ItemsSource = lijstcomedians;
            cmbMaand.ItemsSource = maanden;
            cmbProvincie.ItemsSource = provincies;
        }
    }
}
