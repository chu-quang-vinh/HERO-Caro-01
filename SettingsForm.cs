using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class SettingsForm : Form
    {
        private LoginForm loginForm;
        private UserManager userManager;
        private MusicPlayer bgm;  // Đối tượng MusicPlayer để điều chỉnh nhạc nền
        private float initialVolume;
        private MainForm mainForm; // Thêm một biến để lưu trữ đối tượng MainForm
        public SettingsForm(MainForm mainForm, MusicPlayer bgm, float initialVolume)
        {
            InitializeComponent();
            this.mainForm = mainForm;  // Lưu đối tượng MainForm
            this.bgm = bgm;
            this.initialVolume = initialVolume;
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

            // TrackBar để điều chỉnh âm lượng
            TrackBar trackVolume = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = (int)(initialVolume * 100),
                Location = new Point(120, 90),
                Width = 200
            };
            trackVolume.Scroll += (s, e) =>
            {
                if (bgm != null)
                {
                    float volume = trackVolume.Value / 100f; // Chuyển giá trị từ 0.0f đến 1.0f
                    bgm.SetVolume(volume); // Điều chỉnh âm lượng
                }
                else
                {
                    MessageBox.Show("Music player is not initialized!");
                }
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
                // Kiểm tra bgm không null trước khi lưu âm lượng
                if (bgm != null)
                {
                    float volume = trackVolume.Value / 100f;
                    bgm.SetVolume(volume);
                    // Sử dụng mainForm đã truyền vào để gọi hàm UpdateVolume
                    mainForm.UpdateVolume(volume);
                    MessageBox.Show("Settings saved!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Music player is not initialized!");
                }
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
