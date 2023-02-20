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
    public partial class Director : Form
    {
        public Director()
        {
            InitializeComponent();
        }

        private void Director_Load(object sender, EventArgs e)
        {
            label1.Text = TopDeck.CurrentUserName;

            dataGridView1.DataSource = Sql.QuerryForTable($"Select * from Сотрудники").Tables[0];

            dataGridView1.Columns[0].Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            this.Hide();
            addEmployee.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TopDeck.CheckAllSpace(textBox1.Text) == true || textBox1.Text == String.Empty)
            {
                MessageBox.Show($@"Пожалуйста введите Логин пользователя для редактирования!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (Sql.Querry($@"Select Логин From Сотрудники where Логин = N'{textBox1.Text}'") != textBox1.Text)
                {
                    MessageBox.Show($"Пользователя с таким логином не существует!\nПожалуйста, введите один из предложеных!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    TopDeck.EmpToEditID = Sql.QuerryInt($@"Select ID From Сотрудники where Логин = N'{textBox1.Text}'");

                    EmployeeEdit employeeEdit = new EmployeeEdit();
                    this.Close();
                    employeeEdit.Show();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TopDeck.CheckAllSpace(textBox1.Text) == true || textBox1.Text == String.Empty)
            {
                MessageBox.Show($@"Пожалуйста введите Логин пользователя для удаления!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (Sql.Querry($@"Select Логин From Сотрудники where Логин = N'{textBox1.Text}'") != textBox1.Text)
                {
                    MessageBox.Show($"Пользователя с таким логином не существует!\nПожалуйста, введите один из предложеных!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Sql.Querry($@"DELETE FROM Сотрудники WHERE Логин = N'{textBox1.Text}'");

                    MessageBox.Show($"Пользователь был успешно удалён!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Sql.QuerryForTable($"Select * from Сотрудники").Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    textBox1.Text = String.Empty;
                    return;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var FilterX = "Логин";
            var filter = string.Format(FilterX + " like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
            try
            {
                ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = filter;
            }
            catch (Exception) { }
        }
    }
}
