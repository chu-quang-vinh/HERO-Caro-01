using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp10;

namespace BoardGameWinForms
{
    // Enums
    public enum CellState { Empty, Player1, Player2, Resource }
    public enum ResourceType { Mana, Rage, Sword }
    public enum SkillType { DestroyArea, Heal, RageBoost }

    // Board Manager
    public class BoardManager
    {
        public const int BOARD_SIZE = 10;
        public CellState[,] board = new CellState[BOARD_SIZE, BOARD_SIZE];

        public bool IsValidMove(int x, int y)
        {
            return x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE && board[x, y] == CellState.Empty;
        }

        public void PlacePiece(int x, int y, CellState player)
        {
            if (IsValidMove(x, y))
            {
                board[x, y] = player;
            }
        }

        public bool CheckWinCondition(int x, int y, CellState playerState)
        {
            // Kiểm tra 5 quân liên tiếp (thay vì 4)
            if (CheckLine(x, y, 1, 0, playerState) >= 5 || // Dọc
                CheckLine(x, y, 0, 1, playerState) >= 5 || // Ngang
                CheckLine(x, y, 1, 1, playerState) >= 5 || // Chéo
                CheckLine(x, y, 1, -1, playerState) >= 5)   // Chéo ngược
            {
                return true;
            }
            return false;
        }
        // Kiểm tra số lượng quân liên tiếp theo hướng (dx, dy)
        private int CheckLine(int x, int y, int dx, int dy, CellState playerState)
        {
            int count = 0;
            int nx = x;
            int ny = y;

            // Kiểm tra về phía trước
            while (IsInBounds(nx, ny) && board[nx, ny] == playerState)
            {
                count++;
                nx += dx;
                ny += dy;
            }

            // Kiểm tra về phía sau
            nx = x - dx;
            ny = y - dy;
            while (IsInBounds(nx, ny) && board[nx, ny] == playerState)
            {
                count++;
                nx -= dx;
                ny -= dy;
            }

            return count;
        }



        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE;
        }

        public void ClearArea(int centerX, int centerY)
        {
            for (int x = Math.Max(0, centerX - 1); x < Math.Min(BOARD_SIZE, centerX + 2); x++)
            {
                for (int y = Math.Max(0, centerY - 1); y < Math.Min(BOARD_SIZE, centerY + 2); y++)
                {
                    board[x, y] = CellState.Empty;
                }
            }
        }
        public bool CheckForFourInARow(int x, int y, CellState player)
        {
            int[][] directions = new int[][] {
        new int[] {1, 0}, new int[] {0, 1},
        new int[] {1, 1}, new int[] {1, -1}
    };

            foreach (var dir in directions)
            {
                int count = 1;
                int dx = dir[0];
                int dy = dir[1];

                // Kiểm tra về hướng dương
                for (int i = 1; i <= 3; i++)
                {
                    int newX = x + dx * i;
                    int newY = y + dy * i;
                    if (IsInBounds(newX, newY) && board[newX, newY] == player)
                        count++;
                    else
                        break;
                }

                // Kiểm tra về hướng âm
                for (int i = 1; i <= 3; i++)
                {
                    int newX = x - dx * i;
                    int newY = y - dy * i;
                    if (IsInBounds(newX, newY) && board[newX, newY] == player)
                        count++;
                    else
                        break;
                }

                if (count >= 4)
                    return true;
            }
            return false;
        }

    }

    // Player Class
    public class Player
    {
        public int Mana { get; set; } = 0;
        public int Rage { get; set; } = 0;
        public int Health { get; set; } = 100;
        public int BoostedDamageTurns { get; set; } = 0;

        public bool CanUseSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.DestroyArea:
                    return Mana >= 2 && Rage >= 2;
                case SkillType.Heal:
                    return Mana >= 3;
                case SkillType.RageBoost:
                    return Rage >= 3;
                default:
                    return false;
            }
        }

        public void UseSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.DestroyArea:
                    Mana -= 2;
                    Rage -= 2;
                    break;
                case SkillType.Heal:
                    Mana -= 3;
                    Health = Math.Min(Health + 25, 100);
                    break;
                case SkillType.RageBoost:
                    Rage -= 3;
                    BoostedDamageTurns = 3;
                    break;
            }
        }
    }

    // Main Game Form
    public partial class GameForm : Form
    {
        private BoardManager boardManager;
        private Player player1, player2;
        private int currentPlayerIndex = 0;
        private int turnCount = 0;

        private Button[,] boardButtons;
        private Label lblCurrentTurn, lblPlayer1Info, lblPlayer2Info;
        private Button btnDestroyArea, btnHeal, btnRageBoost;

        public GameForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeComponent()
        {
            this.Text = "Advanced Board Game";
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel boardPanel = new Panel
            {
                Location = new Point(50, 100),
                Size = new Size(500, 500)
            };

            boardButtons = new Button[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    boardButtons[i, j] = new Button
                    {
                        Location = new Point(j * 50, i * 50),
                        Size = new Size(50, 50),
                        Tag = new Point(i, j)
                    };
                    boardButtons[i, j].Click += BoardButton_Click;
                    boardPanel.Controls.Add(boardButtons[i, j]);
                }
            }

            lblCurrentTurn = new Label
            {
                Location = new Point(50, 50),
                Size = new Size(300, 30),
                Font = new Font(Font.FontFamily, 12),
                Text = "Lượt chơi: Người chơi 1"
            };

            lblPlayer1Info = new Label
            {
                Location = new Point(600, 100),
                Size = new Size(300, 100),
                Font = new Font(Font.FontFamily, 10)
            };

            lblPlayer2Info = new Label
            {
                Location = new Point(600, 250),
                Size = new Size(300, 100),
                Font = new Font(Font.FontFamily, 10)
            };

            btnDestroyArea = CreateSkillButton("Phá hủy vùng (2M 2R)", 600, 400);
            btnHeal = CreateSkillButton("Hồi máu (3M)", 600, 450);
            btnRageBoost = CreateSkillButton("Tăng Rage (3R)", 600, 500);

            btnDestroyArea.Click += (s, e) => UseSkill(SkillType.DestroyArea);
            btnHeal.Click += (s, e) => UseSkill(SkillType.Heal);
            btnRageBoost.Click += (s, e) => UseSkill(SkillType.RageBoost);

            this.Controls.Add(boardPanel);
            this.Controls.Add(lblCurrentTurn);
            this.Controls.Add(lblPlayer1Info);
            this.Controls.Add(lblPlayer2Info);
            this.Controls.Add(btnDestroyArea);
            this.Controls.Add(btnHeal);
            this.Controls.Add(btnRageBoost);
        }

        private Button CreateSkillButton(string text, int x, int y)
        {
            return new Button
            {
                Location = new Point(x, y),
                Size = new Size(200, 30),
                Text = text
            };
        }

        private void InitializeGame()
        {
            boardManager = new BoardManager();
            player1 = new Player();
            player2 = new Player();
            UpdatePlayerInfo();
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point coordinates = (Point)clickedButton.Tag;

            // Kiểm tra nếu ô là tài nguyên
            if (boardManager.board[coordinates.X, coordinates.Y] == CellState.Resource)
            {
                CollectResource(coordinates.X, coordinates.Y);
                return;
            }

            // Kiểm tra nước đi hợp lệ
            if (boardManager.IsValidMove(coordinates.X, coordinates.Y))
            {
                // Xác định người chơi hiện tại
                CellState currentPlayerState = currentPlayerIndex == 0 ? CellState.Player1 : CellState.Player2;

                // Đặt quân cờ
                boardManager.PlacePiece(coordinates.X, coordinates.Y, currentPlayerState);

                // Cập nhật giao diện
                UpdateButtonDisplay(coordinates.X, coordinates.Y, currentPlayerState);

                // Kiểm tra điều kiện chiến thắng
                if (boardManager.CheckWinCondition(coordinates.X, coordinates.Y, currentPlayerState))
                {
                    MessageBox.Show($"Người chơi {currentPlayerIndex + 1} chiến thắng!");
                    ResetGame();
                    return;
                }

                // Kiểm tra nếu có 4 quân liên tiếp
                if (boardManager.CheckForFourInARow(coordinates.X, coordinates.Y, currentPlayerState))
                {
                    // Tạo tài nguyên ngẫu nhiên khi có 4 quân liên tiếp
                    GenerateSingleRandomResource(currentPlayerIndex);
                }

                // Chuyển lượt
                currentPlayerIndex = 1 - currentPlayerIndex;
                turnCount++;

                // Sinh tài nguyên ngẫu nhiên sau mỗi 5 lượt
                GenerateRandomResource();

                // Cập nhật nhãn lượt chơi
                lblCurrentTurn.Text = $"Lượt chơi: Người chơi {currentPlayerIndex + 1}";

                // Cập nhật thông tin người chơi
                UpdatePlayerInfo();
            }
            else
            {
                MessageBox.Show("Nước đi không hợp lệ. Vui lòng chọn ô trống.");
            }
        }

        private void CollectResource(int x, int y)
        {
            // Lấy người chơi hiện tại
            var currentPlayer = currentPlayerIndex == 0 ? player1 : player2;
            var opponentPlayer = currentPlayerIndex == 0 ? player2 : player1;

            // Lấy loại tài nguyên từ ô được nhấp
            string resourceText = boardButtons[x, y].Text;

            // Cập nhật tài nguyên dựa trên loại
            if (resourceText == "M")
            {
                currentPlayer.Mana++;
            }
            else if (resourceText == "R")
            {
                currentPlayer.Rage++;
            }
            else if (resourceText == "S")
            {
                // Kiểm tra xem có Rage Boost không
                int damageAmount = currentPlayer.BoostedDamageTurns > 0 ? 20 : 10;

                // Giảm máu đối phương
                opponentPlayer.Health = Math.Max(0, opponentPlayer.Health - damageAmount);

                // Nếu đang có Rage Boost, giảm số lượt Boost
                if (currentPlayer.BoostedDamageTurns > 0)
                {
                    currentPlayer.BoostedDamageTurns--;
                }
            }

            // Xác định trạng thái người chơi hiện tại
            CellState currentPlayerState = currentPlayerIndex == 0 ? CellState.Player1 : CellState.Player2;

            // Đặt quân cờ của người chơi hiện tại vào ô tài nguyên
            boardManager.PlacePiece(x, y, currentPlayerState);

            // Cập nhật giao diện
            UpdateButtonDisplay(x, y, currentPlayerState);

            // Kiểm tra điều kiện chiến thắng
            if (boardManager.CheckWinCondition(x, y, currentPlayerState))
            {
                MessageBox.Show($"Người chơi {currentPlayerIndex + 1} chiến thắng!");
                ResetGame();
                return;
            }

            // Kiểm tra nếu có 4 quân liên tiếp
            if (boardManager.CheckForFourInARow(x, y, currentPlayerState))
            {
                // Tạo tài nguyên ngẫu nhiên khi có 4 quân liên tiếp
                GenerateSingleRandomResource(currentPlayerIndex);
            }

            // Kiểm tra điều kiện kết thúc trò chơi
            if (opponentPlayer.Health <= 0)
            {
                MessageBox.Show($"Người chơi {currentPlayerIndex + 1} chiến thắng!");
                ResetGame();
                return;
            }

            // Chuyển lượt
            currentPlayerIndex = 1 - currentPlayerIndex;
            turnCount++;

            // Sinh tài nguyên ngẫu nhiên sau mỗi 5 lượt
            GenerateRandomResource();

            // Cập nhật nhãn lượt chơi
            lblCurrentTurn.Text = $"Lượt chơi: Người chơi {currentPlayerIndex + 1}";

            // Cập nhật thông tin người chơi
            UpdatePlayerInfo();

            // Thông báo người chơi đã thu thập tài nguyên
            if (resourceText == "S")
            {
                string boostMessage = currentPlayer.BoostedDamageTurns > 0
                    ? " (Sát thương tăng gấp đôi do Rage Boost!)"
                    : "";
                MessageBox.Show($"Người chơi {currentPlayerIndex + 1} đã thu thập Sword, gây {(currentPlayer.BoostedDamageTurns > 0 ? 20 : 10)} sát thương cho đối phương và đặt quân{boostMessage}!");
            }
            else
            {
                MessageBox.Show($"Người chơi {currentPlayerIndex + 1} đã thu thập tài nguyên: {resourceText} và đặt quân");
            }
        }

        private void GenerateSingleRandomResource(int playerIndex)
        {
            Random rand = new Random();
            ResourceType resourceType = (ResourceType)rand.Next(3);

            // Lấy người chơi hiện tại
            Player currentPlayer = playerIndex == 0 ? player1 : player2;

            // Thêm tài nguyên vào kho của người chơi
            switch (resourceType)
            {
                case ResourceType.Mana:
                    currentPlayer.Mana++;
                    MessageBox.Show($"Người chơi {playerIndex + 1} nhận được 1 Mana!");
                    break;
                case ResourceType.Rage:
                    currentPlayer.Rage++;
                    MessageBox.Show($"Người chơi {playerIndex + 1} nhận được 1 Rage!");
                    break;
                case ResourceType.Sword:
                    currentPlayer.Health = Math.Min(currentPlayer.Health + 10, 100);
                    MessageBox.Show($"Người chơi {playerIndex + 1} nhận được 10 HP!");
                    break;
            }

            // Cập nhật thông tin người chơi
            UpdatePlayerInfo();
        }




        private void UseSkill(SkillType skillType)
        {
            Player currentPlayer = currentPlayerIndex == 0 ? player1 : player2;

            if (currentPlayer.CanUseSkill(skillType))
            {
                switch (skillType)
                {
                    case SkillType.DestroyArea:
                        boardManager.ClearArea(5, 5);
                        break;
                    case SkillType.Heal:
                        break;
                    case SkillType.RageBoost:
                        break;
                }

                currentPlayer.UseSkill(skillType);
                UpdatePlayerInfo();
                UpdateBoardDisplay();
            }
            else
            {
                MessageBox.Show("Không đủ tài nguyên để sử dụng kỹ năng!");
            }
        }

        private void GenerateRandomResource()
        {
            // Kiểm tra mỗi 5 lượt chơi, bắt đầu từ lượt thứ 5
            if ((turnCount > 0) && (turnCount % 5 == 0))
            {
                Random rand = new Random();
                int resourceCount = rand.Next(1, 4);  // Tạo số tài nguyên ngẫu nhiên (1-3)

                for (int i = 0; i < resourceCount; i++)
                {
                    int x, y;
                    int attempts = 0;
                    do
                    {
                        x = rand.Next(10);
                        y = rand.Next(10);
                        attempts++;

                        // Ngăn chặn vòng lặp vô hạn nếu bàn cờ gần như đầy
                        if (attempts > 100)
                        {
                            return;  // Thoát nếu không tìm được ô trống
                        }
                    } while (boardManager.board[x, y] != CellState.Empty);  // Kiểm tra ô trống

                    boardManager.board[x, y] = CellState.Resource;
                    ResourceType resourceType = (ResourceType)rand.Next(3);

                    // Chọn loại tài nguyên ngẫu nhiên
                    switch (resourceType)
                    {
                        case ResourceType.Mana:
                            boardButtons[x, y].BackColor = Color.Blue;
                            boardButtons[x, y].Text = "M";
                            break;
                        case ResourceType.Rage:
                            boardButtons[x, y].BackColor = Color.Red;
                            boardButtons[x, y].Text = "R";
                            break;
                        case ResourceType.Sword:
                            boardButtons[x, y].BackColor = Color.Yellow;
                            boardButtons[x, y].Text = "S";
                            break;
                    }
                }
            }
        }

        private bool CheckResourceRow(int x, int y, ResourceType resourceType)
        {
            int[][] directions = new int[][] {
        new int[] {1, 0}, new int[] {0, 1},
        new int[] {1, 1}, new int[] {1, -1}
    };

            foreach (var dir in directions)
            {
                int count = 1;
                int dx = dir[0];
                int dy = dir[1];

                for (int i = 1; i <= 3; i++)
                {
                    int newX = x + dx * i;
                    int newY = y + dy * i;
                    if (boardManager.IsInBounds(newX, newY) && boardManager.board[newX, newY] == CellState.Resource &&
                        boardButtons[newX, newY].Text == resourceType.ToString()[0].ToString())
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = 1; i <= 3; i++)
                {
                    int newX = x - dx * i;
                    int newY = y - dy * i;
                    if (boardManager.IsInBounds(newX, newY) && boardManager.board[newX, newY] == CellState.Resource &&
                        boardButtons[newX, newY].Text == resourceType.ToString()[0].ToString())
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (count >= 4)
                    return true;
            }
            return false;
        }


        private void ResetGame()
        {
            boardManager = new BoardManager();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    boardButtons[i, j].Text = "";
                    boardButtons[i, j].BackColor = DefaultBackColor;
                }
            }

            currentPlayerIndex = 0;
            turnCount = 0;

            lblCurrentTurn.Text = "Lượt chơi: Người chơi 1";
        }

        private void UpdateButtonDisplay(int x, int y, CellState state)
        {
            boardButtons[x, y].Text = state == CellState.Player1 ? "X" : "O";
            boardButtons[x, y].BackColor = state == CellState.Player1 ? Color.Green : Color.Orange;
        }

        private void UpdatePlayerInfo()
        {
            lblPlayer1Info.Text = $"Người chơi 1:\nMana: {player1.Mana}\nRage: {player1.Rage}\nHP: {player1.Health}";
            lblPlayer2Info.Text = $"Người chơi 2:\nMana: {player2.Mana}\nRage: {player2.Rage}\nHP: {player2.Health}";
        }

        private void UpdateBoardDisplay()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    CellState state = boardManager.board[i, j];
                    UpdateButtonDisplay(i, j, state);
                }
            }
        }

    }


    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Mở form đăng nhập
            Application.Run(new LoginForm());
        }
    }


}

