//using System;
//using System.Drawing;
//using System.Windows.Forms;
//using System.Linq;

//namespace chế_độ_cổ_điển
//{
//    public partial class MainForm1 : Form
//    {
//        private Button[] levelButtons;
//        private int currentLevel = 1;
//        private const int MaxLevel = 5; // Hằng số cho số level tối đa

//        public MainForm1()
//        {
//            InitializeComponent();
//            SetBackgroundImage(); // Gọi trước khi căn giữa để lấy kích thước ảnh nền
//            CenterControls();
//        }

//        private void SetBackgroundImage()
//        {
//            try
//            {
//                this.BackgroundImage = Image.FromFile(@"C:\Users\Admin\Pictures\anhnen.jpg");
//                this.BackgroundImageLayout = ImageLayout.Stretch;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Không thể tải hình nền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void CenterControls()
//        {
//            int centerX = this.ClientSize.Width / 2;
//            int currentY = 20; // Vị trí Y bắt đầu

//            // Căn giữa Label tiêu đề
//            Label titleLabel = this.Controls.OfType<Label>().FirstOrDefault(l => l.Text == "Chế độ vượt thử thách");
//            if (titleLabel != null)
//            {
//                titleLabel.Location = new Point(centerX - titleLabel.Width / 2, currentY);
//                currentY += titleLabel.Height + 20;
//            }

//            // Tạo và căn giữa các nút level
//            levelButtons = new Button[MaxLevel];
//            for (int i = 0; i < MaxLevel; i++)
//            {
//                levelButtons[i] = new Button
//                {
//                    Text = $"Level {i + 1} {(i + 1 == currentLevel ? "***" : "")}",
//                    Size = new Size(100, 40),
//                    Location = new Point(centerX - 50, currentY) // Căn giữa nút
//                };
//                levelButtons[i].Click += (sender, e) => StartGame(i + 1);
//                this.Controls.Add(levelButtons[i]);
//                currentY += levelButtons[i].Height + 10;
//            }

//            // Điều chỉnh kích thước form dựa trên vị trí của control cuối cùng
//            this.ClientSize = new Size(this.ClientSize.Width, currentY + 30); // Thêm padding
//        }

//        private void InitializeComponent()
//        {
//            this.Text = "Chế độ vượt thử thách";
//            this.Size = new Size(600, 400); // Kích thước ban đầu (sẽ được điều chỉnh lại)

//            Label titleLabel = new Label
//            {
//                Text = "Chế độ vượt thử thách",
//                Font = new Font("Arial", 16, FontStyle.Bold),
//                AutoSize = true
//            };
//            this.Controls.Add(titleLabel);
//        }
//        public void UpdateLevel(int newLevel)
//        {
//            currentLevel = Math.Min(newLevel, MaxLevel); // Giới hạn currentLevel
//            for (int i = 0; i < levelButtons.Length; i++)
//            {
//                levelButtons[i].Text = $"Level {i + 1} {(i + 1 == currentLevel ? "***" : "")}";
//            }
//        }
//            private void StartGame(int level)
//        {
//            // Xóa bỏ phần kiểm tra level > currentLevel vì đã có UpdateLevel xử lý

//            Form gameForm = null; // Khởi tạo gameForm là null
//            switch (level) // Sử dụng switch để dễ dàng thêm level sau này
//            {
//                case 1:
//                    gameForm = new GameForm0(level, this);
//                    break;
//                case 2:
//                    gameForm = new GameForm1(level, this);
//                    break;
//                case 3:
//                    gameForm = new GameForm2(level, this);
//                    break;
//                default:
//                    MessageBox.Show("Màn chơi này chưa được thiết kế.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    return;
//            }

//            if (gameForm != null) // Kiểm tra gameForm trước khi sử dụng
//            {
//                this.Hide();
//                gameForm.ShowDialog();
//                this.Show();
                
//            }

           
//            }
//        }
//    }