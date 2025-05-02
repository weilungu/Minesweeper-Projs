using System;
using System.Windows.Forms;

namespace s1131375程式設計期末報告
{
    internal class ButtonFunctions // 功能可以寫在這裡
    {
        public void Empty(object sender,  EventArgs e)
        {
            Button btn = sender as Button;

            MessageBox.Show((string)btn.Tag);
        } // 單純空

    }
}
