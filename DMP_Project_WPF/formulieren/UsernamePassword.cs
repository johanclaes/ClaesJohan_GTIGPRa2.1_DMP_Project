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
    public partial class UsernamePassword : Form
    {
        public UsernamePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT id,loginName,loginPassword,locatieId FROM Comedy.LocatieContact";
            sql += " WHERE loginName = @loginName ";              // je kan dit niet meegeven in string format
            

            string username1 = txtUsername.Text;
            string password1 = txtPassword.Text;


            int locatieNr = DatabaseOperations.OpzoekenLocatieOpBasisUsernamePassword(sql, username1, password1);

            if (locatieNr == -1)
            {
                MessageBox.Show("Onbekende username-password");
            }
            else
            {
                this.Close();
                EventToevoegen EventToevoegenWindow = new EventToevoegen(locatieNr);
                EventToevoegenWindow.ShowDialog();
            }

            
        }
    }
}
