using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pacman
{
    internal class Program
    {
       private static void HandleInput(ConsoleKeyInfo pressedKey, ref int userX, ref int userY, char [,] map, ref int score)
       {
           int[] direction = GetDirection(pressedKey);
           int nextPacmanPositionX = userX + direction[0];
           int nextPacmanPositionY = userY + direction[1];
           char nextCell = map[nextPacmanPositionX, nextPacmanPositionY];
           if ( nextCell == ' '|| nextCell == '.')
           {
               userX = nextPacmanPositionX;
               userY = nextPacmanPositionY;
               if (nextCell == '.')
               {
                   score++;
                   map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
               }
           }

       }
       private static int[] GetDirection(ConsoleKeyInfo pressedKey)
       {
           int[] direction = {0, 0};
           if (pressedKey.Key == ConsoleKey.UpArrow)
           {
               direction[1]--;
           }
           else if (pressedKey.Key == ConsoleKey.DownArrow)
           {
               direction[1]++;
           }
           else if (pressedKey.Key == ConsoleKey.LeftArrow)
           {
               direction[0]--;
           }
           else if (pressedKey.Key == ConsoleKey.RightArrow)
           {
               direction[0]++;
           }

           return direction;
       }
        private static void DrawMap(char[,]map)
        {
            for (int y = 0; y < map.GetLength(1); y++) 
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }
        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("map.txt");
            char[,] map = new char[GetLenghtOfLine(file), file.Length];
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = file[y][x];
                }
            }
            return map;
        }
        private static int GetLenghtOfLine(string[] lines)
        {
            int maxLenght = lines[0].Length;
            foreach(var line in lines) 
            {
                if(line.Length > maxLenght) 
                {
                    maxLenght = line.Length;
                }
            }
            return maxLenght; 
        }
        static void Main(string[] args)
        {
            char[,] map = ReadMap("map.text");
            Console.CursorVisible = false;
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });
            int userX = 1;
            int userY = 1;
            int score = 0;
            while (true)
            {
                Console.Clear();
                DrawMap(map);
                Console.SetCursorPosition(userY, userX);
                Console.Write('@');
                Console.SetCursorPosition(35, 0);
                Console.Write($"score: {score} ");
                HandleInput(pressedKey, ref userY, ref userX, map, ref score);
                Thread.Sleep(1000);
            }
        }
    }
}
