using System;

namespace s1131375程式設計期末報告
{
    public partial class Form1 : Form
    {
        int screenWidth, screenHeight;

        ButtonFunctions f;
        Button[,] chessBoard; // 在這裡增加了 Tag，例如 chessBoard[0, 0] 的 Tag : 00，chessBoard[1, 2] 的 Tag : 12，以此類推

        public Form1()
        {
            InitializeComponent();

            f = new ButtonFunctions();

            // 排版初始化
            string[] difficulty = new string[] { "simple", "medium", "hard" }; // 連結難度的地方
            DifficultySwitch(difficulty[0]);
        }

        // 選擇難度
        void DifficultySwitch(string diff)
        {
            switch (diff)
            {
                case "simple": SimpleTypography(); break;
                case "medium": MediumTypography(); break;
                case "hard": HardTypography(); break;
            }
        }


        // === 組裝形成排版 ===
        // wid x hei 顆按鈕，chessLen 為每顆按鈕長度

        void SimpleTypography() // 簡單難度排版
        {
            int[] grids = new int[] { 8, 8 };
            int chessLen = 30,
                gridHei = grids[0],
                gridWid = grids[1];
            chessBoard = new Button[gridHei, gridWid];

            this.Size = new Size(320, 410);
            this.screenWidth = Size.Width;
            this.screenHeight = Size.Height;
            

            int posX, posY, sideLen, imgSize, btnMargins = 3;
            float labelFontSize = 18;
            string imgPath, text;

            ClickerBoard(
                gridWid,
                gridHei,
                posX = (screenWidth - (gridWid * chessLen)) / 2 - btnMargins,
                posY = screenHeight / 5,
                chessLen); // 踩地雷的地方

            SmileResetButton(
                sideLen = (int)(1.5*chessLen),
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                imgPath = "smile.png",
                imgSize = 50); // 笑臉重置按鈕

            GenerateLabel(
                posX = chessBoard[0, 0].Location.X,
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                labelFontSize,
                text = "1234"); // 左邊標籤 顯示旗子數量

            GenerateLabel(
                posX = chessBoard[0, chessBoard.GetLength(0)-1].Location.X - sideLen,
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                labelFontSize,
                text = "5678"); // 右邊標籤顯示時間經過了多少
        }
        void MediumTypography()
        {
            int[] grids = new int[] { 16, 16 };
            int chessLen = 30,
                gridHei = grids[0],
                gridWid = grids[1];
            chessBoard = new Button[gridHei, gridWid];

            this.Size = new Size(320, 410);
            this.screenWidth = Size.Width;
            this.screenHeight = Size.Height;


            int posX, posY, sideLen, imgSize, btnMargins = 3;
            float labelFontSize = 18;
            string imgPath, text;

            ClickerBoard(
                gridWid,
                gridHei,
                posX = (screenWidth - (gridWid * chessLen)) / 2 - btnMargins,
                posY = screenHeight / 5,
                chessLen); // 踩地雷的地方

            SmileResetButton(
                sideLen = (int)(1.5 * chessLen),
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                imgPath = "smile.png",
                imgSize = 50); // 笑臉重置按鈕

            GenerateLabel(
                posX = chessBoard[0, 0].Location.X,
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                labelFontSize,
                text = "1234"); // 左邊標籤 顯示旗子數量

            GenerateLabel(
                posX = chessBoard[0, chessBoard.GetLength(0) - 1].Location.X - sideLen,
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                labelFontSize,
                text = "5678"); // 右邊標籤顯示時間經過了多少
        } // 中等難度排版
        void HardTypography()
        {
            int[] grids = new int[] { 16, 16 };
            int chessLen = 30,
                gridHei = grids[0],
                gridWid = grids[1];
            chessBoard = new Button[gridHei, gridWid];

            this.Size = new Size(320, 410);
            this.screenWidth = Size.Width;
            this.screenHeight = Size.Height;


            int posX, posY, sideLen, imgSize, btnMargins = 3;
            float labelFontSize = 18;
            string imgPath, text;

            ClickerBoard(
                gridWid,
                gridHei,
                posX = (screenWidth - (gridWid * chessLen)) / 2 - btnMargins,
                posY = screenHeight / 5,
                chessLen); // 踩地雷的地方

            SmileResetButton(
                sideLen = (int)(1.5 * chessLen),
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                imgPath = "smile.png",
                imgSize = 50); // 笑臉重置按鈕

            GenerateLabel(
                posX = chessBoard[0, 0].Location.X,
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                labelFontSize,
                text = "1234"); // 左邊標籤 顯示旗子數量

            GenerateLabel(
                posX = chessBoard[0, chessBoard.GetLength(0) - 1].Location.X - sideLen,
                posY = (chessBoard[0, 0].Location.Y - sideLen) / 2,
                labelFontSize,
                text = "5678"); // 右邊標籤顯示時間經過了多少
        } // 困難難度排版


        // === 動態生成物件 ===
        object GenerateSquareButton(int sideLen, int x, int y, EventHandler func, string name = "btn", string text="", string tag = "")
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
        void GenerateLabel(int posX, int posY, float fontSize, string text, string name="label")
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


        // === 組裝物件 ===
        void SmileResetButton(int sideLen, int posY, string imgPath = "smile.png", int imgSize=50)
        {

            int x = screenWidth / 2 - sideLen / 2 - 3, // 有 3px Margin
                y = posY;

            Button btn = (Button)GenerateSquareButton(sideLen, x, y, f.Empty);

            btn.Image = Image.FromFile(imgPath);
            btn.Image = new Bitmap(btn.Image, imgSize, imgSize);
        }
        void ClickerBoard(int width, int height, int posX, int posY, int Len)
        {
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    int x = posX + w * Len,
                        y = posY + h * Len;

                    chessBoard[h, w] = (Button)GenerateSquareButton(Len, x, y, f.Empty, tag: $"{h}{w}");
                }
            }
        } // 生成出 width x height 個按鈕，position 在左上角
    }
}