using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ergasia1
{
    public partial class Stats : Form
    {

        string user;
        SQLiteConnection connection;
        public Stats(string un)
        {
            InitializeComponent();
            user = un;
        }

        private void Stats_Load(object sender, EventArgs e)
        {
            //connects to the database, reads all the data with the username of the last player, puts them in a datatable and then gives that datatable as a data source for the dataGridView
            connection = new SQLiteConnection("Data Source=NaumaxiaStats.db;Version=3;");
            connection.Open();

            SQLiteDataReader reader;
            SQLiteCommand command;
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM naumaxiaSt WHERE username = @user";
            command.Parameters.Add("@user", DbType.String).Value = user;


            reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Username");
            dataTable.Columns.Add("Winner");
            dataTable.Columns.Add("Time Played");
            while (reader.Read())
            {
                DataRow row = dataTable.NewRow();
                row["Username"] = reader["username"];
                row["Winner"] = reader["winner"];
                row["Time Played"] = reader["time"];
                dataTable.Rows.Add(row);
            }
            dataGridView1.DataSource = dataTable;
            


            connection.Close();

        }
        //back to menu
        private void returnbtn_Click(object sender, EventArgs e)
        {

            this.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();

        }
    }
}
