using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            Random randomNumbersGenerator = new Random();
            int score = 0;

            int snakeHeadX = screenWidth / 2;
            int snakeHeadY = screenHeight / 2;
            int snakeLength = 5;
            int direction = 0; // 0 - right, 1 - down, 2 - left, 3 - up

            List<int> snakeX = new List<int>();
            List<int> snakeY = new List<int>();
            int foodX = randomNumbersGenerator.Next(1, screenWidth-1);
            int foodY = randomNumbersGenerator.Next(1, screenHeight-1);
            string snakeSymbol = "\u25A0";
            char foodSymbol = '$';
            TimeSpan gameSpeed = TimeSpan.FromMilliseconds(100);

            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            DrawBorder(screenWidth, screenHeight);

            while (true)
            {
                bool isMoving = false;

                while (!Console.KeyAvailable)
                {
                    if (direction == 0) // right
                    {
                        snakeHeadX++;
                    }
                    if (direction == 1) // down
                    {
                        snakeHeadY++;
                    }
                    if (direction == 2) // left
                    {
                        snakeHeadX--;
                    }
                    if (direction == 3) // up
                    {
                        snakeHeadY--;
                    }

                    if (snakeHeadX >= screenWidth - 1 || snakeHeadX < 1 || snakeHeadY >= screenHeight - 1 || snakeHeadY < 1)
                    {
                        // Game over - snake hit the wall
                        Console.SetCursorPosition(screenWidth / 2 - 4, screenHeight / 2);
                        Console.WriteLine("Game Over!");
                        Console.SetCursorPosition(screenWidth / 2 - 6, screenHeight / 2 + 1);
                        Console.WriteLine("Your score: " + score);
                        if (screenHeight < Console.BufferHeight)
                        {
                            Console.SetCursorPosition(0, screenHeight);
                        }
                        return;
                    }

                    if (snakeX.Contains(snakeHeadX) && snakeY.Contains(snakeHeadY))
                    {
                        // Game over - snake hit itself
                        Console.SetCursorPosition(screenWidth / 2 - 4, screenHeight / 2);
                        Console.WriteLine("Game Over!");
                        Console.SetCursorPosition(screenWidth / 2 - 6, screenHeight / 2 + 1);
                        Console.WriteLine("Your score: " + score);
                        if (screenHeight < Console.BufferHeight)
                        {
                            Console.SetCursorPosition(0, screenHeight);
                        }
                        return;
                    }

                    Console.SetCursorPosition(snakeHeadX, snakeHeadY);
                    Console.Write(snakeSymbol);
                    snakeX.Add(snakeHeadX);
                    snakeY.Add(snakeHeadY);

                    if (snakeX.Count > snakeLength)
                    {
                        Console.SetCursorPosition(snakeX[0], snakeY[0]);
                        Console.Write(' ');
                        snakeX.RemoveAt(0);
                        snakeY.RemoveAt(0);
                    }

                    if (snakeHeadX == foodX && snakeHeadY == foodY)
                    {
                        // Snake ate the food
                        score++;
                        snakeLength++;

                        foodX = randomNumbersGenerator.Next(0, screenWidth);
                        foodY = randomNumbersGenerator.Next(0, screenHeight);
                    }

                    Thread.Sleep(gameSpeed);
                    isMoving = true;

                    if (isMoving)
                    {
                        Console.SetCursorPosition(foodX, foodY);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(foodSymbol);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo userInput = Console.ReadKey();

                        if (userInput.Key == ConsoleKey.LeftArrow && direction != 0)
                        {
                            direction = 2; // left
                        }
                        if (userInput.Key == ConsoleKey.UpArrow && direction != 1)
                        {
                            direction = 3; // up
                        }
                        if (userInput.Key == ConsoleKey.RightArrow && direction != 2)
                        {
                            direction = 0; // right
                        }
                        if (userInput.Key == ConsoleKey.DownArrow && direction != 3)
                        {
                            direction = 1; // down
                        }
                    }
                }

            }
        }

        static void DrawBorder(int screenWidth, int screenHeight)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < screenWidth; i++)
            {
                Console.Write('#');
            }

            for (int i = 1; i < screenHeight - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write('#');
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write('#');
            }

            Console.SetCursorPosition(0, screenHeight - 1);
            for (int i = 0; i < screenWidth; i++)
            {
                Console.Write('#');
            }
        }
    }
}
