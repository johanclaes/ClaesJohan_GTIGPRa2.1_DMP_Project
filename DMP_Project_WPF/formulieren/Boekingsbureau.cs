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
    public partial class Boekingsbureau : Form
    {
        public Boekingsbureau()
        {
            InitializeComponent();
        }

        List<Boekingsburo> lijstboekingsburo = new List<Boekingsburo>();

        private void button1_Click(object sender, EventArgs e)
        {
            // we gaan zoeken of de ingegeven naam voorkomt in de tabel Boekingsburo als naam
            string sql = "SELECT id FROM Comedy.Boekingsburo";
            sql += " WHERE naam = @naam2 ";
            lijstboekingsburo = DatabaseOperations.ZoekBuroOpBasisNaam(sql,txtBoekingsbureau.Text);

            if (lijstboekingsburo.Count == 0 )
            {
                MessageBox.Show("Onbekend boekingsbureau!");
            }
            else
            {
                this.Close();
                int boekingsburoid = lijstboekingsburo.First().id;

                ComedianToevoegen ComedianToevoegenWindow = new ComedianToevoegen(boekingsburoid);
                ComedianToevoegenWindow.ShowDialog();
            }

 
        }
    }
}
