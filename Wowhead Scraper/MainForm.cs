using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wowhead_Scraper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void questControl1_Load(object sender, EventArgs e)
        {

        }

        private void generateIdRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdRangeFrm fm = new IdRangeFrm();
            fm.Show();
        }

        private void QuestControl1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
