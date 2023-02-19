using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ergasia1
{
    public partial class Menu : Form
    {
        
        public Menu()
        {
            InitializeComponent();
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void playbtn_Click(object sender, EventArgs e)
        {
           
            //checks if the username field is empty and propts the user to fill it. Until he does it doesnt let him start the game
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                System.Windows.Forms.MessageBox.Show("Πρέπει να εισάγεις όνομα χρήστη");
                textBox1.Focus();
                return;
            }
                
            //go to form naumaxia and pass the username 
            this.Hide();
            Naumaxia naumaxia = new Naumaxia(textBox1.Text);
            
            naumaxia.ShowDialog();
            
            
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            //closes the app
            Application.Exit();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //closes the app
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
