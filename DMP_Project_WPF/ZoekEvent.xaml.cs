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
            string sql = "abc";
            lijstevents = DatabaseOperations.ZoekEvents(sql);         // je kan niet zomaar type1 op type2 mappen

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
