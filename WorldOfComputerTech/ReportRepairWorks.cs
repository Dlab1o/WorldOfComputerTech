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
    public partial class ReportRepairWorks : Form
    {
        public ReportRepairWorks()
        {
            InitializeComponent();
        }

        private void ReportRepairWorks_Load(object sender, EventArgs e)
        {
            dataGridView4.DataSource = Sql.QuerryForTable($"Select * from УчётРемонтныхРабот").Tables[0];

            dataGridView4.Columns[0].Visible = false;
        }
    }
}
