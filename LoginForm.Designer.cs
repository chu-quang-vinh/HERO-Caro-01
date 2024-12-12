using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Text = "Đăng nhập";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblUsername = new Label { Text = "Tên đăng nhập:", Location = new Point(50, 50) };
            Label lblPassword = new Label { Text = "Mật khẩu:", Location = new Point(50, 100) };

            TextBox txtUsername = new TextBox { Location = new Point(150, 50), Width = 150 };
            TextBox txtPassword = new TextBox { Location = new Point(150, 100), Width = 150, PasswordChar = '*' };

            Button btnLogin = new Button { Text = "Đăng nhập", Location = new Point(150, 150) };
            Button btnRegister = new Button { Text = "Đăng ký", Location = new Point(250, 150) };

            btnLogin.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống!");
                    return;
                }
                if (userManager.Login(txtUsername.Text, txtPassword.Text))
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    // Mở form chơi game
                    
                    GameForm gameForm = new GameForm(this); // Truyền "this" (LoginForm) vào constructor của GameForm
                    this.Hide();
                    gameForm.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            };

            btnRegister.Click += (s, e) => {
                if (userManager.Register(txtUsername.Text, txtPassword.Text))
                {
                    MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập.");
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                }
            };

            this.Controls.Add(lblUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtUsername);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnRegister);
        }
    }
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    

        #endregion
    
}