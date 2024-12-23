using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class SettingsForm : Form
    {
        private LoginForm loginForm;
        private UserManager userManager;
        public SettingsForm(UserManager userManager) // Constructor nhận UserManager
        {
            InitializeComponent();
            this.userManager = userManager; // Assign the UserManager
            SetupSettingsForm();
        }

        private void SetupSettingsForm()
        {
            this.Text = "Settings";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Settings",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(140, 30),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            Label lblVolume = new Label
            {
                Text = "Volume",
                Location = new Point(50, 100),
                AutoSize = true
            };
            this.Controls.Add(lblVolume);

            TrackBar trackVolume = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 50,
                Location = new Point(120, 90),
                Width = 200
            };
            this.Controls.Add(trackVolume);

            Button btnSave = new Button
            {
                Text = "Save",
                Location = new Point(100, 200),
                Size = new Size(100, 40)
            };
            btnSave.Click += (s, e) =>
            {
                MessageBox.Show("Settings saved!");
                this.Close();
            };
            this.Controls.Add(btnSave);

            Button btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(220, 200),
                Size = new Size(100, 40)
            };
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);
        }
    }
}
