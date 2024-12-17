
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chế_độ_cổ_điển
{
    // Project: Classic Challenge Mode for Caro Game (WinForms in C#)
    public partial class MainForm1 : Form
    {
        private Button[] levelButtons;
        private int currentLevel = 1;

        public MainForm1()
        {
            InitializeComponent();
            CenterControls();
            SetBackgroundImage();
        }
        private void SetBackgroundImage()
        {
            // Đặt hình nền cho Form từ file có sẵn trong Resources
            this.BackgroundImage = Image.FromFile(@"C:\Users\Admin\Downloads\tải xuống.jpg");

            // Tự động điều chỉnh kích thước hình nền
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void CenterControls()
        {
            // Tính toán vị trí trung tâm của form
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;
            int totalHeight = 0;


            // Căn giữa Label tiêu đề
            Label titleLabel = this.Controls.OfType<Label>().FirstOrDefault(l => l.Text == "Chế độ vượt thử thách");
            if (titleLabel != null)
            {
                titleLabel.Left = centerX - titleLabel.Width / 2;
                totalHeight += titleLabel.Height + 20; // Khoảng cách 20px
                titleLabel.Top = 20;
            }

            // Tính toán vị trí của các nút
            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].Left = centerX - levelButtons[i].Width / 2;
                levelButtons[i].Top = totalHeight + (i * (levelButtons[i].Height + 10)) + 20;
            }


            // Set lại kích thước form sau khi tính toán vị trí các nút
            this.ClientSize = new Size(600, levelButtons.Last().Bottom + 50);
        }

        private void InitializeComponent()
        {
            this.Text = "Chế độ vượt thử thách";
            this.Size = new Size(600, 400); // Kích thước tạm thời

            Label titleLabel = new Label
            {
                Text = "Chế độ vượt thử thách",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            levelButtons = new Button[5];
            for (int i = 0; i < 5; i++)
            {
                levelButtons[i] = new Button
                {
                    Text = $"Level {i + 1} {(i + 1 == currentLevel ? "***" : "")}",
                    Size = new Size(100, 40),
                };
                levelButtons[i].Click += (sender, e) => StartGame(i + 1);
                this.Controls.Add(levelButtons[i]);
            }
        }

        private void StartGame(int level)
        {
            //  if (level > currentLevel)
            //  {
            //      MessageBox.Show($"You need to complete Level {currentLevel} first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //      return;
            //  }

            Form gameForm;
            level = currentLevel;

            if (level == 1)
            {
                gameForm = new GameForm0(level, this);
            }
            else if (level == 2)
            {
                gameForm = new GameForm1(level, this);
            }
            else if (level == 3)
            {
                gameForm = new GameForm2(level, this);
            }
            else
            {
                MessageBox.Show("This level is not implemented yet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.Hide();
            gameForm.ShowDialog();
            this.Show();
        }

        public void UpdateLevel(int newLevel)
        {
            currentLevel = newLevel;
            for (int i = 0; i < 5; i++)
            {
                levelButtons[i].Text = $"Level {i + 1} {(i + 1 == currentLevel ? "***" : "")}";
            }
        }
    }
}