using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form3 : Form
    {
        public enum CellState { Empty, Player1, Player2 }

        private const int CaroBoardSize = 30; // Khai báo hằng số ở đây
        public class BoardManager
        {
            public const int BOARD_SIZE = 30;
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

            public bool CheckWinCondition(int x, int y, CellState player)
            {
                // Kiểm tra hàng ngang
                int count = 0;
                for (int i = Math.Max(0, y - 4); i <= Math.Min(BOARD_SIZE - 1, y + 4); i++)
                {
                    if (board[x, i] == player)
                    {
                        count++;
                        if (count == 5) return true;
                    }
                    else count = 0;
                }

                // Kiểm tra hàng dọc
                count = 0;
                for (int i = Math.Max(0, x - 4); i <= Math.Min(BOARD_SIZE - 1, x + 4); i++)
                {
                    if (board[i, y] == player)
                    {
                        count++;
                        if (count == 5) return true;
                    }
                    else count = 0;
                }

                // Kiểm tra đường chéo chính
                count = 0;
                for (int i = -4; i <= 4; i++)
                {
                    if (x + i >= 0 && x + i < BOARD_SIZE && y + i >= 0 && y + i < BOARD_SIZE && board[x + i, y + i] == player)
                    {
                        count++;
                        if (count == 5) return true;
                    }
                    else count = 0;
                }

                // Kiểm tra đường chéo phụ
                count = 0;
                for (int i = -4; i <= 4; i++)
                {
                    if (x + i >= 0 && x + i < BOARD_SIZE && y - i >= 0 && y - i < BOARD_SIZE && board[x + i, y - i] == player)
                    {
                        count++;
                        if (count == 5) return true;
                    }
                    else count = 0;
                }

                return false;
            }
        }

        private BoardManager boardManager;
        private int currentPlayerIndex = 0;
        private Button[,] boardButtons;
        private Label lblCurrentTurn;


        public Form3()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeComponent()
        {
            this.Text = "Chế Độ Cổ Điển";
            this.Size = new Size(700, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            lblCurrentTurn = new Label
            {
                Location = new Point(10, 10),
                Size = new Size(300, 30),
                Text = "Lượt chơi: Người chơi 1"
            };
            this.Controls.Add(lblCurrentTurn);
            boardButtons = new Button[CaroBoardSize, CaroBoardSize];
            for (int i = 0; i < CaroBoardSize; i++)
            {
                for (int j = 0; j < CaroBoardSize; j++)
                {
                    boardButtons[i, j] = new Button
                    {
                        Name = "boardButton" + i + "_" + j,
                        Location = new Point(j * 30, i * 30 + 50),
                        Size = new Size(30, 30),
                        Tag = new Point(i, j),
                        BackColor = Color.White,
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };
                    boardButtons[i, j].Click += BoardButton_Click;
                    this.Controls.Add(boardButtons[i, j]);
                }
            }
            

            //boardButtons = new Button[CaroBoardSize, CaroBoardSize]; // Sử dụng CaroBoardSize
            //for (int i = 0; i < CaroBoardSize; i++)
            //{
            //    for (int j = 0; j < CaroBoardSize; j++)
            //    {
            //        // ... (Tạo button)
            //    }
            //}
            //Button btnBack = new Button
            //{
            //    Text = "Quay Lại",
            //    Location = new Point(this.ClientSize.Width - 100, 10),
            //    Size = new Size(80, 30)
            //};
            //btnBack.Click += (s, e) => this.Close();
            //this.Controls.Add(btnBack);
        }
    

            private void InitializeGame()
        {
            boardManager = new BoardManager();
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point coordinates = (Point)clickedButton.Tag;

            if (boardManager.IsValidMove(coordinates.X, coordinates.Y))
            {
                CellState currentPlayerState = currentPlayerIndex == 0 ? CellState.Player1 : CellState.Player2;
                boardManager.PlacePiece(coordinates.X, coordinates.Y, currentPlayerState);
                UpdateButtonDisplay(coordinates.X, coordinates.Y, currentPlayerState);

                if (boardManager.CheckWinCondition(coordinates.X, coordinates.Y, currentPlayerState))
                {
                    MessageBox.Show($"Người chơi {currentPlayerIndex + 1} chiến thắng!");
                    ResetGame();
                    return;
                }

                SwitchTurns();
            }
            else
            {
                MessageBox.Show("Nước đi không hợp lệ. Vui lòng chọn ô trống.");
            }
        }


        private void SwitchTurns()
        {
            currentPlayerIndex = 1 - currentPlayerIndex;
            lblCurrentTurn.Text = $"Lượt chơi: Người chơi {currentPlayerIndex + 1}";
        }


        private void UpdateButtonDisplay(int x, int y, CellState state)
        {
            boardButtons[x, y].Text = state == CellState.Player1 ? "X" : "O";
            boardButtons[x, y].BackColor = state == CellState.Player1 ? Color.Green : Color.Orange;

        }

        private void ResetGame()
        {
            boardManager = new BoardManager();
            foreach (Button btn in boardButtons)
            {
                btn.Text = "";
                btn.BackColor = Color.White;
            }

            currentPlayerIndex = 0;
            lblCurrentTurn.Text = "Lượt chơi: Người chơi 1";
        }

        // ... (Các hàm khác của Form3)
    }
}