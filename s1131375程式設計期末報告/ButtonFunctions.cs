using System;
using System.Windows.Forms;

namespace 第十一組程式設計期末報告
{
    internal class ButtonFunctions // 功能可以寫在這裡  
    {
        Form1 form;
        Engine engine;
        private bool[,] isRevealed;
        public ButtonFunctions(Form1 form)
        {
            this.form = form;
            engine = new Engine();
            InitializeComponent(); // 確保 InitializeComponent 方法被呼叫  
        }

        private void InitializeComponent()
        {
           
        }

        public void Empty(object sender, EventArgs e)
        {

        }
        public void HandleButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // 若已標旗則不繼續揭示
            if (btn.Text == "F") return;
            string[] tagParts = btn.Tag.ToString().Split(',');
            int h = int.Parse(tagParts[0]);
            int w = int.Parse(tagParts[1]);

            if (!form.IsGameStarted)
            {
                form.StartTimer();
                form.IsGameStarted = true;
            }

            // 踩到炸彈
            if (engine.Try(h, w))
            {
                RevealAllMines();
                MessageBox.Show("遊戲結束！踩到地雷了！");
                form.ResetGame();
                form.ResetTimer();
            }
            else
            {
                // 呼叫 Engine 的遞迴展開邏輯
                engine.afterTry(h, w);
                // 根據 engine.map 的內容更新所有按鈕：
                UpdateButtonsFromEngine();

                if (engine.isgameover())
                {
                    MessageBox.Show("恭喜獲勝！");
                    form.ResetGame();
                    form.ResetTimer();
                }
            }
        }

        public void HandleRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button btn = (Button)sender;

                if (!btn.Enabled)
                    return;

                string[] tagParts = btn.Tag.ToString().Split(',');
                int h = int.Parse(tagParts[0]);
                int w = int.Parse(tagParts[1]);

                if (btn.Text == "F")
                {
                    btn.Text = "";
                    form.IncreaseFlagCount(); 
                }
                else if (form.GetRemainingFlags() > 0)
                {
                    btn.Text = "F";
                    form.DecreaseFlagCount(); 
                }
            }
        }
        private void UpdateButtonsFromEngine()
        {
            if (engine == null || engine.map == null || isRevealed == null)
            {
                MessageBox.Show("引擎未正確初始化!");
                return;
            }
            for (int h = 0; h < engine.x; h++)
            {
                for (int w = 0; w < engine.y; w++)
                {
                    if (h < 0 || h >= engine.x || w < 0 || w >= engine.y) continue;
                    if (engine.map[h, w] != '*' && !isRevealed[h, w])
                    {
                        string chessName = $"chess_{h}_{w}";
                        Button chessButton = form.Controls.Find(chessName, true).FirstOrDefault() as Button;

                        if (chessButton != null)
                        {
                            chessButton.Text = engine.map[h, w].ToString();
                            chessButton.Enabled = false;
                            isRevealed[h, w] = true; 
                        }
                    }
                }
            }
        }


        private void RevealAllMines()
        {
            for (int h = 0; h < engine.x; h++)
            {
                for (int w = 0; w < engine.y; w++)
                {
                    if (engine.a[h, w] == -1)
                    {
                        string chessName = $"chess_{h}_{w}";
                        Button chessButton = form.Controls.Find(chessName, true).FirstOrDefault() as Button;
                        if (chessButton != null)
                        {
                            chessButton.Text = "*";
                            chessButton.BackColor = Color.Red;
                        }
                    }
                }
            }
        }
        public void ResetMarking(object sender, EventArgs e)
        {
            form.ResetTimer();
            form.IsGameStarted = false;

            int rows = form.getChessHeight, cols = form.getChessWidth;
            engine.resetmap();
            engine.placebomb(form.getBombsNum);
            InitializeGame(rows, cols);

            form.ResetGame();
            form.UpdateChessBoardTags();

            MessageBox.Show($"{form.getBombsNum}");
        }




        char MarkingMines(int h, int w)
        {
            char result = '\0';
            int bombsAround = engine.a[h, w]; // 回傳 -1 代表那一格是地雷  
            bool haveMines = (bombsAround == -1);

            if (haveMines)
            {
                result = '*';
            }
            else if (bombsAround == 0)
            {
                result = ' ';
            }
            else
            {
                result = (char)('0' + bombsAround);
            }

            return result;
        } // 標記有地雷，或周圍地雷的數量  


        public void InitializeGame(int rows, int cols)
        {
            // 创建或重置引擎
            if (engine == null)
            {
                engine = new Engine();
            }

            // 设置地图大小
            engine.setmapsize(rows, cols);
            engine.resetmap();
            engine.placebomb(form.getBombsNum);

            // 初始化 revealed 数组
            isRevealed = new bool[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    isRevealed[i, j] = false;
                }  
            }
        }
    }
}