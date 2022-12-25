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

            // op basis van de geboortedatum wordt de naam van de comedian opgezocht, en deze naam wordt gebruikt om zijn playlist samen te stellen

            lijstplaylistevents = DatabaseOperations.MaakPlaylist2(naam3);         

            dataComedians.ItemsSource = lijstplaylistevents;

            lblComedianName.Content = naam3;                                    // de naam van de comedian wordt bovenaan getoond
        }

        List<PlayListEvent> lijstplaylistevents = new List<PlayListEvent>();        // dit is een extra model met enkel info die comedian interesseert

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
