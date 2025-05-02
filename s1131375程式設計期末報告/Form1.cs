namespace s1131375程式設計期末報告
{
    public partial class Form1 : Form
    {
        int screenWidth, screenHeight;
        public Button[] boards;

        public Form1()
        {
            InitializeComponent();

            this.Size = new Size(450, 480);
            this.screenWidth = Size.Width;
            this.screenHeight = Size.Height;

            // 排版初始化
            int width = 8,
                height = 8,
                chessLen = 40;
            
            this.boards = new Button[width * height];
            InitializeTypography(width, height, chessLen);
        }

        // === 組裝形成排版 ===
        void InitializeTypography(int width, int height, int chessLen)
        {
            // 450 - 2X = 8 x 30 = 240 => 視窗寬度 - 2X = 寬有多少格子 x 格子邊長
            // X = ( (450 - 240) / 2 ) - 3 = 102    // 格子與格子間格有 3 px 的 Margin

            int posX = (this.screenWidth - width * chessLen) / 2 - 3,
                posY = this.screenHeight / 5;

            ClickerBoard(width, height, posX, posY, chessLen);
            UpButton(25);
        }


        // === 動態生成按鈕物件 ===
        void GenerateSquareButton(int sideLen, int x, int y, EventHandler func, string name = "btn", string text = "")
        {
            Button btn = new Button();

            btn.Location = new Point(x, y);
            btn.Name = name;
            btn.Size = new Size(sideLen, sideLen);
            btn.TabIndex = 0;
            btn.Text = text;
            btn.UseVisualStyleBackColor = true;
            btn.Click += func;

            Controls.Add(btn);
        } // 生出正方形按鈕


        // === 組裝物件 ===
        void UpButton(int posY)
        {
            int sideLen = 50,
                x = screenWidth / 2 - sideLen / 2 - 3, // 有 3px Margin
                y = posY;

            GenerateSquareButton(sideLen, x, y, BtnFuncs.Empty);
        }
        void ClickerBoard(int width, int height, int posX, int posY, int Len)
        {
            int i = 0;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++, i++)
                {
                    int x = posX + w * Len,
                        y = posY + h * Len;
                    GenerateSquareButton(Len, x, y, BtnFuncs.Empty);
                }
            }
        } // 生成出 width x height 個按鈕，position 在左上角
    }
}
