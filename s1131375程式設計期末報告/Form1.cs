using System;
using System.Windows.Forms;

namespace 第十一組程式設計期末報告 // v5
{
    public partial class Form1 : Form
    {
        int screenWidth, screenHeight;
        public int i = 1;
        string[] difficulty;
        string diff; 

        int bombsNum;
        
        Engine engine;
        ButtonFunctions f;
        Button[,] chessBoard; // 在這裡增加了 Tag，例如 chessBoard[0, 0] 的 Tag : 00，chessBoard[1, 2] 的 Tag : 12，以此類推
        private System.Windows.Forms.Timer gameTimer;
        private DateTime gameStartTime;
        private Label flagLabel;
        private Label timeLabel; // 用於顯示時間的標籤
        private int remainingFlags;
        public bool IsGameStarted { get; set; }
        public int GetRemainingFlags() => remainingFlags;
        public void DecreaseFlagCount() => flagLabel.Text = (--remainingFlags).ToString("D3");
        public void IncreaseFlagCount() => flagLabel.Text = (++remainingFlags).ToString("D3");
        public int getBombsNum { get => bombsNum; }
        public int getChessHeight { get => chessBoard.GetLength(0); }
        public int getChessWidth { get => chessBoard.GetLength(1); }

        public Form1(int a)
        {
            InitializeComponent();
            InitializeTimer(); // 初始化計時器

            engine = new Engine();
            f = new ButtonFunctions(this);

            // 初始化難度
            difficulty = new string[] { "simple", "medium", "hard" };
            i = a - 1;
            diff = difficulty[i];

            if (diff == difficulty[0]) bombsNum = 10;
            else if (diff == difficulty[1]) bombsNum = 30;
            else bombsNum = 99;

            DifficultySwitch(); // 根據難度設置界面
        }

        private void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - gameStartTime;
            timeLabel.Text = $"{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
        }
        public void StartTimer()
        {
            gameStartTime = DateTime.Now;
            gameTimer.Start();
        }
        public void StopTimer()
        {
            gameTimer.Stop();
        }
        public void ResetTimer()
        {
            gameTimer.Stop();
            timeLabel.Text = "00:00";
        }
        void DifficultySwitch()
        {
            switch (this.diff)
            {
                case "simple": SimpleTypography(320, 430); break;
                case "medium": MediumTypography(640, 660); break;
                case "hard": HardTypography(1000, 660); break;
            }
        } // 選擇難度


        // === 組裝形成排版 ===
        // wid x hei 顆按鈕，chessLen 為每顆按鈕長度

        void SetScreenSize(int screenWid, int screenHei)
        {
            this.Size = new Size(screenWid, screenHei); // 視窗大小
            this.screenWidth = Size.Width;
            this.screenHeight = Size.Height;
        } // 設定一開始的視窗大小
        void SetGrid(int gridWid, int gridHei)
        {
            int[] grids = new int[] { gridHei, gridWid };
            chessBoard = new Button[gridHei, gridWid];
        } // 設定地雷盤的格子數
        void BuildGrid(int gridWid, int gridHei, int chessLen,
               string leftText, string rightText, string smileImgPath = "smile.png")
        {
            f.InitializeGame(gridHei, gridWid);
            //棋盤按鈕
            ClickerBoard(gridWid, gridHei,
                         posX: (screenWidth - (gridWid * chessLen)) / 2 - 6,
                         posY: 100,
                         Len: chessLen);

            //笑臉重置
            int sideLen = (int)(1.5 * chessLen);
            int smileY = (100 - sideLen) / 2;
            SmileResetButton(sideLen, smileY, smileImgPath, 50);

            //標籤位置
            int labelY = smileY + (sideLen - 30) / 2; // 垂直居中
            int flagX = chessBoard[0, 0].Location.X;
            int timeX = chessBoard[0, gridWid - 1].Location.X + chessLen - 100;

            //創建標籤
            flagLabel = GenerateLabel(flagX, labelY, 18, leftText, "flagLabel");
            timeLabel = GenerateLabel(timeX, labelY, 18, rightText, "timeLabel");

            //初始化旗子數
            remainingFlags = bombsNum;
            flagLabel.Text = remainingFlags.ToString("D3");

            //按鈕事件
            for (int h = 0; h < gridHei; h++)
            {
                for (int w = 0; w < gridWid; w++)
                {
                    if (chessBoard[h, w] != null)
                    {
                        chessBoard[h, w].Click -= f.HandleButtonClick;
                        chessBoard[h, w].MouseDown -= f.HandleRightClick;
                        chessBoard[h, w].Click += f.HandleButtonClick;
                        chessBoard[h, w].MouseDown += f.HandleRightClick;
                    }
                }
            }
            ResetTimer();
        }
        
        Label GenerateLabel(int posX, int posY, float fontSize, string text, string name = "label")
        {
            Label lb = new Label();

            lb.Font = new Font("Microsoft JhengHei UI", fontSize, FontStyle.Regular, GraphicsUnit.Point, 136);
            lb.AutoSize = true;
            lb.Location = new Point(posX, posY);
            lb.Name = name;
            lb.Size = new Size(100, 100);
            lb.TabIndex = 0;
            lb.Text = text;

            this.Controls.Add(lb);
            return lb; // 返回創建的標籤
        }

        // 把所有排版物件組裝起來

        void SimpleTypography(int screenWid, int screenHei, string flagsNum = "010", string times = "00:00")
        {
            int gridWid = 9,
                gridHei = 9,
                chessLen = 30;
            SetScreenSize(screenWid, screenHei);
            SetGrid(gridWid, gridHei);
            BuildGrid(gridWid, gridHei, chessLen, leftText: flagsNum, rightText: times);
        }// 簡單難度排版
        void MediumTypography(int screenWid, int screenHei, string flagsNum = "030", string times = "00:00")
        {
            int gridWid = 16,
                gridHei = 16,
                chessLen = 30;

            SetScreenSize(screenWid, screenHei);
            SetGrid(gridWid, gridHei);
            BuildGrid(gridWid, gridHei, chessLen,
                leftText: flagsNum, rightText: times);
        } // 中等難度排版
        void HardTypography(int screenWid, int screenHei, string flagsNum = "099", string times = "00:00")
        {
            int gridWid = 30,
                gridHei = 16,
                chessLen = 30;

            SetScreenSize(screenWid, screenHei);
            SetGrid(gridWid, gridHei);
            BuildGrid(gridWid, gridHei, chessLen,
                leftText: flagsNum, rightText: times);
        } // 困難難度排版


        // === 組裝物件 ===
        void SmileResetButton(int sideLen, int posY, string imgPath = "smile.png", int imgSize = 50)
        {
            int btnMargin = 3;
            int x = screenWidth / 2 - sideLen / 2 - 2 * btnMargin, // 左右各有3px Margin
                y = posY;
            Button btn = (Button)GenerateSquareButton(sideLen, x, y, f.ResetMarking);
            btn.Image = Image.FromFile(imgPath);
            btn.Image = new Bitmap(btn.Image, imgSize, imgSize);
            btn.Click -= f.ResetMarking; // 確保不重複
            btn.Click += f.ResetMarking;

        }

        void ClickerBoard(int width, int height, int posX, int posY, int Len)
        {
            engine.setmapsize(height, width);
            engine.placebomb(bombsNum);
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    string btnName = $"chess_{h}_{w}";
                    int x = posX + w * Len,
                        y = posY + h * Len;
                    char cellContent = MarkingMines(h, w);
                    string tagValue = $"{h},{w},{cellContent}";
                    chessBoard[h, w] = (Button)GenerateSquareButton(Len, x, y, func: null, name: btnName, tag: tagValue, text: "");

                }
            }
        } // 生成出 width x height 個按鈕，position 在左上角

        public void ResetGame()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn.Name.StartsWith("chess_"))
                {
                    btn.Text = "";
                    btn.Enabled = true;
                    btn.BackColor = SystemColors.Control;
                    if (btn.Name.StartsWith("chess_"))
                    {
                        // Debug：印出 btn.Name 及 btn.Tag
                        Console.WriteLine($"{btn.Name}: {btn.Tag}");
                    }

                }
            }
            remainingFlags = bombsNum;
            flagLabel.Text = remainingFlags.ToString("D3");
            UpdateChessBoardTags();
        }
        public void UpdateChessBoardTags()
        {
            int rows = getChessHeight;
            int cols = getChessWidth;
            for (int h = 0; h < rows; h++)
            {
                for (int w = 0; w < cols; w++)
                {
                    string chessName = $"chess_{h}_{w}";
                    Button chessButton = this.Controls.Find(chessName, true).FirstOrDefault() as Button;
                    if (chessButton != null)
                    {
                        char newMark = MarkingMines(h, w);
                        chessButton.Tag = $"{h},{w},{newMark}";
                    }
                }
            }
        }
        // === 動態生成物件 ===
        object GenerateSquareButton(int sideLen, int x, int y, EventHandler func, string name = "btn", string text = "", string tag = "")
        {
            Button btn = new Button();
            btn.Location = new Point(x, y);
            btn.Name = name;
            btn.Size = new Size(sideLen, sideLen);
            btn.TabIndex = 0;
            btn.Text = text; 
            btn.UseVisualStyleBackColor = true;
            if (!string.IsNullOrEmpty(tag))
                btn.Tag = tag;
            Controls.Add(btn);
            return btn;
        }

        char MarkingMines(int h, int w)
        {
            int bombsAround = engine.a[h, w]; // 回傳 -1 代表那一格是地雷
            if (bombsAround == -1)
            {
                return '*';
            }
            else if (bombsAround == 0)
            {
                return ' ';
            }
            else
            {
                return(char)('0' + bombsAround);
            }

        } // 標記有地雷，或周圍地雷的數量

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
    }
}