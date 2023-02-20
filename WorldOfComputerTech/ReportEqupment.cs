using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldOfComputerTech
{
    public partial class ReportEqupment : Form
    {
        public ReportEqupment()
        {
            InitializeComponent();
        }

        private void ReportEqupment_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Sql.QuerryForTable($"Select * from ЗаказМатериалов").Tables[0];

            dataGridView1.Columns[0].Visible = false;
        }
    }
}
