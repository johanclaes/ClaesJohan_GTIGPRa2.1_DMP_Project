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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMP_Project_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ZoekEvents_Click(object sender, RoutedEventArgs e)
        {
            ZoekEvent ZoekEventWindow = new ZoekEvent();
            ZoekEventWindow.ShowDialog();
        }

        private void EventToevoegen_Click(object sender, RoutedEventArgs e)
        {
            var myForm = new UsernamePassword();
            myForm.Show();
        }

        private void ToonPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var myForm = new Geboortedatum();
            myForm.Show();
        }

        private void ComedianToevoegen_Click(object sender, RoutedEventArgs e)
        {
            var myForm = new Boekingsbureau();
            myForm.Show();
        }
    }
}
