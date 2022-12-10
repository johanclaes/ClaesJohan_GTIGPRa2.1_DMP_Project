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
    /// Interaction logic for ComedianToevoegen.xaml
    /// </summary>
    public partial class ComedianToevoegen : Window
    {
        public ComedianToevoegen(int boekingsburoid)
        {
            InitializeComponent();
            // MessageBox.Show(boekingsburoid.ToString());
            List<Comedian> lijstcomedians2 = new List<Comedian>();
            string sql = "SELECT * FROM Comedy.Comedian";
            sql += " WHERE boekingsburoid = @id2 ";
            sql += " ORDER BY naam";

            lijstcomedians2 = DatabaseOperations.OphalenComediansVan1Buro(sql,boekingsburoid);
            foreach (Comedian comedian1 in lijstcomedians2)
            {
                stringlijstcomedians2.Add(comedian1.voornaam + " " + comedian1.naam);
            }
            lbComedians.ItemsSource = stringlijstcomedians2;
        }

        List<Comedian> lijstcomedians = new List<Comedian>();
        List<string> stringlijstcomedians = new List<string>();
        List<string> stringlijstcomedians2 = new List<string>();

        private void BTNVoegToe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNVerwijder_Click(object sender, RoutedEventArgs e)
        {

        }

        private string Valideer()
        {
            string fout = "";

            
            if (string.IsNullOrEmpty(txtVoornaam.Text))
            {
                fout += "Voornaam is verplicht" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtNaam.Text))
            {
                fout += "Naam is verplicht" + Environment.NewLine;
            }

            return fout;

        }




        private void BTNMaakComedianAan_Click(object sender, RoutedEventArgs e)
        {
            // hier moet voornaam en achternaam ingegeven worden, bij voorkeur ook geboortedatum
            

            string fout = "";
            // string fout = Valideer();
            if (string.IsNullOrEmpty(fout))
            {
                Comedian2 comedian8 = new Comedian2(txtNaam.Text, txtVoornaam.Text, calGeboortedatum.SelectedDate.Value);

                DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();

                if (xyz.ComedianToevoegen(comedian8))
                {
                    MessageBox.Show("Comedian werd toegevoegd");
                    Close();
                }
                else
                {
                    MessageBox.Show("De comedian is nog geen lid van het clubje, nogmaals ... ");
                    Close();
                }
            }
            else
            {
                MessageBox.Show(fout);
            }



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


            // lbComedians.ItemsSource = stringlijstcomedians;
        }
    }
}
