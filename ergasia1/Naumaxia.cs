using ergasia1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace ergasia1
{
    public partial class Naumaxia : Form
    {
        
        Random r;
        //creating the ships
        Ship Aeroplanoforo = new Ship(5);
        Ship Antitorpiliko = new Ship(4);
        Ship Polemiko = new Ship(3);
        Ship Ypovryxio = new Ship(2);
        EnemyShip Aeroplanoforo2 = new EnemyShip(5);
        EnemyShip Antitorpiliko2 = new EnemyShip(4);
        EnemyShip Polemiko2 = new EnemyShip(3);
        EnemyShip Ypovryxio2 = new EnemyShip(2);
        //initializing the variables that count how many times a ship  has been hit 
        int aeHits = 0;
        int anHits = 0;
        int poHits = 0;
        int ypHits = 0;
        int ae2Hits = 0;
        int an2Hits = 0;
        int po2Hits = 0;
        int yp2Hits = 0;
        //initializing the variables that count how many times each side has played
        int playert = 0;
        int enemyt = 0;
        //initializing the variable that shows the winner
        int win = 0;
        //initializing the variable that keeps the time tha has passed
        int timePassed = 0;
        ////initializing a variable to disable the click event so that the player can not play again before the enemy has played
        bool played = false;
        //initializing the variables that keep the user name and the winner
        string user, winner;
        //initializing the sqlite connection
        SQLiteConnection connection;


        public Naumaxia(string un)
        {
    
            InitializeComponent();
            //saving the username to a local variable
            user = un;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //checks if the database file exists. If it doesnt it creates it and the table inside it. In both cases it creates the connection
            if (File.Exists("NaumaxiaStats.db"))
            {
                connection = new SQLiteConnection("Data Source=NaumaxiaStats.db;Version=3;");    
            }
            else
            {
                
                SQLiteConnection.CreateFile("NaumaxiaStats.db");
                connection = new SQLiteConnection("Data Source=NaumaxiaStats.db;Version=3;");
                connection.Open();
                string sql = "create table naumaxiaSt (username char, winner  char, time integer)";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            //creates a new random shows the labels and calls the gamesetup function
            r = new Random();
            info.Show();
            label5.Show();
            gameSetUp();
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //when the timer ticks it updates the game timer and the played turns of each side
            timePassed++;
            lblTime.Text = "Time: " + timePassed;
            playerTurns.Text = "playerTurns: " + playert;
            EnemyTurns.Text = "enemyTurns: " + enemyt;
        }

        

        

        private void gameSetUp()
        {
            //this function calls the 2 functions that place the ships for the user and the enemy
            placePlayerShips();
            placeEnemyShips();    
        }

        
        private void placePlayerShips()
        {
            //placement aeroplanoforou
            //gets a random number between zero and one. If zero the ship will be horizontal else it will be vertical
            int temp = r.Next(0, 2);

            if (temp == 0)
            {
                //changes the horizontal property to true and gets a row location between 0 and 9 and the first column location between 0 and 5 because after five there is not enough space to place the ship
                Aeroplanoforo.hor = true;
                int rloc = r.Next(0, 10);
                int cloc = r.Next(0, 6);
                //places the ship by placing labels with its name in the coordinates we got before 
                for (int i = cloc; i < cloc + 5; i++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Ae";
                    lbl.ForeColor = System.Drawing.Color.Yellow;
                    playersField.Controls.Add(lbl, i, rloc);
                    
                }
            }
            else
            {
                // else changes the horizontal property to false and gets a column location between 0 and 9 and the first row location between 0 and 5 because after five there is not enough space to place the ship
                Aeroplanoforo.hor = false;
                int cloc = r.Next(0, 10);
                int rloc = r.Next(0, 6);
                //places the ship by placing labels with its name in the coordinates we got before 
                for (int i = rloc; i < rloc + 5; i++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Ae";
                    lbl.ForeColor = System.Drawing.Color.Yellow;
                    playersField.Controls.Add(lbl, cloc, i);
                }
            }
            //placement antitorpilikou
            //gets a random number between zero and one. If zero the ship will be horizontal else it will be vertical
            int temp2 = r.Next(0, 2);
            List<int> num = new List<int>();
            
            if (temp2 == 0)
            {
                //changes the horizontal property to true and gets a row location between 0 and 9 and the first column location between 0 and 6 because after six there is not enough space to place the ship
                Antitorpiliko.hor = true;
                int rloc = r.Next(0, 10);
                int cloc = r.Next(0, 7);
                bool ok = false;
                //checks if there is another ship(label) in these coordinates. If there isnt we keep the coordinates else we ask for new ones until there is not another ship there
                while (!ok) 
                {
                    int y = 0;
                    for (int i = cloc; i < cloc + 4; i++)
                    {
                        Control c = playersField.GetControlFromPosition(i, rloc);
                        if (c == null)
                        {
                            y++;
                            if (y == 4)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num.Add(i);
                        }
                    }
                    if (num.Count == 0)
                    {
                        ok = true;
                    }
                    else 
                    {
                        num.Clear();
                        rloc = r.Next(0, 10);
                        
                        cloc = r.Next(0, 7);
                    }
                }
                //places the ship by placing labels with its name in the coordinates we got before 
                for (int r = cloc; r < cloc + 4; r++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "An";
                    lbl.ForeColor = System.Drawing.Color.DarkBlue;
                    playersField.Controls.Add(lbl, r, rloc);
                }
            }
            else
            {
                //else changes the horizontal property to false and gets a column location between 0 and 9 and the first row location between 0 and 6 because after six there is not enough space to place the ship
                Antitorpiliko.hor = false;
                int cloc = r.Next(0, 10);
                int rloc = r.Next(0, 7);
                bool ok = false;
                while (!ok)
                {
                    //checks if there is another ship(label) in these coordinates. If there isnt we keep the coordinates else we ask for new ones until there is not another ship there
                    int y = 0;
                    for (int i = rloc; i < rloc + 4; i++)
                    {
                        Control c = playersField.GetControlFromPosition(cloc, i);
                        if (c == null)
                        {
                            y++;
                            if (y == 4)
                            {
                                break;
                            }
                        }
                        else
                        {                           
                            num.Add(i);
                        }
                    }
                    if (num.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {                       
                        num.Clear();
                        cloc = r.Next(0, 10);
                        
                        rloc = r.Next(0, 7);
                    }
                }
                //places the ship by placing labels with its name in the coordinates we got before 
                for (int i = rloc; i < rloc + 4; i++)
                { 
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "An";
                    lbl.ForeColor = System.Drawing.Color.DarkBlue;
                    playersField.Controls.Add(lbl, cloc, i);
                }
            }
            //placement polemikou
            //gets a random number between zero and one. If zero the ship will be horizontal else it will be vertical
            int temp3 = r.Next(0, 2);
            List<int> num2 = new List<int>();
            if (temp3 == 0)
            {
                //changes the horizontal property to true and gets a row location between 0 and 9 and the first column location between 0 and 7 because after seven there is not enough space to place the ship
                Polemiko.hor = true;
                int rloc = r.Next(0, 10);
                int cloc = r.Next(0, 8);
                bool ok = false;
                while (!ok)
                {
                    //checks if there is another ship(label) in these coordinates. If there isnt we keep the coordinates else we ask for new ones until there is not another ship there
                    int y = 0;
                    for (int i = cloc; i < cloc + 3; i++)
                    {
                        Control c = playersField.GetControlFromPosition(i, rloc);
                        if (c == null)
                        {
                            y++;
                            if (y == 3)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num2.Add(i);
                        }
                    }
                    if (num2.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num2.Clear();
                        rloc = r.Next(0, 10);
                        
                        cloc = r.Next(0, 8);
                    }
                }
                //places the ship by placing labels with its name in the coordinates we got before 
                for (int r = cloc; r < cloc + 3; r++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Po";
                    lbl.ForeColor = System.Drawing.Color.Purple;
                    playersField.Controls.Add(lbl, r, rloc);
                }
            }
            else
            {
                //else changes the horizontal property to false and gets a column location between 0 and 9 and the first row location between 0 and 7 because after seven there is not enough space to place the ship
                Polemiko.hor = false;
                int cloc = r.Next(0, 10);
                int rloc = r.Next(0, 8);
                bool ok = false;
                while (!ok)
                {
                    //checks if there is another ship(label) in these coordinates. If there isnt we keep the coordinates else we ask for new ones until there is not another ship there
                    int y = 0;
                    for (int i = rloc; i < rloc + 3; i++)
                    {
                        Control c = playersField.GetControlFromPosition(cloc, i);
                        if (c == null)
                        {
                            y++;
                            if (y == 3)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num2.Add(i);
                        }
                    }
                    if (num2.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num2.Clear();
                        cloc = r.Next(0, 10);
                        
                        rloc = r.Next(0, 8);
                    }
                }
                //places the ship by placing labels with its name in the coordinates we got before 
                for (int i = rloc; i < rloc + 3; i++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Po";
                    lbl.ForeColor = System.Drawing.Color.Purple;
                    playersField.Controls.Add(lbl, cloc, i);
                }
            }
            //placement Ypovryxiou
            //gets a random number between zero and one. If zero the ship will be horizontal else it will be vertical
            int temp4 = r.Next(0, 2);
            List<int> num3 = new List<int>();
            if (temp4 == 0)
            {
                //changes the horizontal property to true and gets a row location between 0 and 9 and the first column location between 0 and 8 because after eight there is not enough space to place the ship
                Ypovryxio.hor = true;
                int rloc = r.Next(0, 10);
                int cloc = r.Next(0, 9);
                bool ok = false;
                while (!ok)
                {
                    //checks if there is another ship(label) in these coordinates. If there isnt we keep the coordinates else we ask for new ones until there is not another ship there
                    int y = 0;
                    for (int i = cloc; i < cloc + 2; i++)
                    {
                        Control c = playersField.GetControlFromPosition(i, rloc);
                        if (c == null)
                        {
                            y++;
                            if (y == 2)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num3.Add(i);
                        }
                    }
                    if (num3.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num3.Clear();
                        rloc = r.Next(0, 10);
                        
                        cloc = r.Next(0, 9);
                    }
                }
                //places the ship by placing labels with its name in the coordinates we got before 
                for (int r = cloc; r < cloc + 2; r++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Yp";
                    playersField.Controls.Add(lbl, r, rloc);
                }
            }
            else
            {
                //else changes the horizontal property to false and gets a column location between 0 and 9 and the first row location between 0 and 8 because after eight there is not enough space to place the ship
                Ypovryxio.hor = false;
                int cloc = r.Next(0, 10);
                int rloc = r.Next(0, 9);
                bool ok = false;
                while (!ok)
                {
                    //checks if there is another ship(label) in these coordinates. If there isnt we keep the coordinates else we ask for new ones until there is not another ship there
                    int y = 0;
                    for (int i = rloc; i < rloc + 2; i++)
                    {
                        Control c = playersField.GetControlFromPosition(cloc, i);
                        if (c == null)
                        {
                            y++;
                            if (y == 2)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num3.Add(i);
                        }
                    }
                    if (num3.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num3.Clear();
                        cloc = r.Next(0, 10);
                        
                        rloc = r.Next(0, 9);
                    }
                }
                //places the ship by placing labels with its name in the coordinates we got before 
                for (int i = rloc; i < rloc + 2; i++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Yp";
                    playersField.Controls.Add(lbl, cloc, i);
                }
            }
        }

        //this function places the enemy's ships. it works the same way with the placePlayerShips function. The only difference is that we hide the labels after we add them to the table
        private void placeEnemyShips()
        {
            //aeroplanoforo
            int temp = r.Next(0, 2);
            if (temp == 0)
            {
                Aeroplanoforo2.hor = true;
                int rloc = r.Next(0, 10);
                int cloc = r.Next(0, 6);
                for (int i = cloc; i < cloc + 5; i++)
                {
                    
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Ae2";
                    enemyField.Controls.Add(lbl, i, rloc);
                    lbl.Hide();
                }
            }
            else
            {
                Aeroplanoforo2.hor = false;
                int cloc = r.Next(0, 10);
                
                int rloc = r.Next(0, 6);
                for (int i = rloc; i < rloc + 5; i++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Ae2";
                    enemyField.Controls.Add(lbl, cloc, i);
                    lbl.Hide();
                }
            }
            //antitorpiliko
            int temp2 = r.Next(0, 2);
            List<int> num = new List<int>();
            if (temp2 == 0)
            {
                Antitorpiliko2.hor = true;
                int rloc = r.Next(0, 10);
                
                int cloc = r.Next(0, 7);
                bool ok = false;
                while (!ok)
                {
                    int y = 0;
                    for (int i = cloc; i < cloc + 4; i++)
                    {
                        Control c = enemyField.GetControlFromPosition(i, rloc);
                        if (c == null)
                        {
                            y++;
                            if (y == 4)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num.Add(i);
                        }
                    }
                    if (num.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num.Clear();
                        rloc = r.Next(0, 10);
                        
                        cloc = r.Next(0, 7);
                    }
                }
                for (int r = cloc; r < cloc + 4; r++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "An2";
                    enemyField.Controls.Add(lbl, r, rloc);
                    lbl.Hide();
                }
            }
            else
            {
                Antitorpiliko2.hor = false;
                int cloc = r.Next(0, 10);
                
                int rloc = r.Next(0, 7);
                bool ok = false;
                while (!ok)
                {
                    int y = 0;
                    for (int i = rloc; i < rloc + 4; i++)
                    {
                        Control c =enemyField.GetControlFromPosition(cloc, i);
                        if (c == null)
                        {
                            y++;
                            if (y == 4)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num.Add(i);
                        }
                    }
                    if (num.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num.Clear();
                        cloc = r.Next(0, 10);
                        
                        rloc = r.Next(0, 7);
                    }
                }
                for (int i = rloc; i < rloc + 4; i++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "An2";
                    enemyField.Controls.Add(lbl, cloc, i);
                    lbl.Hide();
                }
            }
            //polemiko
            int temp3 = r.Next(0, 2);
            List<int> num2 = new List<int>();
            if (temp3 == 0)
            {
                Polemiko2.hor = true;
                int rloc = r.Next(0, 10);
                
                int cloc = r.Next(0, 8);
                bool ok = false;
                while (!ok)
                {
                    int y = 0;
                    for (int i = cloc; i < cloc + 3; i++)
                    {
                        Control c = enemyField.GetControlFromPosition(i, rloc);
                        if (c == null)
                        {
                            y++;
                            if (y == 3)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num2.Add(i);
                        }
                    }
                    if (num2.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num2.Clear();
                        rloc = r.Next(0, 10);
                        
                        cloc = r.Next(0, 8);
                    }
                }
                for (int r = cloc; r < cloc + 3; r++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Po2";
                    enemyField.Controls.Add(lbl, r, rloc);
                    lbl.Hide();
                }
            }
            else
            {
                Polemiko2.hor = false;
                int cloc = r.Next(0, 10);
                
                int rloc = r.Next(0, 8);
                bool ok = false;
                while (!ok)
                {
                    int y = 0;
                    for (int i = rloc; i < rloc + 3; i++)
                    {
                        Control c = enemyField.GetControlFromPosition(cloc, i);
                        if (c == null)
                        {
                            y++;
                            if (y == 3)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num2.Add(i);
                        }
                    }
                    if (num2.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num2.Clear();
                        cloc = r.Next(0, 10);
                        
                        rloc = r.Next(0, 8);
                    }
                }
                for (int i = rloc; i < rloc + 3; i++)
                {
                    
                    
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Po2";
                    enemyField.Controls.Add(lbl, cloc, i);
                    lbl.Hide();
                }
            }
            //Ypovryxio
            int temp4 = r.Next(0, 2);
            List<int> num3 = new List<int>();
            if (temp4 == 0)
            {
                Ypovryxio2.hor = true;
                int rloc = r.Next(0, 10);
                
                int cloc = r.Next(0, 9);
                bool ok = false;
                while (!ok)
                {
                    int y = 0;
                    for (int i = cloc; i < cloc + 2; i++)
                    {
                        Control c = enemyField.GetControlFromPosition(i, rloc);
                        if (c == null)
                        {
                            y++;
                            if (y == 2)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num3.Add(i);
                        }
                    }
                    if (num3.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num3.Clear();
                        rloc = r.Next(0, 10);
                        
                        cloc = r.Next(0, 9);
                    }
                }
                for (int r = cloc; r < cloc + 2; r++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Yp2";
                    enemyField.Controls.Add(lbl, r, rloc);
                    lbl.Hide();
                }
            }
            else
            {
                Ypovryxio2.hor = false;
                int cloc = r.Next(0, 10);
                
                int rloc = r.Next(0, 9);
                bool ok = false;
                while (!ok)
                {
                    int y = 0;
                    for (int i = rloc; i < rloc + 2; i++)
                    {
                        Control c = enemyField.GetControlFromPosition(cloc, i);
                        if (c == null)
                        {
                            y++;
                            if (y == 2)
                            {
                                break;
                            }
                        }
                        else
                        {
                            num3.Add(i);
                        }
                    }
                    if (num3.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        num3.Clear();
                        cloc = r.Next(0, 10);
                        
                        rloc = r.Next(0, 9);
                    }
                }
                for (int i = rloc; i < rloc + 2; i++)
                {
                    var lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Text = "Yp2";
                    enemyField.Controls.Add(lbl, cloc, i);
                    lbl.Hide();
                }
            }

        }

        //this function takes the coordinates on the ennemyboard that the player clicked, sends it to the getRowCol that sends back the row and column location of these coordinates
       private void enemyField_Click(object sender, MouseEventArgs e)
        {
            //prevents the user to play if the game has ended or if the enemy hasnt played after the user played
            if (win == 0 && !played)
            {
                played = true;
                //increases the playerturn variable
                playert++;
                var position = GetRowCol(enemyField, enemyField.PointToClient(Cursor.Position));
                Control c = enemyField.GetControlFromPosition(position.Value.X, position.Value.Y);
                //checks if there is a lebel on that cell. If there isnt it gives message you missed and places a pixturebox of a green bar in that cell
                if (c == null)
                {
                    System.Windows.Forms.MessageBox.Show("You missed");
                    var pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.Image = Properties.Resources.greenbar;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    enemyField.Controls.Add(pb, position.Value.X, position.Value.Y);
                }
                else
                {
                    //else it checks to see which ship is located there by checking the text of the label. Then it gives the appropriate message, it deletes the label in that cell and places a pixturebox of a red x
                    //it also increases the hits on that ship variable and checks if it is equal to the length of the ship. If it is it changes the destroyed property of the ship to true and gives a message 
                    if (c.Text == "Ae2")
                    {
                        ae2Hits++;
                        System.Windows.Forms.MessageBox.Show(" you hit | Aeroplanoforo");
                        var pb = new PictureBox();
                        pb.Dock = DockStyle.Fill;
                        pb.Image = Properties.Resources.redx;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        enemyField.Controls.Remove(enemyField.GetControlFromPosition(position.Value.X, position.Value.Y));
                        enemyField.Controls.Add(pb, position.Value.X, position.Value.Y);
                        if (ae2Hits == 5)
                        {
                            Aeroplanoforo2.destroyed = true;
                            System.Windows.Forms.MessageBox.Show("Βυθίσες το Αεροπλανοφόρο του!");
                        }

                    }
                    else if (c.Text == "An2")
                    {
                        an2Hits++;
                        System.Windows.Forms.MessageBox.Show("you hit | Antitorpiliko");
                        var pb = new PictureBox();
                        pb.Dock = DockStyle.Fill;
                        pb.Image = Properties.Resources.redx;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        enemyField.Controls.Remove(enemyField.GetControlFromPosition(position.Value.X, position.Value.Y));
                        enemyField.Controls.Add(pb, position.Value.X, position.Value.Y);
                        if (an2Hits == 4)
                        {
                            Antitorpiliko2.destroyed = true;
                            System.Windows.Forms.MessageBox.Show("Βύθισες το Αντιτορπιλικό του!");
                        }

                    }
                    else if (c.Text == "Po2")
                    {
                        po2Hits++;
                        System.Windows.Forms.MessageBox.Show("you hit | Polemiko");
                        var pb = new PictureBox();
                        pb.Dock = DockStyle.Fill;
                        pb.Image = Properties.Resources.redx;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        enemyField.Controls.Remove(enemyField.GetControlFromPosition(position.Value.X, position.Value.Y));
                        enemyField.Controls.Add(pb, position.Value.X, position.Value.Y);
                        if (po2Hits == 3)
                        {
                            Polemiko2.destroyed = true;
                            System.Windows.Forms.MessageBox.Show("Βύθισες το Πολεμικό του!");
                        }


                    }
                    else if (c.Text == "Yp2")
                    {
                        yp2Hits++;
                        System.Windows.Forms.MessageBox.Show("you hit | Ypovryxio");
                        var pb = new PictureBox();
                        pb.Dock = DockStyle.Fill;
                        pb.Image = Properties.Resources.redx;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        enemyField.Controls.Remove(enemyField.GetControlFromPosition(position.Value.X, position.Value.Y));
                        enemyField.Controls.Add(pb, position.Value.X, position.Value.Y);
                        if (yp2Hits == 2)
                        {
                            Ypovryxio2.destroyed = true;
                            System.Windows.Forms.MessageBox.Show("Βύθισες το Υποβρύχιο του!");
                        }

                    }
                }

                // if all the ships are destroyed it changes the win variable to 1, gives a message, increases the total wins number on this playing session and calls the gameover function
                if (Aeroplanoforo2.destroyed && Antitorpiliko2.destroyed && Polemiko2.destroyed && Ypovryxio2.destroyed)
                    {
                        win = 1;
                        System.Windows.Forms.MessageBox.Show("Νίκησες");
                        Settings.Default.wins++;
                        gameOver();
                }
                    else
                    {
                    //else it is the enemys turn to play by calling the enemyturn function. If it returns 1 the enemy has won, it gives a message, increases the total losses number on this playing session and calls the gameover function
                    int w = enemyTurn();
                        if (w == 1)
                        {
                            win = 2;
                            System.Windows.Forms.MessageBox.Show("Έχασες");
                            Settings.Default.losses++;
                            gameOver();

                    }
                    }
                


            }
            
        }

        private void gameOver()
        {
            //this function stops the timer, inserts into the database the information of this game and goes to the outro form to which it passes some information of this game needed for the outro screen.
            int wins = Settings.Default.wins;
            int losses = Settings.Default.losses;
            if (win == 1)
            {
                 winner = "Player";
            }
            else
            {
                 winner = "Enemy";
            }
            gameTimer.Stop();
            connection.Open();    
            SQLiteCommand command = new SQLiteCommand("insert into naumaxiaSt (username, winner, time) values (@username, @winner, @time)", connection);
            command.Parameters.AddWithValue("@username", user);
            command.Parameters.AddWithValue("@winner", winner);
            command.Parameters.AddWithValue("@time", timePassed);
            command.ExecuteNonQuery();
            connection.Close();

            this.Hide();
            Outro outro = new Outro(win, playert, timePassed, wins, losses, user);
            outro.ShowDialog();



        }


        //this function gets as input the enemy's panel and the point where the player clicked and returns the column and row as a new Point object or null if the click was outside the panel
        public Point? GetRowCol(TableLayoutPanel panel, Point point)
        {
            // If the point is outside  the panel, return null
            if (point.X > panel.Width || point.Y > panel.Height)
                return null;

            int paxos = 0;
            int ypsos = 0;
            int[] widths = panel.GetColumnWidths(), heights = panel.GetRowHeights();
            // Loop through the column widths and find the column that the point is in
            int i;
            for (i = 0; i < widths.Length && point.X > paxos; i++)
            {
                paxos += widths[i];
            }
            int col = i - 1;
            // Loop through the row heights and find the row that the point is in.
            for (i = 0; i < heights.Length && point.Y + panel.VerticalScroll.Value > ypsos; i++)
            {
                ypsos += heights[i];
            }
            int row = i - 1;
            // Return the column and row as a new Point object
            return new Point(col, row);
        }



        //this function chooses a random location  for the enemy to "select" checks if it has choosen that location already. If it has it gives a new one until it hasnt. Then does the same checks as the enemyfieldclick function
        private int enemyTurn()
        {
            //increases the enemy turns variable . Starts a do while loop to find new coordinates
            int col, row;
            enemyt++;
            bool aa = false;
            do
            {
                col = r.Next(0, 10);
                row = r.Next(0, 10);
                Control co = playersField.GetControlFromPosition(col, row);
                if (!(co is PictureBox))
                {
                    aa = true;
                }
            } while (!aa);

            Control c = playersField.GetControlFromPosition(col, row);
            if (c == null)
            {
                System.Windows.Forms.MessageBox.Show("Your opponent missed");
                var pb = new PictureBox();
                pb.Dock = DockStyle.Fill;
                pb.Image = Properties.Resources.greenbar;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                playersField.Controls.Add(pb, col, row);
            }
            else
            {
                if (c.Text== "Ae")
                {
                    aeHits++;
                    System.Windows.Forms.MessageBox.Show("Your opponent hit | Aeroplanoforo");
                    var pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.Image = Properties.Resources.redx;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    playersField.Controls.Remove(playersField.GetControlFromPosition(col, row));
                    playersField.Controls.Add(pb, col, row);
                    if (aeHits == 5)
                    {
                        Aeroplanoforo.destroyed = true;
                        System.Windows.Forms.MessageBox.Show("Βυθίστηκε το Αεροπλανοφόρο σου!");
                    }

                }
                else if (c.Text == "An")
                {
                    anHits++;
                    System.Windows.Forms.MessageBox.Show("Your opponent hit | Antitorpiliko");
                    var pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.Image = Properties.Resources.redx;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    playersField.Controls.Remove(playersField.GetControlFromPosition(col, row));
                    playersField.Controls.Add(pb, col, row);
                    if (anHits == 4)
                    {
                        Antitorpiliko.destroyed = true;
                        System.Windows.Forms.MessageBox.Show("Βυθίστηκε το Αντιτορπιλικό σου!");
                    }

                }
                else if (c.Text == "Po")
                {
                    poHits++;
                    System.Windows.Forms.MessageBox.Show("Your opponent hit | Polemiko");
                    var pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.Image = Properties.Resources.redx;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    playersField.Controls.Remove(playersField.GetControlFromPosition(col, row));
                    playersField.Controls.Add(pb, col, row);
                    if (poHits == 3)
                    {
                        Polemiko.destroyed = true;
                        System.Windows.Forms.MessageBox.Show("Βυθίστηκε το Πολεμικό σου!");
                    }


                }
                else if (c.Text == "Yp")
                {
                    ypHits++;
                    System.Windows.Forms.MessageBox.Show("Your opponent hit | Ypovryxio");
                    var pb = new PictureBox();
                    pb.Dock = DockStyle.Fill;
                    pb.Image = Properties.Resources.redx;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    playersField.Controls.Remove(playersField.GetControlFromPosition(col, row));
                    playersField.Controls.Add(pb, col, row);
                    if (ypHits == 2)
                    {
                        Ypovryxio.destroyed = true;
                        System.Windows.Forms.MessageBox.Show("Βυθίστηκε το Υποβρύχιο σου!");
                    }

                }
            }
            played = false;
            //changes the played property so the user can play now and returns 1 if the enemy won,  else 0
            if (Aeroplanoforo.destroyed && Antitorpiliko.destroyed && Polemiko.destroyed && Ypovryxio.destroyed)
            {
                return (1);
            }
            else
            {
                return (0);
            }


        }
        //closes the app
        private void Naumaxia_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
