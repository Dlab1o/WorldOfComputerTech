using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WorldOfComputerTech
{
    public partial class EmployeeEdit : Form
    {
        public EmployeeEdit()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (TopDeck.CheckAllSpace(textBox1.Text) == true)
            {
                MessageBox.Show($@"Пожалуйста введите Фамилию!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (TopDeck.CheckAllSpace(textBox2.Text) == true)
                {
                    MessageBox.Show($@"Пожалуйста введите Имя!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (TopDeck.CheckAllSpace(textBox3.Text) == true)
                    {
                        MessageBox.Show($@"Пожалуйста введите Отчество!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (dateTimePicker1.Value == DateTime.Today)
                        {
                            MessageBox.Show($@"Указанна дата сегодняшнего дня!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            if (TopDeck.CheckAllSpace(textBox4.Text) == true)
                            {
                                MessageBox.Show($@"Пожалуйста введите Логин!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                if (TopDeck.CheckAllSpace(textBox5.Text) == true)
                                {
                                    MessageBox.Show($@"Пожалуйста введите Пароль!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    if (Sql.Querry($@"SELECT Логин FROM Сотрудники WHERE Логин = N'{textBox4.Text}' and ID != N'{TopDeck.EmpToEditID}'") == textBox4.Text)
                                    {
                                        MessageBox.Show($@"Пользователь с таким логином уже существует!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    else
                                    {
                                        int IDRole = Sql.QuerryInt($"SELECT ID FROM Должность WHERE Наименование = N'{comboBox1.Text}'");

                                        Sql.Querry($@"UPDATE Сотрудники SET Имя = N'{textBox1.Text}', Фамилия = N'{textBox2.Text}', Отчество = N'{textBox3.Text}', ДатаРождения = N'{dateTimePicker1.Value}', Должность = N'{IDRole}', Фото = N'{TopDeck.CurrentEmpPhotoName}', Логин = N'{textBox4.Text}', Пароль = N'{textBox5.Text}' WHERE ID = N'{TopDeck.EmpToEditID}'");
                                        MessageBox.Show($@"Данные пользователя успешно изменены!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
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
        DataTable GoDropRole;
        private void EmployeeEdit_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            GoDropRole = Sql.QuerryForTable(@"SELECT Наименование FROM Должность").Tables[0];

            comboBox1.DataSource = GoDropRole.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

            int IDRole = Sql.QuerryInt($@"SELECT Должность FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'");

            comboBox1.Text = Sql.Querry($@"SELECT Наименование FROM Должность WHERE ID = N'{IDRole}'");

            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\ProfileImages\";

            pictureBox2.Image = Image.FromFile(appPath + Sql.Querry($@"SELECT Фото FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'"));

            TopDeck.CurrentEmpPhotoName = Sql.Querry($@"SELECT Фото FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'");

            Choose.DI = true;

            //======================================================================

            textBox1.Text = Sql.Querry($@"SELECT Фамилия FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'");

            textBox2.Text = Sql.Querry($@"SELECT Имя FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'");

            textBox3.Text = Sql.Querry($@"SELECT Отчество FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'");

            dateTimePicker1.Value = Sql.QuerryDate($@"SELECT ДатаРождения FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'");

            textBox4.Text = Sql.Querry($@"SELECT Логин FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'");

            textBox5.Text = Sql.Querry($@"SELECT Пароль FROM Сотрудники WHERE ID = N'{TopDeck.EmpToEditID}'");

        }

        class Choose
        {
            public static bool DI = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFile = new OpenFileDialog();
            opFile.Title = "Ваше изображение...";
            opFile.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";

            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\ProfileImages\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }

            if (opFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string iName = opFile.SafeFileName;
                    string filepath = opFile.FileName;
                    File.Copy(filepath, appPath + iName);
                    pictureBox2.Image = new Bitmap(opFile.OpenFile());
                    TopDeck.CurrentEmpPhotoName = iName;
                    Choose.DI = false;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                }
            }
            else
            {
                opFile.Dispose();
            }
        }
    }
}
