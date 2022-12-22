using DMP_Project_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMP_Project_WPF
{
    public partial class Geboortedatum : Form
    {
        public Geboortedatum()
        {
            InitializeComponent();
        }

        DateTime geboortedatum1;
        List<Comedian> lijstcomedians = new List<Comedian>();

        private void button1_Click(object sender, EventArgs e)
        {
            // we nemen de ingevulde datum van het formulier
            if (DateTime.TryParse(txtGeboortedatum.Text, out geboortedatum1))
            {
                string sql = "SELECT id,naam,geboortedatum FROM Comedy.Comedian";
                sql += " WHERE geboortedatum = @geboortedatum2 ";              // je kan dit niet meegeven in string format
                sql += " ORDER BY naam";

                lijstcomedians = DatabaseOperations.ZoekComedianOpBasisGeboortedatum(sql, geboortedatum1);

                if (lijstcomedians.Count == 0)
                {
                    MessageBox.Show("Onbekende geboortedatum!");
                }
                else
                {
                    this.Close();
                    string achternaam = lijstcomedians.First().naam;
                    
                    ToonPlaylist ToonPlaylistWindow = new ToonPlaylist(achternaam);
                    ToonPlaylistWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("gelieve datum in correct formaat in te vullen");
            }
        }
    }
}
