using MetroFramework.Controls;
using MetroFramework.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace WindowsFormsApp10
{
    partial class RegistrationForm
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
            this.Text = "Đăng ký";
            this.Size = new Size(450, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Theme = MetroFramework.MetroThemeStyle.Light;

            // Labels
            lblUsername = new MetroLabel { Text = "Tên đăng nhập:", Location = new Point(50, 100) };
            lblPassword = new MetroLabel { Text = "Mật khẩu:", Location = new Point(50, 150) };
            lblEmail = new MetroLabel { Text = "Email:", Location = new Point(50, 200) };
            lblFullName = new MetroLabel { Text = "Họ và tên:", Location = new Point(50, 250) };

            // MetroTextBox controls for user input
            txtUsername = new MetroTextBox { Location = new Point(150, 100), Width = 200 };
            txtPassword = new MetroTextBox { Location = new Point(150, 150), Width = 200, PasswordChar = '*' };
            txtEmail = new MetroTextBox { Location = new Point(150, 200), Width = 200 };
            txtFullName = new MetroTextBox { Location = new Point(150, 250), Width = 200 };

            // Style the textboxes (set placeholder)
            StyleTextBox(txtUsername, "Tên đăng nhập");
            StyleTextBox(txtPassword, "Mật khẩu");
            StyleTextBox(txtEmail, "Email");
            StyleTextBox(txtFullName, "Họ và tên");

            // MetroButton for Register
            MetroButton btnRegister = new MetroButton { Text = "Đăng ký", Size = new Size(100, 40), Location = new Point(100, 330) };
            MetroButton btnCancel = new MetroButton { Text = "Hủy", Size = new Size(100, 40), Location = new Point(220, 330) };

            // Button Click Events
            btnRegister.Click += BtnRegister_Click;
            btnCancel.Click += (s, e) => this.Close();

            // Add controls to the form
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblFullName);
            this.Controls.Add(txtFullName);
            this.Controls.Add(btnRegister);
            this.Controls.Add(btnCancel);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        //private void InitializeComponent()
        //{
        //    this.components = new System.ComponentModel.Container();
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(800, 450);
        //    this.Text = "RegistrationForm";
        //}

        #endregion
    }
}