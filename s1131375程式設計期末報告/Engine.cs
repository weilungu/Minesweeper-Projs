using System;

namespace 第十一組程式設計期末報告
{


    internal class Engine // 暫時先把一些遊戲邏輯放在這裡
    {
        public int x = 16, y = 16; //地圖大小
        public int bombnum = 0;
        public char[,] map = new char[16, 16];
        public int[,] a = new int[16, 16];
        static Random random = new Random();
        public Engine()
        {
        }
        public void resetmap()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    map[i, j] = '*';
                    this.a[i, j] = 0;
                }
            }
        }
        public void setmapsize(int a, int b)
        {
            x = a;
            y = b;
            map = new char[a, b];
            this.a = new int[a, b];
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    map[i, j] = '*';
                    this.a[i, j] = 0;
                }
            }
        }
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
        public void printmap() //測試地圖是否正常
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write(this.map[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void printa() //測試地圖是否正常
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write(this.a[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public bool Try(int x, int y) //判斷是否踩到炸彈
        {
            if (a[x, y] == -1)
                return true;
            else
                return false;
        }
        public char[,] afterTry(int x, int y) //踩到非地雷格時的展開邏輯
        {
            if (a[x, y] > 0)
            {
                map[x, y] = (char)(a[x, y] + '0');
                return map;
            }
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int X = x + i, Y = y + j;
                    if (X >= 0 && X < this.x && Y >= 0 && Y < this.y) //邊界
                    {
                        if (map[X, Y] == '*' && a[X, Y] == 0)
                        {
                            map[X, Y] = ' ';
                            afterTry(X, Y);
                        }
                        if (map[X, Y] == '*' && a[X, Y] > 0)
                        {
                            map[X, Y] = (char)(a[X, Y] + '0');
                        }
                    }
                }
            }
            return map;
        }
        public bool isgameover() //判斷遊戲是否結束
        {
            int cont = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (map[i, j] == '*')
                        cont++;
                }
            }
            if (cont == bombnum)
                return true;
            else
                return false;
        }
    }
}

