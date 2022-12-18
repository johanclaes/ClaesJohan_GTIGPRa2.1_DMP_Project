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
            VulListboxVanDitBuro(boekingsburoid);
            boekingsburoid1 = boekingsburoid;
            
        }

        List<Comedian> lijstcomedians = new List<Comedian>();
        List<string> stringlijstcomedians = new List<string>();
        List<string> stringlijstcomedians2 = new List<string>();
        int boekingsburoid1;
        Comedian2 selectedComedian;
        string selectedComedianNaam; 

        private void VulListboxVanDitBuro(int boekingsburoid2)
        {
            // comedians van hun boekingsbureau, zie je in de listbox
            List<Comedian2> lijstcomedians2 = new List<Comedian2>();
            string sql = "SELECT * FROM Comedy.Comedian";
            sql += " WHERE boekingsburoid = @id2 ";                             // het boekingsburoid komt mee met het formulier
            sql += " ORDER BY naam";

            lijstcomedians = DatabaseOperations.OphalenComediansVan1Buro(sql, boekingsburoid2);
            //  de klasse comedian bevat geen ToString, dus lijstComedian copiëren naar lijstComedian2
            foreach (Comedian comedian1 in lijstcomedians)
            {
                Comedian2 comedian2 = new Comedian2(comedian1.naam,comedian1.voornaam);
                lijstcomedians2.Add(comedian2);
                
            }
            // lbComedians.ItemsSource = stringlijstcomedians2;
            lbComedians.ItemsSource = lijstcomedians2;

        }

        private void VulComboboxComedianZonderBuro()
        {
            // enkel comedians die geen boekingsburo hebben, kan je toevoegen en komen in de combobox
            List<Comedian2> lijstcomedians2 = new List<Comedian2>();
            string sql = "SELECT * FROM Comedy.Comedian";
            sql += " WHERE boekingsburoid IS NULL ";                            // als boekingsburoid = NULL dan is comedian zonder buro
            sql += " ORDER BY naam";

            lijstcomedians = DatabaseOperations.OphalenComediansOpNaamGesorteerd(sql);
            foreach (Comedian comedian1 in lijstcomedians)
            {
                Comedian2 comedian2 = new Comedian2(comedian1.naam, comedian1.voornaam);
                lijstcomedians2.Add(comedian2);
            }
            cmbComedians.ItemsSource = lijstcomedians2;

        }

        private void BTNVoegToe_Click(object sender, RoutedEventArgs e)
        {
            // uit de combobox van comedians zonder buro, kan er ene toegevoegd worden aan het eigen buro
            selectedComedian = (Comedian2)cmbComedians.SelectedItem;
            if (selectedComedian is null)
            {
                MessageBox.Show("Selecteer een Comedian in de Combobox.");
            }
            else
            {
                selectedComedianNaam = selectedComedian.naam;
                DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();
                if (xyz.ComedianBuroInvullen(selectedComedianNaam, boekingsburoid1))
                {
                    MessageBox.Show("Comedian is nu lid van ons buro.");
                }
                else
                {
                    MessageBox.Show("Er is niks aangepast.");
                }

                VulListboxVanDitBuro(boekingsburoid1);
                VulComboboxComedianZonderBuro();
            }
            
        }

        private void BTNVerwijder_Click(object sender, RoutedEventArgs e)
        {
            // als een comedian niet meer wil verdergaan met ons buro, aanvinken in listbox en verwijder clicken
            selectedComedian = (Comedian2)lbComedians.SelectedItem;
            if (selectedComedian is null)
            {
                MessageBox.Show("Selecteer een Comedian in de listbox.");
            }
            else
            {
                selectedComedianNaam = selectedComedian.naam;
                DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();
                if (xyz.ComedianBuroVerwijderen(selectedComedianNaam))
                {
                    MessageBox.Show("een extra comedian zonder buro.");
                }
                else
                {
                    MessageBox.Show("Er is niks aangepast.");
                }
                VulListboxVanDitBuro(boekingsburoid1);
                VulComboboxComedianZonderBuro();
            }
            
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
                    VulComboboxComedianZonderBuro();
                }
                else
                {
                    MessageBox.Show("De comedian is nog geen lid van het clubje, nogmaals ... ");
                }
            }
            else
            {
                MessageBox.Show(fout);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulComboboxComedianZonderBuro();

        }

        private void BTNComedian_Stopt_Click(object sender, RoutedEventArgs e)
        {
            selectedComedian = (Comedian2)cmbComedians.SelectedItem;
            if (selectedComedian is null)
            {
                MessageBox.Show("Selecteer een Comedian in de combobox.");
            }
            else
            {
                selectedComedianNaam = selectedComedian.naam;
                DatabaseOperationsWrite xyz = new DatabaseOperationsWrite();        // eerst eventcomedian opkuisen .. dan comedian deleten
                if (xyz.ComedianStoptErmee(selectedComedianNaam))
                {
                    MessageBox.Show("Comedian heeft zijn schop afgekuisd.");
                }
                else
                {
                    MessageBox.Show("Er is niks aangepast.");
                }
                VulListboxVanDitBuro(boekingsburoid1);
                VulComboboxComedianZonderBuro();
            }
            
        }
    }
}
