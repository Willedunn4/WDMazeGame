using System;

namespace WDMazeGame
{
    using System;

    class Program
    {
        static char[,] maze = {
        { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' },
        { '-', 'C', ' ', ' ', '-', ' ', '-', ' ', ' ', '-' },
        { '-', '-', '-', ' ', '-', ' ', '-', ' ', '-', '-' },
        { '-', ' ', '-', ' ', ' ', ' ', ' ', ' ', '-', '-' },
        { '-', ' ', '-', '-', '-', ' ', '-', '-', '-', '-' },
        { '-', ' ', ' ', ' ', ' ', ' ', ' ', '-', ' ', '-' },
        { '-', ' ', '-', '-', '-', '-', ' ', '-', ' ', '-' },
        { '-', ' ', '-', ' ', ' ', '-', ' ', ' ', ' ', '-' },
        { '-', '-', '-', ' ', '-', ' ', '-', ' ', '-', '-' },
        { '-', '-', '-', '-', '-', '-', '-', 'E', '-', '-' }
    };

        static int playerX = 1; // Starting X position of 'C'
        static int playerY = 1; // Starting Y position of 'C'

        static void Main(string[] args)
        {
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.Clear();
                DisplayMaze();
                Console.WriteLine("Choose a direction (w=up, s=down, a=left, d=right):");
                char move = Console.ReadKey().KeyChar;

                switch (move)
                {
                    case 'w':
                        MovePlayer(-1, 0); // Up
                        break;
                    case 's':
                        MovePlayer(1, 0); // Down
                        break;
                    case 'a':
                        MovePlayer(0, -1); // Left
                        break;
                    case 'd':
                        MovePlayer(0, 1); // Right
                        break;
                    default:
                        Console.WriteLine("Invalid move!");
                        break;
                }

                if (maze[playerX, playerY] == 'E')
                {
                    Console.Clear();
                    DisplayMaze();
                    Console.WriteLine("Congratulations, you found the exit!");
                    gameRunning = false;
                }
            }
        }

        static void DisplayMaze()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (i == playerX && j == playerY)
                    {
                        Console.Write('C'); // Player's current position
                    }
                    else
                    {
                        Console.Write(maze[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        static void MovePlayer(int dx, int dy)
        {
            int newX = playerX + dx;
            int newY = playerY + dy;

            if (maze[newX, newY] == ' ' || maze[newX, newY] == 'E') // Valid movement
            {
                playerX = newX;
                playerY = newY;
            }
        }
    }

}