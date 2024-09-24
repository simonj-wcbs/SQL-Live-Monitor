using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data.Odbc;
using SQLMonitor;

namespace SQLMonitor
{
    public partial class TempDBInfo : Form
    {
        public OdbcConnection ProcCon;
        private OdbcCommand MyCom;
        private OdbcDataAdapter Ad;
        private DataSet ds;
        public bool pre2005;
        private CustomProgressBar progressBar1;

        public TempDBInfo(bool version)
        {
            InitializeComponent();
            pre2005 = version;
            cmbCategory.Text = "Summary";
        }

        private void RenderInfo(string Category)
        {
            // Keep existing RenderInfo code
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Keep existing button1_Click code
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RenderInfo(cmbCategory.Text);
        }

        private void TempDBInfo_Load(object sender, EventArgs e)
        {
            RenderInfo(cmbCategory.Text);
        }
    }
}
