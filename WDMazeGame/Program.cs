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
        { '-', '-', '-', ' ', '-', ' ', '-', ' ', ' ', '-' },
        { '-', '-', '-', '-', '-', '-', '-', 'E', '-', '-' }
    };

        static (int x, int y) start = (1, 1); // Starting position of 'C'
        static (int x, int y) exit = (9, 7);  // Exit position of 'E'

        static void Main(string[] args)
        {
            Console.Clear();
            DisplayMaze();

            Console.WriteLine("\nAI Bot is solving the maze...");
            List<(int x, int y)> path = SolveMaze();

            if (path != null)
            {
                foreach (var step in path)
                {
                    Console.Clear();
                    LeaveTrail(start.x, start.y); // Leave a trail at the current position
                    MoveBot(step.x, step.y);
                    DisplayMaze();
                    System.Threading.Thread.Sleep(300); // Pause for visualization
                }

                Console.WriteLine("\nAI Bot reached the exit! Maze completed.");
            }
            else
            {
                Console.WriteLine("\nNo path to the exit found!");
            }
        }

        static List<(int x, int y)> SolveMaze()
        {
            // Directions: up, down, left, right
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            Queue<(int x, int y, List<(int, int)> path)> queue = new();
            HashSet<(int, int)> visited = new();

            queue.Enqueue((start.x, start.y, new List<(int, int)> { start }));
            visited.Add(start);

            while (queue.Count > 0)
            {
                var (currentX, currentY, path) = queue.Dequeue();

                if ((currentX, currentY) == exit)
                    return path; // Return the successful path

                for (int i = 0; i < 4; i++)
                {
                    int newX = currentX + dx[i];
                    int newY = currentY + dy[i];

                    if (IsValidMove(newX, newY) && !visited.Contains((newX, newY)))
                    {
                        visited.Add((newX, newY));
                        var newPath = new List<(int, int)>(path) { (newX, newY) };
                        queue.Enqueue((newX, newY, newPath));
                    }
                }
            }

            return null; // No path found
        }

        static bool IsValidMove(int x, int y)
        {
            return x >= 0 && x < maze.GetLength(0) &&
                   y >= 0 && y < maze.GetLength(1) &&
                   (maze[x, y] == ' ' || maze[x, y] == 'E');
        }

        static void MoveBot(int x, int y)
        {
            maze[start.x, start.y] = '.'; // Leave a trail at the old position
            maze[x, y] = 'C'; // Move the bot to the new position
            start = (x, y); // Update the bot's current position
        }

        static void LeaveTrail(int x, int y)
        {
            if (maze[x, y] != 'C' && maze[x, y] != 'E') // Ensure we don't overwrite the bot or the exit
            {
                maze[x, y] = '.'; // Mark the trail
            }
        }

        static void DisplayMaze()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
        }
    }

}