using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorldOfComputerTech
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        DataTable GoDropRole;
        private void Admin_Load(object sender, EventArgs e)
        {
            label1.Text = TopDeck.CurrentUserName;

            dataGridView1.DataSource = Sql.QuerryForTable($"Select * from Сотрудники").Tables[0];
            dataGridView2.DataSource = Sql.QuerryForTable($"Select * from Клиенты").Tables[0];

            dataGridView1.Columns[0].Visible = false;
            dataGridView2.Columns[0].Visible = false;


            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            GoDropRole = Sql.QuerryForTable(@"SELECT COLUMN_NAME FROM DB_UP02.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'Клиенты' AND COLUMN_NAME != 'ID'").Tables[0];

            comboBox1.DataSource = GoDropRole.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            this.Hide();
            addEmployee.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var FilterX = comboBox1.Text;

            if (TopDeck.CheckAllSpace(textBox2.Text) == true || textBox2.Text == String.Empty)
            {
                MessageBox.Show($@"Пожалуйста введите корректные данные пользователя!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (Sql.Querry($@"Select {comboBox1.Text} From Клиенты where {comboBox1.Text} = N'{textBox2.Text}'") != textBox2.Text)
                {
                    MessageBox.Show($"Пользователя с такими данными не существует!\nПожалуйста, вводите информацию корректно!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    TopDeck.ClientToCreative = Sql.QuerryInt($@"Select ID From Клиенты where {comboBox1.Text} = N'{textBox2.Text}'");

                    ClientRedactor addEmployee = new ClientRedactor();
                    this.Close();
                    addEmployee.Show();

                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var FilterX = comboBox1.Text;
            var filter = string.Format(FilterX + " like '%{0}%'", textBox2.Text.Trim().Replace("'", "''"));
            try
            {
                ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = filter;
            }
            catch (Exception) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddClient employeeEdit = new AddClient();
            this.Close();
            employeeEdit.Show();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
