
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp10;

namespace chế_độ_cổ_điển
{
    // Project: Classic Challenge Mode for Caro Game (WinForms in C#)
    public partial class MainForm1 : Form
    {
        private Button[] levelButtons;
        private int currentLevel = 1;
        private UserManager userManager;
        private string currentUsername;

        public MainForm1(UserManager userManager, string username)
        {
            this.userManager = userManager;
            this.currentUsername = username;
            InitializeComponent();
            CenterControls();
            // Gọi SetFormBackground với tên file ảnh trong thư mục Resources
            UIManager.SetFormBackground(this, "back_normal.jpg");

            //levelButtons = new Button[5];
            //for (int i = 0; i < 5; i++)
            //{
            //    levelButtons[i] = UIManager.CreateImageButton(
            //        "level1.png",      // Nút bình thường
            //        "level_hover.png", // Nút hover
            //        150, 60
            //    );
            //    levelButtons[i].Click += (sender, e) => StartGame(i + 1);
            //    this.Controls.Add(levelButtons[i]);
            //}
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
            this.Size = new Size(800, 600); // Đặt kích thước form

            // Đặt form ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            // Đặt hình nền
            UIManager.SetFormBackground(this, "back_normal.jpg");

            // Tạo tiêu đề
            Label titleLabel = new Label
            {
                Text = "Chế Độ Vượt Thử Th  ách",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(titleLabel);

            // Khởi tạo mảng nút level
            levelButtons = new Button[5];

            for (int i = 0; i < 5; i++)
            {
                // Tạo nút cho mỗi level
                levelButtons[i] = UIManager.CreateImageButton(
                    "level1.png",        // Ảnh mặc định
                    "level_hover.png",   // Ảnh hover
                    150, 60
                );

                // Hiển thị text trên nút
                levelButtons[i].Text = $"Level {i + 1}";
                levelButtons[i].Font = new Font("Arial", 14, FontStyle.Bold);
                levelButtons[i].ForeColor = Color.Black;

                // Gán sự kiện click cho mỗi nút
                levelButtons[i].Click += (sender, e) => StartGame(i + 1);

                // Thêm nút vào form
                this.Controls.Add(levelButtons[i]);
            }

            // Căn giữa các thành phần
            CenterControls();
        }


        private void StartGame(int level)
        {
            Form gameForm;
            level = currentLevel;

            if (level == 1)
            {
                gameForm = new GameForm0(level, this, userManager, currentUsername);
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
            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].Text = $"Level {i + 1} {(i + 1 <= currentLevel ? "***" : "")}";
            }
        }



    }
}