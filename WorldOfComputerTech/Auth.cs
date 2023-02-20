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
    public partial class Auth : Form
    {
        public Auth()
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

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox4.Visible = false;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Visible = true;
            pictureBox2.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show($@"Пожалуйста введите логин!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (textBox2.Text == String.Empty)
                {
                    MessageBox.Show($@"Пожалуйста введите пароль!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (Sql.Querry($@"Select Логин From Сотрудники where Логин = N'{textBox1.Text}'") != textBox1.Text)
                    {
                        MessageBox.Show($@"Пользователя с таким логином не зарегестрированно!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (Sql.Querry($@"Select Пароль From Сотрудники where Пароль = N'{textBox2.Text}'") != textBox2.Text)
                        {
                            MessageBox.Show($@"Пароль неверный!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            try
                            {
                                TopDeck.CurrentUserName = Sql.Querry($@"SELECT Фамилия + ' ' + Имя + ' ' + Отчество FROM Сотрудники WHERE Логин = N'{textBox1.Text}'");
                                TopDeck.CurrentUserRoleID = Sql.QuerryInt($@"SELECT Должность FROM Сотрудники WHERE Логин = N'{textBox1.Text}'");
                                TopDeck.CurrentUserRoleName = Sql.Querry($@"SELECT Наименование FROM Должность WHERE ID = N'{TopDeck.CurrentUserRoleID}'");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($@"{ex.Message}!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            MessageBox.Show($"Вы успешно авторизовались!" + $"\nВаша должность: {TopDeck.CurrentUserRoleName}", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           
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
            }
        }
        bool CanSee = false;
        private void label5_Click(object sender, EventArgs e)
        {
            string pass = textBox2.Text;

            if (CanSee == false)
            {
                Cursor.Current = Cursors.WaitCursor;
                textBox2.UseSystemPasswordChar = false;
                label5.Text = (@"👁");
                CanSee = true;
                Cursor.Current = Cursors.Default;
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                textBox2.UseSystemPasswordChar = true;
                label5.Text = (@"︶");
                CanSee = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void Auth_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
