using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp10
{
    public partial class LoginForm : Form
    {
        private UserManager userManager;
        private string userFilePath = "users.json";

        public LoginForm()
        {
            InitializeComponent();
            userManager = new UserManager(userFilePath);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống!");
                return;
            }

            if (userManager.Login(txtUsername.Text, txtPassword.Text))
            {
                MessageBox.Show("Đăng nhập thành công!");
                MainForm mainForm = new MainForm(this, userManager, txtUsername.Text);
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (userManager.Register(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtFullName.Text))
            {
                MessageBox.Show("Đăng ký thành công!");
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Vui lòng kiểm tra lại thông tin!");
            }
        }
    }
}