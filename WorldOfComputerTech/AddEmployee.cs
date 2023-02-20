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
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;
using Image = System.Drawing.Image;

namespace WorldOfComputerTech
{
    public partial class AddEmployee : Form
    {
        public AddEmployee()
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
                                    if (Sql.Querry($@"SELECT Логин FROM Сотрудники WHERE Логин = N'{textBox4.Text}'") == textBox4.Text)
                                    {
                                        MessageBox.Show($@"Пользователь с таким логином уже существует!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    else
                                    {
                                        int IDRole = Sql.QuerryInt($"SELECT ID FROM Должность WHERE Наименование = N'{comboBox1.Text}'");

                                        if (Choose.DI == true)
                                        {
                                            DialogResult result = MessageBox.Show($"Вы не выбрали изображение!\nБудет установлено изобажение по умолчанию.\nЖелаете продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (result == DialogResult.Yes)
                                            {
                                                Sql.Querry($@"INSERT INTO Сотрудники(Фамилия, Имя, Отчество, ДатаРождения, Должность, Фото, Логин, Пароль) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', N'{textBox3.Text}', N'{dateTimePicker1.Value}', N'{IDRole}', N'DefaultProfilePhoto.jpg', N'{textBox4.Text}', N'{textBox5.Text}')");

                                                MessageBox.Show($@"Сотрудник был успешно добавлен!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            else if (result == DialogResult.No)
                                            {
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            Sql.Querry($@"INSERT INTO Сотрудники(Фамилия, Имя, Отчество, ДатаРождения, Должность, Фото, Логин, Пароль) VALUES (N'{textBox1.Text}', N'{textBox2.Text}', N'{textBox3.Text}', N'{dateTimePicker1.Value}', N'{IDRole}', N'{TopDeck.CurrentEmpPhotoName}', N'{textBox4.Text}', N'{textBox5.Text}')");

                                            MessageBox.Show($@"Сотрудник был успешно добавлен!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        DataTable GoDropRole;

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            GoDropRole = Sql.QuerryForTable(@"SELECT Наименование FROM Должность").Tables[0];

            comboBox1.DataSource = GoDropRole.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();


            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\ProfileImages\";

            pictureBox2.Image = Image.FromFile(appPath + "DefaultProfilePhoto.jpg");

            Choose.DI = true;
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