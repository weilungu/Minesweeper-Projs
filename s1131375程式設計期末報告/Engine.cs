using System;

namespace s1131375程式設計期末報告
{


    internal class Engine
    {
        public int x = 16, y = 16; //地圖大小
        public int bombnum = 0;
        public char[,] map = new char[16, 16];
        public int[,] a = new int[16, 16];
        static Random random = new Random();
        public Engine() { }
        public void placebomb(int bombnum)
        {
            this.bombnum = bombnum;
            for (int i = 0; i < bombnum; i++)
            {
                int q, w;
                do
                {
                    q = random.Next(0, x);
                    w = random.Next(0, y);
                } while (a[q, w] == -1); // 確保該位置沒有炸彈

                a[q, w] = -1;

                for (int j = -1; j <= 1; j++)
                {
                    for (int r = -1; r <= 1; r++)
                    {
                        if (q + j >= 0 && q + j < x && w + r >= 0 && w + r < y)
                        {
                            if (a[q + j, w + r] != -1)
                            {
                                a[q + j, w + r]++;
                            }
                        }
                    }
                }
            }
        }

    }
}
