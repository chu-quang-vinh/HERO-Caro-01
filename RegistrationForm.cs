using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApp10
{
    public partial class RegistrationForm : MetroForm
    {
        private UserManager userManager;
        private MetroTextBox txtUsername, txtPassword, txtEmail, txtFullName;
        private MetroLabel lblUsername, lblPassword, lblEmail, lblFullName;

        public RegistrationForm(UserManager userManager)
        {
            this.userManager = userManager;
            InitializeComponent();
        }

        

        private void StyleTextBox(MetroTextBox textBox, string placeholder)
        {
            textBox.Font = new Font("Arial", 12);
            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.LightGray;
            textBox.Text = placeholder;
            textBox.UseSelectable = true;

            // Placeholder logic
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

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Perform registration
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
    }
}


