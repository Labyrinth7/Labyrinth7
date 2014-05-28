using System;
using System.Collections.Generic;

// bachka!! yesss
namespace Labyrinth
{

    class Program : Game
    {
        private const int upperSuccessfulFinish = 0;
        private const int downSuccessfulFinish = 6;
        private const int leftSuccessfulFinish = 0;
        private const int rightSuccesfulFinish = 6;

        static void Main(string[] args)
        {
            positionX = positionY = 3;  // player position
            flag2 = isPlaying = true;
            string[,] labyrinth = new string[7, 7];

            while (isPlaying) // IsRunning or IsPlaying
            {
                Console.WriteLine("Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top \nscoreboard,'restart' to start a new game and 'exit' to quit the game.\n ");
                isLabyrinthValid = isEscapedNaturally = false;

                while (isLabyrinthValid == false) // isLabyrinthIntializationIsValid
                {
                    LabyrinthGenerator(labyrinth, positionX, positionY);
                    // Check is the generated labyrinth has a right path
                    SolutionChecker(labyrinth, positionX, positionY);
                }

                // After intializing, here the labyrinth is still not rendered
                // Printing Labyrinth on console
                DisplayLabyrinth(labyrinth);

                // The actual game - moving and checking
                Test(labyrinth, flag2, positionX, positionY);
                
                while (isEscapedNaturally) //used for adding score only when game is finished naturally and not by the restart command.
                {
                    Add(scores, currentMoves);
                }
            }
        }

        static void Add(List<Table> s, int m)
        {
            if (s.Count != 0)
            {
                s.Sort(delegate(Table s1, Table s2) { return s1.moves.CompareTo(s2.moves); });
            }

            if (s.Count == 5)
            {
                if (s[4].moves > m)
                {
                    s.Remove(s[4]);
                    Console.WriteLine("Please enter your nickname");
                    string name = Console.ReadLine();
                    s.Add(new Table(m, name));
                    Table_(s);
                }
            }

            if (s.Count < 5)
            {
                Console.WriteLine("Please enter your nickname");
                string name = Console.ReadLine();
                s.Add(new Table(m, name));
                Table_(s);
            }

            // isEscapedNaturally = false, so the new iteration can begin again
            isEscapedNaturally = false;
        }

        // Printing the top 5 results
        static void Table_(List<Table> scores)
        {
            Console.WriteLine("\n");
            if (scores.Count == 0) { Console.WriteLine("The scoreboard is empty! "); }
            else
            {
                int i = 1;
                scores.Sort(delegate(Table s1, Table s2) { return s1.moves.CompareTo(s2.moves); });
                Console.WriteLine("Top 5: \n");
                scores.ForEach(delegate(Table s)
                {
                    Console.WriteLine(String.Format(i+". {1} ---> {0} moves", s.moves, s.name));
                    i++;   
                }
                );
                Console.WriteLine("\n");
            }
        }

        // Reading and displaying the new field
        static void Test(string[,] labyrinth, bool isInLabyrinth, int x, int y)
        {
            // For what is current moves?
            //flag_temp is for isInLabyrinth
            // temp_flag is getting value from flag2
            currentMoves = 0;

            while (isInLabyrinth)  // is in labyrinth
            {
                // Reading the direction for moving
                Console.Write("\nEnter your move (L=left, R=right, D=down, U=up): ");
                // Direction
                string direction = string.Empty;
                direction = Console.ReadLine().ToLower();
           
                // All these cases can be combined in one
                switch (direction)
                {
                    // Checking for lower case? 
                    case "d":
                        if (labyrinth[x + 1, y] == "-")
                        {
                            labyrinth[x, y] = "-";
                            labyrinth[x + 1, y] = "*";
                            x++;
                            currentMoves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");

                        }

                        if (x == downSuccessfulFinish)
                        {
                           isInLabyrinth = SuccessfulEscape(currentMoves);
                        }

                        DisplayLabyrinth(labyrinth);
                        break;
                        // Rendering the next labyrinth
                    case "u":
                        if (labyrinth[x - 1, y] == "-")
                        {
                            labyrinth[x, y] = "-";
                            labyrinth[x - 1, y] = "*";
                            x--;
                            currentMoves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        if (x == upperSuccessfulFinish)
                        {
                            isInLabyrinth = SuccessfulEscape(currentMoves);
                        }

                        DisplayLabyrinth(labyrinth);
                        break;
                    case "r":
                        if (labyrinth[x, y + 1] == "-")
                        {
                            labyrinth[x, y] = "-";
                            labyrinth[x, y + 1] = "*";
                            y++;
                            currentMoves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        if (y == rightSuccesfulFinish)
                        {
                            isInLabyrinth = SuccessfulEscape(currentMoves);
                        }

                        DisplayLabyrinth(labyrinth);
                        break;
                    case "l":
                        if (labyrinth[x, y - 1] == "-")
                        {
                            labyrinth[x, y] = "-";
                            labyrinth[x, y - 1] = "*";
                            y--;
                            currentMoves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        if (y == leftSuccessfulFinish)
                        {
                            isInLabyrinth = SuccessfulEscape(currentMoves);
                        }

                        DisplayLabyrinth(labyrinth);
                        break;
                    case "top":
                        Table_(scores);
                        Console.WriteLine("\n");
                        DisplayLabyrinth(labyrinth);
                        break;
                    case "restart":
                        isInLabyrinth = false;
                        break;
                    case "exit":
                        Console.WriteLine("Good bye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }
        }

        private static bool SuccessfulEscape(int totalMoves)
        {
            Console.WriteLine("\nCongratulations you escaped with {0} moves.\n", totalMoves);
            bool IsInLabyrinth = false;
            // isEscaped naturally (not restarted or exit)
            isEscapedNaturally = true;
            return IsInLabyrinth;
        }
    }
}