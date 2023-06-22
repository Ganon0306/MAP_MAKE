using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _230613_2
{
    internal class Program
    {
        static char[,] MiniMap = new char[20 + 2, 20 + 2];

        static int MapX = 11;        //플레이어 위치
        static int MapY = 11;

        static int mmX = 3; //현재 미니맵 좌표
        static int mmY = 3;

        static Random rnd = new Random();

        static int moveCount = 0;
        static int score = 0;
        static int bestScore = 0;

        static bool game = true;

        static List<(int, int)> MapTile = new List<(int, int)>();
        static void Main(string[] args)
        {
            while (true)
            {

                while (game == true)
                {
                    RndMapTiles();
                    enemy();
                    MakeMap();

                    Console.WriteLine();
                    Console.WriteLine("현재 스코어 : {0}         최고 스코어 : {1}", score, bestScore);
                    #region 플레이어 움직임
                    ConsoleKeyInfo KeyInfo = Console.ReadKey();
                    char move = KeyInfo.KeyChar;

                    if (move == 'a')
                    {
                        if (MapY > 1 && MiniMap[MapX, MapY - 1] != '■')
                        {
                            MapY -= 1;
                            moveCount++;
                            enemyMove();
                            score++;
                        }
                    }
                    else if (move == 'd')
                    {
                        if (MapY < 20 && MiniMap[MapX, MapY + 1] != '■')
                        {
                            MapY += 1;
                            moveCount++;
                            enemyMove();
                            score++;
                        }
                    }
                    else if (move == 'w')
                    {
                        if (MapX > 1 && MiniMap[MapX - 1, MapY] != '■')
                        {
                            MapX -= 1;
                            moveCount++;
                            enemyMove();
                            score++;
                        }
                    }
                    else if (move == 's')
                    {
                        if (MapX < 20 && MiniMap[MapX + 1, MapY] != '■')
                        {
                            MapX += 1;
                            moveCount++;
                            
                            enemyMove();
                            score++;
                        }
                    }
                    Console.Clear();
                    if (MiniMap[MapX, MapY] == '◈')
                    {
                        game_();
                    }
                    #endregion
                }
                game = true;
            }
        }

        static void RndMapTiles()//렌덤 맵타일 생성
        {
            while (MapTile.Count < 50)
            {
                int x = rnd.Next(1, 21);
                int y = rnd.Next(1, 21);

                if (!MapTile.Contains((x, y)) && (x != 20 || y != 20))
                {
                    MapTile.Add((x, y));
                }
            }
        }

        static void MakeMap()
        {
            for (int a = 0; a < 1; a++) //미니맵
            {

                for (int i = 0; i < 20 + 2; i++)
                {
                    for (int j = 0; j < 20 + 2; j++)
                    {
                        if (i == 0 || i == 21 || j == 0 || j == 21)
                        {
                            MiniMap[i, j] = '■';
                        }
                        else
                        {
                            if (MiniMap[i, j] != '◈')
                            {
                                MiniMap[i, j] = '□';
                            }
                        }
                    }
                }

                foreach ((int posX, int posY) in MapTile)
                {
                    MiniMap[posX, posY] = '■';
                    MiniMap[MapX, MapY] = '★';
                }
            }
            for (int i = 0; i < 20 + 2; i++)
            {
                for (int j = 0; j < 20 + 2; j++)
                {
                    Console.Write("{0} ", MiniMap[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void enemy()
        {
            if (moveCount % 25 == 0)
            {
                int x = rnd.Next(1, 21);
                int y = rnd.Next(1, 21);

                int RangeX = Math.Abs(x - MapX);
                int RangeY = Math.Abs(y - MapY);
                while (Math.Abs((RangeX) - (RangeY)) > 5 || MiniMap[x, y] == '■')
                {
                    x = rnd.Next(1, 21);
                    y = rnd.Next(1, 21);
                    RangeX = Math.Abs(x - MapX);
                    RangeY = Math.Abs(y - MapY);
                }


                MiniMap[x, y] = '◈';
            }
        }
        static void enemyMove() 
        {
            for (int i = 0; i < 20 + 2; i++)
            {
                for (int j = 0; j < 20 + 2; j++)
                {
                    if (MiniMap[i, j] == '◈')
                    {
                        if (Math.Abs(i - MapX) > Math.Abs(j - MapY))
                        {
                            if (i > MapX)
                            {
                                if (MiniMap[i - 1, j] != '■' && MiniMap[i - 1, j] != '◈')
                                {
                                    MiniMap[i, j] = '□';
                                    i--;
                                    MiniMap[i, j] = '◈';
                                }
                                else if(MiniMap[i - 1, j] == '■' || MiniMap[i - 1, j] == '◈')
                                {
                                    if (MiniMap[i, j - 1] != '■' && MiniMap[i, j - 1] != '◈')
                                    {
                                        MiniMap[i, j] = '□';
                                        j--;
                                        MiniMap[i, j] = '◈';
                                    }
                                    else if (MiniMap[i, j + 1] != '■' && MiniMap[i, j + 1] != '◈')
                                    {
                                        MiniMap[i, j] = '□';
                                        j++;
                                        MiniMap[i, j] = '◈';
                                    }
                                }
                            }
                            else if (i < MapX)
                            {
                                if (MiniMap[i + 1, j] != '■' && MiniMap[i + 1, j] != '◈')
                                {
                                    MiniMap[i, j] = '□';
                                    i++;
                                    MiniMap[i, j] = '◈';
                                }
                                else if (MiniMap[i + 1, j] == '■' || MiniMap[i + 1, j] == '◈')
                                {
                                    if (MiniMap[i, j + 1] != '■' && MiniMap[i, j + 1] != '◈')
                                    {
                                        MiniMap[i, j] = '□';
                                        j++;
                                        MiniMap[i, j] = '◈';
                                    }
                                    else if (MiniMap[i, j - 1] != '■' && MiniMap[i, j - 1] != '◈')
                                    {
                                        MiniMap[i, j] = '□';
                                        j++;
                                        MiniMap[i, j] = '◈';
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (j > MapY)
                            {
                                if (MiniMap[i, j - 1] != '■' && MiniMap[i, j - 1] != '◈')
                                {
                                    MiniMap[i, j] = '□';
                                    j--;
                                    MiniMap[i, j] = '◈';
                                }
                                else if (MiniMap[i, j - 1] == '■' || MiniMap[i, j - 1] == '◈')
                                {
                                    if (MiniMap[i - 1, j] != '■' && MiniMap[i - 1, j] != '◈')
                                    {
                                        MiniMap[i, j] = '□';
                                        i--;
                                        MiniMap[i, j] = '◈';
                                    }
                                    else if (MiniMap[i + 1, j] != '■' && MiniMap[i + 1, j] != '◈')
                                    {
                                        MiniMap[i, j] = '□';
                                        i++;
                                        MiniMap[i, j] = '◈';
                                    }
                                }
                            }
                            else if (j < MapY)
                            {
                                if (MiniMap[i, j + 1] != '■' && MiniMap[i, j + 1] != '◈')
                                {
                                    MiniMap[i, j] = '□';
                                    j++;
                                    MiniMap[i, j] = '◈';
                                }
                                else if (MiniMap[i, j + 1] == '■' || MiniMap[i, j + 1] == '◈')
                                {
                                    if (MiniMap[i + 1, j] != '■' && MiniMap[i + 1, j] != '◈')
                                    {
                                        MiniMap[i, j] = '□';
                                        i++;
                                        MiniMap[i, j] = '◈';
                                    }
                                    else if (MiniMap[i - 1, j] != '■' && MiniMap[i - 1, j] != '◈')
                                    {
                                        MiniMap[i, j] = '□';
                                        i--;
                                        MiniMap[i, j] = '◈';
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        static void game_()
        {            
            for (int i = 0; i < 20 + 2; i++)
            {
                for (int j = 0; j < 20 + 2; j++)
                {
                    if (MiniMap[i, j] == '◈')
                    {
                        MiniMap[i, j] = '□';
                    }
                }
            }
            moveCount = 0;
            if (bestScore < score)
            {
                bestScore = score;
            }
            score = 0;
            game = false;
        }
    }
}

