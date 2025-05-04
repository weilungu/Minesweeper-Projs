using System;

namespace s1131375程式設計期末報告 // v4
{
    public partial class Form1 : Form
    {
        int screenWidth, screenHeight;
        string[] difficulty;
        string diff;

        int bombsNum;

        Engine engine;
        ButtonFunctions f;
        Button[,] chessBoard; // 在這裡增加了 Tag，例如 chessBoard[0, 0] 的 Tag : 00，chessBoard[1, 2] 的 Tag : 12，以此類推

        public int getBombsNum { get => bombsNum; }
        public int getChessHeight {  get => chessBoard.GetLength(0);}
        public int getChessWidth {  get => chessBoard.GetLength(1);}

        public Form1()
        {
            InitializeComponent();

            engine = new Engine();
            f = new ButtonFunctions(this);

            // 排版初始化
            difficulty = new string[] { "simple", "medium", "hard" };
            diff = difficulty[1];

            if (diff == difficulty[0])
            {
                bombsNum = 10;
            }
            else if (diff == difficulty[1])
            {
                bombsNum = 30;
            }
            else
            {
                bombsNum = 99;
            }

            DifficultySwitch(); // 連結難度的地方
        }

        void DifficultySwitch()
        {
            switch (this.diff)
            {
                case "simple": SimpleTypography(320, 410); break;
                case "medium": MediumTypography(640, 660); break;
                case "hard"  : HardTypography(1000, 660) ; break;
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
            int posX, posY, sideLen, imgSize, btnMargins = 3;
            float labelFontSize = 18;
            string imgPath, text;

            ClickerBoard(
                gridWid,
                gridHei,
                posX = (screenWidth - (gridWid * chessLen)) / 2 - 2*btnMargins,// 有 3px Margin，左右都有 Margin，所以要 *2
                posY = 100, // 把 y 座標統一設成 100
                chessLen); // 踩地雷的地方

            SmileResetButton(
                sideLen = (int)(1.5 * chessLen),
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                imgPath = smileImgPath,
                imgSize = 50); // 笑臉重置按鈕

            GenerateLabel(
                posX = chessBoard[0, 0].Location.X,
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                labelFontSize,
                text = leftText); // 左邊標籤 顯示旗子數量

            GenerateLabel(
                //posX = chessBoard[0, chessBoard.GetLength(0) - 1].Location.X - sideLen,
                posX = chessBoard[0, chessBoard.GetLength(1) - 1].Location.X - sideLen,
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                labelFontSize,
                text = rightText); // 右邊標籤顯示時間經過了多少
        }
                       // 把所有排版物件組裝起來

        void SimpleTypography(int screenWid=320, int screenHei=410, string flagsNum = "010", string times = "00:00")
        {
            int gridWid = 8,
                gridHei = 8,
                chessLen = 30;

            SetScreenSize(screenWid, screenHei);
            SetGrid(gridWid, gridHei);
            BuildGrid(gridWid, gridHei, chessLen, leftText: flagsNum, rightText: times);
        }// 簡單難度排版
        void MediumTypography(int screenWid=640, int screenHei=660, string flagsNum = "030", string times = "00:00")
        {
            int gridWid = 16,
                gridHei = 16,
                chessLen = 30;

            SetScreenSize(screenWid, screenHei);
            SetGrid(gridWid, gridHei);
            BuildGrid(gridWid, gridHei, chessLen,
                leftText: flagsNum, rightText: times);
        } // 中等難度排版
        void HardTypography(int screenWid=960, int screenHei=660, string flagsNum = "099", string times = "00:00")
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
        void SmileResetButton(int sideLen, int posY, string imgPath = "smile.png", int imgSize=50)
        {

            int btnMargin = 3;
            int x = screenWidth / 2 - sideLen / 2 - 2*btnMargin, // 有 3px Margin，左右都有 Margin，所以要 *2
                y = posY;

            Button btn = (Button)GenerateSquareButton(sideLen, x, y, f.ResetMarking);

            btn.Image = Image.FromFile(imgPath);
            btn.Image = new Bitmap(btn.Image, imgSize, imgSize);
        }
        void ClickerBoard(int width, int height, int posX, int posY, int Len)
        {
            engine.placebomb(bombsNum);
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    string btnName = $"chess_{h}_{w}";
                    int x = posX + w * Len,
                        y = posY + h * Len;

                    char haveBomb = MarkingMines(h, w);
                    chessBoard[h, w] = (Button)GenerateSquareButton(Len, x, y, f.Empty, name: btnName, tag: $"{h},{w},{haveBomb}", text: $"{haveBomb}");
                }
            }
        } // 生成出 width x height 個按鈕，position 在左上角


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
            btn.Click += func;
            btn.Tag = tag;

            Controls.Add(btn);
            return btn;
        } // 生出正方形按鈕
        void GenerateLabel(int posX, int posY, float fontSize, string text, string name = "label")
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
        }
        char MarkingMines(int h, int w)
        {
            char result = '\0';
            int[,] minesMap = engine.a; // 設置 16 x 16 的 Array (目前)

            int bombsAround = minesMap[h, w]; // 回傳 -1 代表那一格是地雷
            bool haveMines = (bombsAround == -1);

            if (haveMines)
            {
                result = '*';
            }
            else if(bombsAround == 0)
            {
                result = ' ';
            }
            else
            {
                result = (char)('0' + bombsAround);
            }

            return result;
        } // 標記有地雷，或周圍地雷的數量

        // === 非物件功能 ===
        void haha()
        {

        } // 可能會用到
    }
}