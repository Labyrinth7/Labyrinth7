using System;
using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    class Game
    {
        public static bool isLabyrinthValid;
        public static bool flag2;
        public static bool isPlaying; // or isRunning
        public static bool isEscapedNaturally;
        public static int positionX;
        public static int positionY;
        public static int currentMoves;
        public static List<Table> scores = new List<Table>(4);

        protected static void DisplayLabyrinth(string[,] labyrinth)
        {
            // 7 is the rows of labyrinth
            // This can be optimized by two nested loops iterating by rows and cols
            for (int linee = 0; linee < 7; linee++)
            {
                string s1 = labyrinth[linee, 0];
                string s2 = labyrinth[linee, 1];
                string s3 = labyrinth[linee, 2];
                string s4 = labyrinth[linee, 3];
                string s5 = labyrinth[linee, 4];
                string s6 = labyrinth[linee, 5];
                string s7 = labyrinth[linee, 6];

                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} ", s1, s2, s3, s4, s5, s6, s7);
            }
            Console.WriteLine();
        }

        // positionX and positionY
        protected static void LabyrinthGenerator(string[,] labyrinth, int x, int y)
        {
            Random randomInt = new Random();
            // They are using randomInt to get a random binary number 1 or 0 and then check if the number is 0 (empty position) they are fill the cell with '-' but they still DON'T PRINTING IT (just filling) !!!

            // what is 7?? we have to know rows and cols
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    // That converting to string can be removed for optimizing
                    labyrinth[i, j] = Convert.ToString(randomInt.Next(2));
                    if (labyrinth[i, j] == "0")
                    {
                        labyrinth[i, j] = "-";
                    }
                    else
                    {
                        labyrinth[i, j] = "x";
                    }
                }
            }
            // They are placing the starting position using a positionX and Y from Labyrinth class. Variant is to give them as a parameters in method
            labyrinth[positionX, positionY] = "*";
        }

        protected static void SolutionChecker(string[,] labyrinth, int x, int y)
        {
            // X and Y are the current position FOR NOW!!!
            // isCorrectPath
            bool checking = true;

            if (labyrinth[x + 1, y] == "x" && labyrinth[x, y + 1] == "x" && labyrinth[x - 1, y] == "x" && labyrinth[x, y - 1] == "x")
            {
                checking = false;
            }

            // If there is a correct path while is correct path
            while (checking)
            {
                try
                {
                    if (labyrinth[x + 1, y] == "-")
                    {
                        labyrinth[x + 1, y] = "0";
                        x++;
                    }
                    else if (labyrinth[x, y + 1] == "-")
                    {
                        labyrinth[x, y + 1] = "0";
                        y++;
                    }
                    else if (labyrinth[x - 1, y] == "-")
                    {
                        labyrinth[x - 1, y] = "0";
                        x--;
                    }
                    else if (labyrinth[x, y - 1] == "-")
                    {
                        labyrinth[x, y - 1] = "0";
                        y--;
                    }
                    else
                    {
                        checking = false;
                    }
                }
                catch (Exception)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            if (labyrinth[i, j] == "0")
                            {
                                labyrinth[i, j] = "-";
                            }
                        }

                        checking = false;
                        isLabyrinthValid = true;
                    }
                }
            }
        }
    }
}