using System;
using System.Windows.Forms;

namespace 第十一組程式設計期末報告
{
    internal class ButtonFunctions // 功能可以寫在這裡  
    {
        Form1 form;
        Engine engine;
        public ButtonFunctions(Form1 form)
        {
            this.form = form;
            engine = new Engine();
            InitializeComponent(); // 確保 InitializeComponent 方法被呼叫  
        }

        public void Empty(object sender, EventArgs e)
        {
            //Button btn = sender as Button;  

            //MessageBox.Show((string)btn.Tag);  
        } // 單純空  

        public void ResetMarking(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (form.getBombsNum)
            {
                case 10:
                    engine.setmapsize(9, 9);
                    break;
                case 30:
                    engine.setmapsize(16, 16);
                    break;
                case 99:
                    engine.setmapsize(16, 30);
                    break;
            }
            engine.resetmap();
            engine.placebomb(form.getBombsNum);
            for (int h = 0; h < form.getChessHeight; h++)
            {
                for (int w = 0; w < form.getChessWidth; w++)
                {
                    string chessName = $"chess_{h}_{w}";
                    Button chessButton = (Button)form.Controls.Find(chessName, true).FirstOrDefault(); // 找到每一個地雷盤按鈕的名字  

                    char newMark = MarkingMines(h, w);
                    //if (chessButton != null)  
                    //{  
                    chessButton.Text = $"{newMark}";
                    chessButton.Tag = $"{h},{w},{newMark}";
                    //}  
                }
            }
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

        private void InitializeComponent()
        {
            // 確保此方法存在並初始化必要的元件  
        }
    }
}
