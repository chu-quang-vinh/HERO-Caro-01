
using System;
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

        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtEmail;
        private TextBox txtFullName;

        private void InitializeComponent()
        {
            this.Text = "Đăng nhập";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblUsername = new Label { Text = "Tên đăng nhập:", Location = new Point(50, 50) };
            Label lblPassword = new Label { Text = "Mật khẩu:", Location = new Point(50, 100) };
            Label lblEmail = new Label { Text = "Email:", Location = new Point(50, 150) };
            Label lblFullName = new Label { Text = "Họ và tên:", Location = new Point(50, 200) };

            txtUsername = new TextBox { Location = new Point(150, 50), Width = 150, Name = "txtUsername" };
            txtPassword = new TextBox { Location = new Point(150, 100), Width = 150, PasswordChar = '*', Name = "txtPassword" };
            txtEmail = new TextBox { Location = new Point(150, 150), Width = 150, Name = "txtEmail" };
            txtFullName = new TextBox { Location = new Point(150, 200), Width = 150, Name = "txtFullName" };

            Button btnLogin = new Button { Text = "Đăng nhập", Location = new Point(150, 250), Name = "btnLogin" };
            Button btnRegister = new Button { Text = "Đăng ký", Location = new Point(250, 250) };

            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;

            this.Controls.Add(lblUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(lblEmail);
            this.Controls.Add(lblFullName);
            this.Controls.Add(txtUsername);
            this.Controls.Add(txtPassword);
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtFullName);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnRegister);


            //    private void BtnRegister_Click(object sender, EventArgs e)
            //    {
            //        // Thực hiện đăng ký
            //        if (userManager.Register(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtFullName.Text))
            //        {
            //            MessageBox.Show("Đăng ký thành công!");
            //        }
            //        else
            //        {
            //            MessageBox.Show("Đăng ký thất bại. Vui lòng kiểm tra lại thông tin!");
            //        }
            //    }



            //    private void BtnLogin_Click(object sender, EventArgs e)
            //    {
            //        if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            //        {
            //            MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống!");
            //            return;
            //        }

            //        if (userManager.Login(txtUsername.Text, txtPassword.Text))
            //        {
            //            MessageBox.Show("Đăng nhập thành công!");
            //            // Mở form chính và truyền userManager vào MainForm
            //            MainForm mainForm = new MainForm(this, userManager, txtUsername.Text); // Pass username
            //            this.Hide();
            //            mainForm.Show();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
            //        }
            //    }
            //}
            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>


            #endregion
        }
    }
}
    
 