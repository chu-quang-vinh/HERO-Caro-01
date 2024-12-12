using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO; // Thêm thư viện IO

namespace WindowsFormsApp10
{
    public partial class LoginForm : Form
    {
        private UserManager userManager;

        private string userFilePath = "users.json";
        public LoginForm()
        {
            InitializeComponent();
            userManager = new UserManager(userFilePath); // Quản lý thông tin người dùng
        }



        // Add UserManager class
        
    }
}
