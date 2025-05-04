using System;
using System.Windows.Forms;

namespace s1131375程式設計期末報告
{
    internal class ButtonFunctions // 功能可以寫在這裡
    {
        Form1 form;
        Engine engine;
        public ButtonFunctions(Form1 form)
        {
            this.form = form;
            engine = new Engine();
        }
        public void Empty(object sender,  EventArgs e)
        {
            //Button btn = sender as Button;

            //MessageBox.Show((string)btn.Tag);
        } // 單純空

        public void ResetMarking(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            engine.placebomb(form.getBombsNum);
            for (int h=0; h<form.getChessHeight; h++)
            {
                for(int w=0;  w<form.getChessWidth; w++)
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
            int[,] minesMap = engine.a; // 設置 16 x 16 的 Array (目前)

            int bombsAround = minesMap[h, w]; // 回傳 -1 代表那一格是地雷
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

    }
}
