
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MetroFramework.Controls; // For Metro controls

using MetroFramework.Forms; // Import MetroFramework
namespace WindowsFormsApp10
{
    partial class LoginForm : MetroForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;

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


        private void StyleTextBox(TextBox textBox, string placeholder)
        {
            textBox.Font = new Font("Arial", 12);
            this.BorderStyle = MetroFormBorderStyle.None;

            textBox.BackColor = Color.White;
            textBox.Width = 200;
            textBox.Multiline = false;



            textBox.ForeColor = Color.LightGray;
            textBox.Text = placeholder;
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };
            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.LightGray;
                }
            };

        }
        private MetroButton StyleButton(MetroButton button, string buttonText)
        {
            button.Text = buttonText;
            button.Font = new Font("Arial", 12);
            button.BackColor = Color.FromArgb(0, 122, 255); // Apple-like blue
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Size = new Size(120, 40);
            button.Cursor = Cursors.Hand;

            // Hover Effect
            button.MouseEnter += (s, e) => button.BackColor = Color.FromArgb(0, 100, 200);
            button.MouseLeave += (s, e) => button.BackColor = Color.FromArgb(0, 122, 255);

            return button;
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.lblUsername = new MetroFramework.Controls.MetroLabel();
            this.lblPassword = new MetroFramework.Controls.MetroLabel();
            this.txtUsername = new MetroFramework.Controls.MetroTextBox();
            this.txtPassword = new MetroFramework.Controls.MetroTextBox();
            this.btnLogin = new MetroFramework.Controls.MetroButton();
            this.btnRegister = new MetroFramework.Controls.MetroButton();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();

            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(67, 80);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(108, 19);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Tên đăng nhập:";

            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(67, 140);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(70, 19);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Mật khẩu:";
            // 
            // txtUsername
            // 
            // ... other properties as needed
            this.txtUsername.Lines = new string[] {
        "Tên đăng nhập"}; // Placeholder
            this.txtUsername.Location = new Point(200, 75);
            this.txtUsername.MaxLength = 32767;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PasswordChar = '\0';
            this.txtUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUsername.SelectedText = "";
            this.txtUsername.Size = new Size(200, 29);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.Text = "Tên đăng nhập";
            this.txtUsername.UseSelectable = true;

            // Add placeholder behavior
            this.txtUsername.Enter += (s, e) =>
            {
                if (this.txtUsername.Text == "Tên đăng nhập")
                {
                    this.txtUsername.Text = "";
                }
            };
            this.txtUsername.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(this.txtUsername.Text))
                {
                    this.txtUsername.Text = "Tên đăng nhập";
                }

            };

            // 
            // txtPassword
            // 
            // ... other properties as needed
            this.txtPassword.Lines = new string[] {
        "Mật khẩu"};
            this.txtPassword.Location = new Point(200, 135);

            this.txtPassword.MaxLength = 32767;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●'; // Use a password character
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new Size(200, 29);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "Mật khẩu";
            this.txtPassword.UseSelectable = true;

            this.txtPassword.Enter += (s, e) =>
            {
                if (this.txtPassword.Text == "Mật khẩu")
                {
                    this.txtPassword.Text = "";
                }
            };
            this.txtPassword.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(this.txtPassword.Text))
                {
                    this.txtPassword.Text = "Mật khẩu";
                }

            };

            // 
            // btnLogin
            // 
            this.btnLogin.Location = new Point(100, 190);  // Đẩy nút xuống thấp hơn
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(111, 42);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseSelectable = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);



            // 
            // btnRegister
            // 
            this.btnRegister.Location = new Point(300, 190);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new Size(107, 42);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Đăng ký";
            this.btnRegister.UseSelectable = true;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(505, 300);  // Tăng chiều cao form


            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);

            this.Name = "LoginForm";
            this.Text = "Đăng nhập";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Default;

            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





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
    
 