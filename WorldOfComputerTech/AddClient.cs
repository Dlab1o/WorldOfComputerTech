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
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
        }
        DataTable Gender;
        DataTable Status;
        private void AddClient_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Gender = Sql.QuerryForTable(@"SELECT Наименование FROM Пол").Tables[0];

            comboBox1.DataSource = Gender.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

            //======================================================================================

            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Status = Sql.QuerryForTable(@"SELECT Наименование FROM СтатусКлиента").Tables[0];

            comboBox2.DataSource = Status.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TopDeck.CheckAllSpace(textBox1.Text) == true)
            {
                MessageBox.Show($@"Пожалуйста, введите Фамилию!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (TopDeck.CheckAllSpace(textBox2.Text) == true)
                {
                    MessageBox.Show($@"Пожалуйста, введите Имя!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (TopDeck.CheckAllSpace(textBox3.Text) == true)
                    {
                        MessageBox.Show($@"Пожалуйста, введите Отчество!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (TopDeck.CheckAllSpace(textBox4.Text) == true)
                        {
                            MessageBox.Show($@"Пожалуйста, укажите место рождения!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            if (TopDeck.CheckAllSpace(textBox5.Text) == true || textBox5.Text.Length < 4)
                            {
                                MessageBox.Show($@"Пожалуйста, укажите серию паспорта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                if (TopDeck.CheckAllSpace(textBox6.Text) == true || textBox6.Text.Length < 6)
                                {
                                    MessageBox.Show($@"Пожалуйста, укажите номер паспорта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    if (TopDeck.CheckAllSpace(textBox7.Text) == true)
                                    {
                                        MessageBox.Show($@"Пожалуйста, укажите орган выдавший документ!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            int IDGender = Sql.QuerryInt($"SELECT ID FROM Пол WHERE Наименование = N'{comboBox1.Text}'");
                                            int IDStatus = Sql.QuerryInt($"SELECT ID FROM СтатусКлиента WHERE Наименование = N'{comboBox2.Text}'");
                                            Sql.Querry($@"INSERT INTO Клиенты(Фамилия, Имя, Отчество, ДатаРождения, МестоРождения, Серия, Номер, ДатаПолучения, ДатаОкончанияСрока, МестоПолучения, Пол, Статус) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', N'{textBox3.Text}', N'{dateTimePicker1.Value}', N'{textBox4.Text}', N'{textBox5.Text}', N'{textBox6.Text}', N'{dateTimePicker2.Value}', N'{dateTimePicker3.Value}', N'{textBox7.Text}', N'{IDGender}', N'{IDStatus}')");
                                            MessageBox.Show($@"Клиент был успешно добавлен!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($@"{ex.Message}!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (TopDeck.CurrentUserRoleID)
            {
                case 1:
                    //Директор

                    Director Dir = new Director();
                    this.Hide();
                    Dir.Show();

                    break;
                case 2:
                    //Сборщик

                    Assembler assemb = new Assembler();
                    this.Hide();
                    assemb.Show();

                    break;
                case 3:
                    //Менеджер-кассир

                    Manager man = new Manager();
                    this.Hide();
                    man.Show();

                    break;
                case 4:
                    //Администратор

                    Admin admin = new Admin();
                    this.Hide();
                    admin.Show();

                    break;
            }
        }
    }
}
