using DMP_Project_DAL;
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
    /// Interaction logic for ComedianToevoegen.xaml
    /// </summary>
    public partial class ComedianToevoegen : Window
    {
        public ComedianToevoegen()
        {
            InitializeComponent();
        }

        List<Comedian> lijstcomedians = new List<Comedian>();
        List<string> stringlijstcomedians = new List<string>();

        private void BTNVoegToe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNVerwijder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNMaakComedianAan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // enkel comedians die geen boekingsburo hebben, kan je toevoegen en komen in de combobox
            List<Comedian> lijstcomedians = new List<Comedian>();
            string sql = "SELECT * FROM Comedy.Comedian";
            sql += " WHERE boekingsburoid IS NULL ";                 
            sql += " ORDER BY naam";

            lijstcomedians = DatabaseOperations.OphalenComediansOpNaamGesorteerd(sql);
            foreach (Comedian comedian1 in lijstcomedians)
            {
                stringlijstcomedians.Add(comedian1.voornaam + " " + comedian1.naam);
            }
            cmbComedians.ItemsSource = stringlijstcomedians;

            // comedians van hun boekingsbureau, zie je in de listbox


            lbComedians.ItemsSource = stringlijstcomedians;
        }
    }
}
