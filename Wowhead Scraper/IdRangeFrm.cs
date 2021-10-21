using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Wowhead_Scraper
{
    public partial class IdRangeFrm : Form
    {
        List<string> rangeFile = new List<string>();

        public IdRangeFrm()
        {
            InitializeComponent();
        }

        private void IdRangeFrm_Load(object sender, EventArgs e)
        {
            lblStatus.Parent = progressBar1;
            lblStatus.BackColor = Color.Transparent;
        }

        private void Get_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder str = new MySqlConnectionStringBuilder();
            str.Server = txtServer.Text;
            str.Port = Convert.ToUInt32(txtPort.Text);
            str.UserID = txtUser.Text;
            str.Password = txtPass.Text;
            str.Database = txtWorld.Text;

            MySqlConnection conn = new MySqlConnection(str.ToString());
            MySqlCommand command = conn.CreateCommand();
            conn.Open();

            command.CommandText = "SELECT "+ txtRowName.Text +" FROM "+ txtTable.Text +"";
            MySqlDataReader reader = command.ExecuteReader(); 
            while (reader.Read())
            {
                rangeFile.Add(reader.GetInt32(0).ToString());
            }

            SaveFile();
        }

        private void SaveFile()
        {
            string filename = "range-" + txtTable.Text + "-"+ txtRowName.Text +".rng";
            if (!System.IO.File.Exists(filename))
            {
                System.IO.File.WriteAllLines(filename, rangeFile);
                MessageBox.Show("File: " + filename + " Saved"); 
            }
            else
            {
                if (MessageBox.Show(this, "You already have a file with the same name, Do you want to rewrite the file?", "Alert", MessageBoxButtons.YesNo) == DialogResult.Yes) ;
                {
                    System.IO.File.WriteAllLines(filename, rangeFile);
                    MessageBox.Show("File: " + filename + " Saved"); 
                }
            }
        }
    }
}
