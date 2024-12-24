using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp10;

namespace WindowsFormsApp10
{
    public partial class MainForm : Form
    {
        private LoginForm loginForm;
        private UserManager userManager;
        private string currentUsername;
        private MusicPlayer bgm;  // Biến lưu trữ nhạc nền
        private float currentVolume = 0.5f;  // Biến lưu trữ giá trị âm lượng
        public MainForm(LoginForm loginForm, UserManager userManager, string username)
        {
            //InitializeComponent();
            this.loginForm = loginForm;
            this.userManager = userManager;
            this.currentUsername = username; // Store the username
                                             // Đường dẫn tới file nhạc nền
            string musicPath = Application.StartupPath + @"\Resources\BauTroiMoi-DaLABMinhTocLam-16366284.mp3";
            Console.WriteLine(musicPath);  // Kiểm tra đường dẫn
            bgm = new MusicPlayer(musicPath);
            bgm.SetVolume(currentVolume);  // Cập nhật âm lượng khi khởi tạo

            // Phát nhạc khi form được tải lên
            this.Load += (s, e) => bgm.Play();

            // Dừng nhạc khi form đóng
            this.FormClosing += (s, e) => bgm.Stop();

            SetupMainForm();  // Or whatever your setup method is called
        }
        // Hàm này sẽ lưu giá trị âm lượng khi người dùng nhấn Save trong SettingsForm
        public void UpdateVolume(float volume)
        {
            currentVolume = volume;
            if (bgm != null)
            {
                bgm.SetVolume(volume);
            }
        }

        private void SetupMainForm()
        {
            this.Text = "Caro Game - Main Menu";
            this.Size = new Size(600, 400); // Set the desired size
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Caro Game",
                Font = new Font("Arial", 20, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(200, 30)
            };
            this.Controls.Add(titleLabel);

            // Play game button
            Button btnPlayGame = new Button
            {
                Text = "Play Game",
                Location = new Point(200, 100),
                Size = new Size(200, 40),
            };
            btnPlayGame.Click += (s, e) =>
            {
                GameForm gameForm = new GameForm(this, this.loginForm, this.userManager); // Truyền this vào đây
                this.Hide();
                gameForm.Show();
            };
            this.Controls.Add(btnPlayGame);

            // User profile button
            Button btnUserProfile = new Button
            {
                Text = "User Profile",
                Location = new Point(200, 160),
                Size = new Size(200, 40)
            };
            // In MainForm.cs
            btnUserProfile.Click += (s, e) =>
            {
                UserProfileForm userProfileForm = new UserProfileForm(this.userManager, this.currentUsername); // Pass the username
                this.Hide();
                userProfileForm.ShowDialog();
                this.Show();
            };
            this.Controls.Add(btnUserProfile);

            // Settings button
            Button btnSettings = new Button
            {
                Text = "Settings",
                Location = new Point(200, 220),
                Size = new Size(200, 40)
            };
            btnSettings.Click += (s, e) =>
            {
                // Mở SettingsForm và truyền giá trị âm lượng hiện tại vào
                SettingsForm settingsForm = new SettingsForm(this, bgm, currentVolume);
                this.Hide();
                settingsForm.ShowDialog();
                this.Show();
            };
            this.Controls.Add(btnSettings);

            // Exit button
            Button btnExit = new Button
            {
                Text = "Exit Game",
                Location = new Point(200, 280),
                Size = new Size(200, 40)
            };
            btnExit.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnExit);
        }
    }
}