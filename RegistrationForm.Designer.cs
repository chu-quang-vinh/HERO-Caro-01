using System.Drawing.Drawing2D;
using System.Drawing;
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
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;


            lblUsername = new Label { Text = "Tên đăng nhập:", Location = new Point(50, 50) };
            lblPassword = new Label { Text = "Mật khẩu:", Location = new Point(50, 100) };
            lblEmail = new Label { Text = "Email:", Location = new Point(50, 150) };
            lblFullName = new Label { Text = "Họ và tên:", Location = new Point(50, 200) };



            txtUsername = new TextBox { Location = new Point(150, 50), Width = 150, Name = "txtUsername" };
            txtPassword = new TextBox { Location = new Point(150, 100), Width = 150, PasswordChar = '*', Name = "txtPassword" };
            txtEmail = new TextBox { Location = new Point(150, 150), Width = 150, Name = "txtEmail" };
            txtFullName = new TextBox { Location = new Point(150, 200), Width = 150, Name = "txtFullName" };

            // Style the textboxes
            StyleTextBox(txtUsername, "Tên đăng nhập");
            StyleTextBox(txtPassword, "Mật khẩu");
            StyleTextBox(txtEmail, "Email"); // Add placeholder for email
            StyleTextBox(txtFullName, "Họ và tên"); // Add placeholder for full name

            Button btnRegister = StyleButton(new Button(), "Đăng ký");
            btnRegister.Location = new Point(100, 250);
            Button btnCancel = StyleButton(new Button(), "Hủy");
            btnCancel.Location = new Point(220, 250);


            // Add click handlers for Register and Cancel buttons
            btnRegister.Click += BtnRegister_Click;
            btnCancel.Click += (s, e) => this.Close(); // Simply close the form


            // ... (Add other controls and styling as needed)
            Panel inputPanel = new Panel();
            inputPanel.Location = new Point(25, 40);
            inputPanel.Size = new Size(350, 205);
            inputPanel.BackColor = Color.FromArgb(240, 240, 240); // Light gray background for panel

            // Set rounded corners for the panel
            int cornerRadius = 10;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddArc(inputPanel.Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddArc(inputPanel.Width - cornerRadius * 2, inputPanel.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddArc(0, inputPanel.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            inputPanel.Region = new Region(path);

            // Add textboxes and labels to the panel
            inputPanel.Controls.Add(lblUsername);
            inputPanel.Controls.Add(txtUsername);

            inputPanel.Controls.Add(lblPassword);
            inputPanel.Controls.Add(txtPassword);

            inputPanel.Controls.Add(lblEmail); // Add email field to panel
            inputPanel.Controls.Add(txtEmail);

            inputPanel.Controls.Add(lblFullName); // Add full name field to panel
            inputPanel.Controls.Add(txtFullName);

            // Adjust label locations within the panel
            lblUsername.Location = new Point(15, 15);
            txtUsername.Location = new Point(135, 10);

            lblPassword.Location = new Point(15, 60);
            txtPassword.Location = new Point(135, 55);

            lblEmail.Location = new Point(15, 105); // Adjust locations
            txtEmail.Location = new Point(135, 100);

            lblFullName.Location = new Point(15, 150);
            txtFullName.Location = new Point(135, 145);

            this.Controls.Add(inputPanel);

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