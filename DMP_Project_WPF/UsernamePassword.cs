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
            if (txtUsername.Text == "username")
            {
                if (txtPassword.Text == "password")
                {
                    this.Close();
                    EventToevoegen EventToevoegenWindow = new EventToevoegen();
                    EventToevoegenWindow.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Wrong password");
                }
            }
            else
            {
                MessageBox.Show("Onbekende username!");
            }
        }
    }
}
