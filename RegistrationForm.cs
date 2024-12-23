using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApp10
{
    public partial class RegistrationForm : Form
    {
        private UserManager userManager;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtEmail;
        private TextBox txtFullName;
        private Label lblUsername, lblPassword, lblEmail, lblFullName; // Declare the labels


        public RegistrationForm(UserManager userManager)
        {
            this.userManager = userManager;
            InitializeComponent();
        }



        private void StyleTextBox(TextBox textBox, string placeholder)
        {
            textBox.Font = new Font("Arial", 12);
            textBox.BorderStyle = BorderStyle.None; // Remove default border
            textBox.BackColor = Color.White;
            textBox.Width = 200;
            textBox.Multiline = false;


            // Placeholder text
            textBox.ForeColor = Color.LightGray; // Placeholder color
            textBox.Text = placeholder;
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black; // Normal text color
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
        private Button StyleButton(Button button, string buttonText)
        {
            button.Text = buttonText;
            button.Font = new Font("Arial", 12);
            button.BackColor = Color.FromArgb(0, 122, 255); // Apple-like blue
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Size = new Size(100, 35);
            button.Cursor = Cursors.Hand;


            // Rounded corners
            int cornerRadius = 10;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddArc(button.Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 277, 90);
            path.AddArc(button.Width - cornerRadius * 2, button.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddArc(0, button.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            button.Region = new Region(path);

            // Add hover effect (optional)
            button.MouseEnter += (s, e) => button.BackColor = Color.FromArgb(0, 100, 200); // Darker blue on hover
            button.MouseLeave += (s, e) => button.BackColor = Color.FromArgb(0, 122, 255); // Restore original color

            return button;
        }



        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Thực hiện đăng ký
            if (userManager.Register(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtFullName.Text))
            {
                MessageBox.Show("Đăng ký thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Vui lòng kiểm tra lại thông tin!");
            }
        }


        // ... (Other methods if needed)
    }

}

