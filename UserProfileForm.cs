using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic; // Add this for Dictionary

namespace WindowsFormsApp10
{
    public partial class UserProfileForm : Form
    {
        private UserManager userManager;
        private string currentUsername;

        public UserProfileForm(UserManager userManager, string username)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.currentUsername = username;
            DisplayUserProfile();


            this.Text = "User Profile";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

        }


        private void DisplayUserProfile()
        {
            Dictionary<string, string> userInfo = userManager.GetUserInfo(currentUsername);

            if (userInfo != null)
            {
                Label lblUsername = new Label { Text = "Tên đăng nhập: " + currentUsername, Location = new Point(50, 50), AutoSize = true };
                Label lblEmail = new Label { Text = "Email: " + userInfo["email"], Location = new Point(50, 80), AutoSize = true };
                Label lblFullName = new Label { Text = "Họ và tên: " + userInfo["fullName"], Location = new Point(50, 110), AutoSize = true };


                this.Controls.Add(lblUsername);
                this.Controls.Add(lblEmail);
                this.Controls.Add(lblFullName);
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng.");
            }
        }



    }
}