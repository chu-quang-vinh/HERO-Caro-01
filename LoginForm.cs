using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using MetroFramework.Forms; // Import MetroFramework
using MetroFramework.Controls; // For Metro controls
using System.Drawing.Drawing2D; // For rounded corners


namespace WindowsFormsApp10
{
    public partial class LoginForm : MetroForm
    {
        private UserManager userManager;
        private string userFilePath = "users.json";
        private MetroFramework.Controls.MetroTextBox txtUsername;
        private MetroFramework.Controls.MetroTextBox txtPassword;
        private MetroFramework.Controls.MetroLabel lblUsername, lblPassword;
        private MetroFramework.Controls.MetroButton btnLogin;
        private MetroFramework.Controls.MetroButton btnRegister;

        public LoginForm()
        {

            InitializeComponent();
            userManager = new UserManager(userFilePath);

            // Style the buttons and add click event in LoginForm.cs (not in designer!)
            //btnLogin = StyleButton(btnLogin, "Đăng nhập");
            //btnLogin.Location = new Point(100, 185); // Example location
            //btnLogin.Click += BtnLogin_Click;

            //btnRegister = StyleButton(btnRegister, "Đăng ký");
            //btnRegister.Location = new Point(220, 185); // Example location
            //btnRegister.Click += BtnRegister_Click;


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
            // Open the registration form
            RegistrationForm registrationForm = new RegistrationForm(userManager);
            registrationForm.ShowDialog(); // Show modally so user must interact with it
        }
    }
}