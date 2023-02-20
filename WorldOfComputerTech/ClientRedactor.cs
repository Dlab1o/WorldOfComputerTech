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
    public partial class ClientRedactor : Form
    {
        public ClientRedactor()
        {
            InitializeComponent();
        }
        DataTable GoDropGender;
        DataTable GoDropStatus;
        private void ClientRedactor_Load(object sender, EventArgs e)
        {
            textBox1.Text = Sql.Querry($@"SELECT Фамилия FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            textBox2.Text = Sql.Querry($@"SELECT Имя FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            textBox3.Text = Sql.Querry($@"SELECT Отчество FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            dateTimePicker1.Value = Sql.QuerryDate($@"SELECT ДатаРождения FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            dateTimePicker1.Value = Sql.QuerryDate($@"SELECT ДатаПолучения FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            dateTimePicker1.Value = Sql.QuerryDate($@"SELECT ДатаОкончанияСрока FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            textBox4.Text = Sql.Querry($@"SELECT МестоРождения FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            textBox5.Text = Sql.Querry($@"SELECT Серия FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            textBox6.Text = Sql.Querry($@"SELECT Номер FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            textBox7.Text = Sql.Querry($@"SELECT МестоПолучения FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            //============================================================================================================

            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            GoDropGender = Sql.QuerryForTable(@"SELECT Наименование FROM Пол").Tables[0];

            comboBox1.DataSource = GoDropGender.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

            int Gender = Sql.QuerryInt($@"SELECT Пол FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            comboBox1.Text = Sql.Querry($@"SELECT Наименование FROM Пол WHERE ID = N'{Gender}'");

            //============================================================================================================


            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            GoDropStatus = Sql.QuerryForTable(@"SELECT Наименование FROM СтатусКлиента").Tables[0];

            comboBox2.DataSource = GoDropStatus.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

            int ClientStatus = Sql.QuerryInt($@"SELECT Статус FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");

            comboBox1.Text = Sql.Querry($@"SELECT Наименование FROM СтатусКлиента WHERE ID = N'{ClientStatus}'");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

                                            Sql.Querry($@"UPDATE Клиенты SET Имя = N'{textBox1.Text}', Фамилия = N'{textBox2.Text}', Отчество = N'{textBox3.Text}', ДатаРождения = N'{dateTimePicker1.Value}', МестоРождения = N'{textBox4.Text}', Серия = N'{textBox5.Text}', Номер = N'{textBox6.Text}', ДатаПолучения = N'{dateTimePicker1.Value}', ДатаОкончанияСрока = N'{dateTimePicker2.Value}', МестоПолучения = N'{textBox7.Text}', Пол = N'{IDGender}', Статус = N'{IDStatus}' WHERE ID = N'{TopDeck.ClientToCreative}'");

                                            MessageBox.Show($@"Клиент был успешно изменён!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Вы действительно хотите удалить текущего клиента?", "Подтверждение!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Sql.Querry($@"DELETE FROM Клиенты WHERE ID = N'{TopDeck.ClientToCreative}'");
                    MessageBox.Show($@"Клиент был успешно удалён!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //////////////////////////////
                    Admin addEmployee = new Admin();
                    this.Close();
                    addEmployee.Show();
                    //////////////////////////////
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"{ex.Message}!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (result == DialogResult.No)
            {
                return;
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
