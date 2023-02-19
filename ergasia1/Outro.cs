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
    public partial class Outro : Form
    {
        int w, l = 0;
        string username;
        
        public Outro(int win, int playerTurns, int time, int wins, int losses, string un)
        {
            //sets up the outro form labels 
            InitializeComponent();
             w = wins;
             l = losses;
            username = un;
            if (win == 1)
            {
                label2.Text = "You Won";
                label2.ForeColor = System.Drawing.Color.Lime;
                label3.Text = "It took you " + playerTurns + "  attempts and " + time + " seconds to beat your opponent!";
                
            }
            else
            {
                label2.Text = "You Lost";
                label2.ForeColor = System.Drawing.Color.Firebrick;
                label3.Text = "You had " + playerTurns + "  attempts and played for  " + time + " seconds before your opponent beat you!";
                
            }
            
        }

        private void Outro_Load(object sender, EventArgs e)
        {
            
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            //if the user exits it shows the number of wins and losses in the current playing session
            System.Windows.Forms.MessageBox.Show("Νικησες: "+  w.ToString() + " παιχνιδι(α) και εχασες: " + l.ToString() );
            Application.Exit();

        }
        //return to menu
        private void playbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();


        }
        //view stats button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
           Stats stats = new Stats(username);
            stats.ShowDialog();
        }
        //closes the app
        private void Outro_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
