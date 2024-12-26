
using System;
using System.Drawing;
using System.Windows.Forms;

namespace chế_độ_cổ_điển
{
    public partial class GameForm0 : Form
    {
        private int level;
        private Button[,] board;
        private int boardSize = 15;
        private MainForm1 mainForm;
        private string[,] gameState;
        private System.Windows.Forms.Timer timer;
        private int timeLeft;
        private Label timerLabel;
        private bool playerTurn = true;
        private UserManager userManager; // Add UserManager field
        private string currentUsername;  // Add currentUsername field

        public GameForm0(int level, MainForm1 mainForm, UserManager userManager, string username)

        {
            this.level = userManager.LoadGameProgress(username);  // Load tiến độ
            this.mainForm = mainForm;
            this.userManager = userManager;
            this.currentUsername = username;
            InitializeComponentCustom();
            InitializeBoard();
            InitializeTimer();
            SetBackgroundImage();
        }

        private void SetBackgroundImage()
        {
            // Đặt hình nền cho Form từ file có sẵn trong Resources
            this.BackgroundImage = Image.FromFile(@"C:\Users\Admin\Downloads\tải xuống.jpg");

            // Tự động điều chỉnh kích thước hình nền
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void InitializeComponentCustom()
        {
            this.Text = $"Caro Game - Level {level}";
            this.Size = new Size(660, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            timerLabel = new Label
            {
                Text = "Time: 15",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10 + boardSize * 30),
                Size = new Size(100, 20)
            };
            this.Controls.Add(timerLabel);
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 giây
            timer.Tick += new EventHandler(timer_Tick);
            timeLeft = 15;
            timerLabel.Text = $"Time: {timeLeft}";
        }

        private void InitializeBoard()
        {
            board = new Button[boardSize, boardSize];
            gameState = new string[boardSize, boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    board[i, j] = new Button
                    {
                        Width = 30,
                        Height = 30,
                        Location = new Point(30 * j, 30 * i)
                    };
                    board[i, j].Click += BoardButton_Click;

                    gameState[i, j] = "";
                }
            }

            gameState[5, 2] = "X";
            gameState[5, 3] = "O";
            gameState[4, 3] = "X";
            gameState[3, 2] = "X";
            gameState[4, 2] = "O";
            gameState[6, 3] = "X";
            gameState[3, 4] = "O";
            gameState[4, 4] = "O";

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (gameState[i, j] == "X" || gameState[i, j] == "O")
                    {
                        board[i, j].Text = gameState[i, j];
                        board[i, j].Enabled = false;
                    }
                    this.Controls.Add(board[i, j]);
                }
            }
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            if (!playerTurn) return; // Đảm bảo chỉ thực hiện khi đến lượt người chơi

            StartTimer(); // Khởi động lại bộ đếm thời gian
            Button clickedButton = sender as Button;
            int x = clickedButton.Location.Y / 30;
            int y = clickedButton.Location.X / 30;

            if (gameState[x, y] != "") return; // Không cho phép chọn ô đã đánh

            clickedButton.Text = "X";
            gameState[x, y] = "X";
            playerTurn = false; // Chuyển lượt cho bot

            // Khi thắng hoặc thua -> Đóng form hoàn toàn
            if (CheckWin(x, y, "X"))
            {
                MessageBox.Show("You Win!", "Victory", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật level và đóng form
                mainForm.UpdateLevel(level + 1);
                this.Dispose();  // Giải phóng tài nguyên hoàn toàn
                return;
            }

            // Đóng form khi thua
            if (CheckWin(x, y, "O"))
            {
                DialogResult result = MessageBox.Show("You Lose!", "Defeat", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    this.Dispose();  // Đóng form khi người dùng nhấn OK
                }
            }




            // Để bot đánh ngay lập tức
            timer.Stop();
            BotMove();
            playerTurn = true; // Trả lại quyền chơi cho người
            StartTimer(); // Bắt đầu lại bộ đếm
        }

        private void BotMove()
        {
            // 1. Đánh để thắng
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (gameState[i, j] == "")
                    {
                        gameState[i, j] = "O";
                        if (CheckWin(i, j, "O"))
                        {
                            board[i, j].Text = "O";
                            MessageBox.Show("You Lose!", "Defeat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            return;
                        }
                        gameState[i, j] = ""; // Hoàn trả trạng thái
                    }
                }
            }

            // 2. Chặn người chơi khi họ có 4 quân liên tiếp
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (gameState[i, j] == "")
                    {
                        if (CanBlockPlayer(i, j, "X", 4)) // Kiểm tra chặn 4 quân liên tiếp
                        {
                            gameState[i, j] = "O"; // Bot chặn bằng cách đặt "O"
                            board[i, j].Text = "O";
                            return;
                        }
                    }
                }
            }

            // 3. Chặn người chơi khi họ có 3 quân liên tiếp
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (gameState[i, j] == "")
                    {
                        if (CanBlockPlayer(i, j, "X", 3)) // Kiểm tra chặn 3 quân liên tiếp
                        {
                            gameState[i, j] = "O"; // Bot chặn bằng cách đặt "O"
                            board[i, j].Text = "O";
                            return;
                        }
                    }
                }
            }

            // 4. Tạo cơ hội cho bot (ưu tiên đánh gần các ô đã có "O")
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (gameState[i, j] == "" && HasAdjacentO(i, j))
                    {
                        gameState[i, j] = "O";
                        board[i, j].Text = "O";
                        return;
                    }
                }
            }

            // 5. Đánh ngẫu nhiên nếu không có cơ hội đặc biệt
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (gameState[i, j] == "")
                    {
                        gameState[i, j] = "O";
                        board[i, j].Text = "O";
                        return;
                    }
                }
            }
        }

        // Hàm kiểm tra bot có thể chặn người chơi không (dựa trên số lượng quân liên tiếp)
        private bool CanBlockPlayer(int x, int y, string player, int requiredCount)
        {
            int[] dx = { 1, 0, 1, 1 };
            int[] dy = { 0, 1, 1, -1 };

            for (int dir = 0; dir < 4; dir++)
            {
                int count = 0;
                bool hasSpaceBefore = false;
                bool hasSpaceAfter = false;

                // Kiểm tra theo hướng chính
                for (int step = 1; step < 5; step++)
                {
                    int nx = x + step * dx[dir];
                    int ny = y + step * dy[dir];
                    if (nx >= 0 && nx < boardSize && ny >= 0 && ny < boardSize)
                    {
                        if (gameState[nx, ny] == player)
                            count++;
                        else if (gameState[nx, ny] == "")
                        {
                            hasSpaceAfter = true;
                            break;
                        }
                        else break;
                    }
                }

                // Kiểm tra theo hướng ngược lại
                for (int step = 1; step < 5; step++)
                {
                    int nx = x - step * dx[dir];
                    int ny = y - step * dy[dir];
                    if (nx >= 0 && nx < boardSize && ny >= 0 && ny < boardSize)
                    {
                        if (gameState[nx, ny] == player)
                            count++;
                        else if (gameState[nx, ny] == "")
                        {
                            hasSpaceBefore = true;
                            break;
                        }
                        else break;
                    }
                }

                // Nếu đạt số quân liên tiếp yêu cầu và có ít nhất 1 đầu còn trống
                if (count == requiredCount && (hasSpaceBefore || hasSpaceAfter))
                {
                    return true;
                }
            }

            return false;
        }

        // Hàm kiểm tra ô trống có nằm gần quân "O" hay không
        private bool HasAdjacentO(int x, int y)
        {
            int[] dx = { -1, 0, 1, 0, -1, -1, 1, 1 };
            int[] dy = { 0, 1, 0, -1, -1, 1, -1, 1 };

            for (int i = 0; i < 8; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if (nx >= 0 && nx < boardSize && ny >= 0 && ny < boardSize && gameState[nx, ny] == "O")
                {
                    return true;
                }
            }
            return false;
        }



        private void StartTimer()
        {
            timeLeft = 15;
            timerLabel.Text = $"Time: {timeLeft}";
            timer.Start();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (playerTurn)
            {
                if (timeLeft > 0)
                {
                    timeLeft--;
                    timerLabel.Text = $"Time: {timeLeft}";
                }
                else
                {
                    timer.Stop(); // Hết thời gian của người chơi
                    playerTurn = false;
                    BotMove(); // Bot đánh ngay lập tức
                    playerTurn = true; // Trả lại quyền đánh cho người chơi
                    StartTimer(); // Bắt đầu lại bộ đếm cho lượt của người chơi
                }
            }
        }


        private bool CheckWin(int x, int y, string player)
        {
            return CheckDirection(x, y, player, 1, 0) ||
                   CheckDirection(x, y, player, 0, 1) ||
                   CheckDirection(x, y, player, 1, 1) ||
                   CheckDirection(x, y, player, 1, -1);
        }

        private bool CheckDirection(int x, int y, string player, int dx, int dy)
        {
            int count = 1;

            for (int step = 1; step < 5; step++)
            {
                int nx = x + step * dx;
                int ny = y + step * dy;
                if (nx >= 0 && nx < boardSize && ny >= 0 && ny < boardSize && gameState[nx, ny] == player)
                    count++;
                else
                    break;
            }

            for (int step = 1; step < 5; step++)
            {
                int nx = x - step * dx;
                int ny = y - step * dy;
                if (nx >= 0 && nx < boardSize && ny >= 0 && ny < boardSize && gameState[nx, ny] == player)
                    count++;
                else
                    break;
            }

            return count >= 5;
        }
    }
}