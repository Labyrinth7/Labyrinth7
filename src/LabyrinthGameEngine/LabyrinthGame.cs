namespace LabyrinthGameEngine
{
    using GameHandler;
    using LabyrinthGameEngine.Interfaces;
    using System;
    using System.Collections.Generic;

    public class LabyrinthGame : IGame
    {
        private GameState gameState;
        private const int labyrinthRows = 7;
        private const int labyrinthCols = 7;

        private string[,] labyrinth;

        private const int upperSuccessfulFinish = 0;
        private const int downSuccessfulFinish = 6;
        private const int leftSuccessfulFinish = 0;
        private const int rightSuccesfulFinish = 6;

        public bool isInLabyrinth;
        public static bool isPlaying; // or isRunning
        public static bool isEscapedNaturally;

        public static int positionX;
        public static int positionY;
        public static int currentMoves;
        public static List<Player> scores = new List<Player>(4);

        public LabyrinthGame()
        {
            this.GameState = GameState.New;

            int initialPlayerPositionX = labyrinthCols / 2;
            int initialPlayerPositionY = labyrinthRows / 2;

            Player = new Player(initialPlayerPositionX, initialPlayerPositionY);

            isInLabyrinth = isPlaying = true;
            labyrinth = new string[7, 7];

            while (isPlaying) // IsRunning or IsPlaying
            {
                Console.WriteLine("Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top \nscoreboard,'restart' to start a new game and 'exit' to quit the game.\n ");
                Labyrinth.isLabyrinthValid = isEscapedNaturally = false;

                while (Labyrinth.isLabyrinthValid == false) // isLabyrinthIntializationIsValid
                {
                    Labyrinth.LabyrinthGenerator(labyrinth, this.Player.PositionX, this.Player.PositionY);
                    // Check is the generated labyrinth has a right path
                    Labyrinth.SolutionChecker(labyrinth, this.Player.PositionX, this.Player.PositionY);
                }

                // After intializing, here the labyrinth is still not rendered
                // Printing Labyrinth on console
                Labyrinth.DisplayLabyrinth(labyrinth);

                // The actual game - moving and checking
                Update();

                while (isEscapedNaturally) //used for adding score only when game is finished naturally and not by the restart command.
                {
                    Add(scores, currentMoves);
                }
            }
        }

        public IPlayer Player
        {
            get;
            set;
        }

        public GameState GameState
        {
            get
            {
                return this.gameState;
            }
            protected set
            {
                if (Enum.IsDefined(typeof(GameState), value))
                {
                    this.gameState = value;
                }
                else
                {
                    throw new ArgumentException("The chosen game state is not valid.");
                }
            }
        }

        public static void Add(List<Player> s, int m)
        {
            int count = s.Count;

            if (count != 0)
            {
                s.Sort(delegate(Player s1, Player s2) { return s1.Moves.CompareTo(s2.Moves); });
            }

            if (count == 5)
            {
                if (s[4].Moves > m)
                {
                    s.Remove(s[4]);
                    Console.WriteLine("Please enter your nickname");
                    string name = Console.ReadLine();
                    s[4].Name = name;
                    Player_(s);
                }
            }

            if (s.Count < 5)
            {
                Console.WriteLine("Please enter your nickname");
                string name = Console.ReadLine();
                s[count].Name = name;
                Player_(s);
            }

            // isEscapedNaturally = false, so the new iteration can begin again
            isEscapedNaturally = false;
        }

        // Printing the top 5 results
        public static void Player_(List<Player> scores)
        {
            Console.WriteLine("\n");
            if (scores.Count == 0) { Console.WriteLine("The scoreboard is empty! "); }
            else
            {
                int i = 1;
                scores.Sort(delegate(Player s1, Player s2) { return s1.Moves.CompareTo(s2.Moves); });
                Console.WriteLine("Top 5: \n");
                scores.ForEach(delegate(Player s)
                {
                    Console.WriteLine(String.Format(i + ". {1} ---> {0} moves", s.Moves, s.Name));
                    i++;   
                }
                );
                Console.WriteLine("\n");
            }
        }

        // Reading and displaying the new field
        public void Update()
        {
            // For what is current moves?
            //flag_temp is for isInLabyrinth
            // temp_flag is getting value from flag2
            currentMoves = 0;

            while (this.isInLabyrinth)  // is in labyrinth
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
                        if (this.labyrinth[this.Player.PositionX + 1, this.Player.PositionY] == "-")
                        {
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY] = "-";
                            this.labyrinth[this.Player.PositionX + 1, this.Player.PositionY] = "*";
                            this.Player.PositionX++;
                            currentMoves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");

                        }

                        if (this.Player.PositionX == downSuccessfulFinish)
                        {
                            isInLabyrinth = SuccessfulEscape(currentMoves);
                        }

                        Labyrinth.DisplayLabyrinth(labyrinth);
                        break;
                        // Rendering the nethis.Player.PositionXt labyrinth
                    case "u":
                        if (this.labyrinth[this.Player.PositionX - 1, this.Player.PositionY] == "-")
                        {
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY] = "-";
                            this.labyrinth[this.Player.PositionX - 1, this.Player.PositionY] = "*";
                            this.Player.PositionX--;
                            currentMoves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        if (this.Player.PositionX == upperSuccessfulFinish)
                        {
                            this.isInLabyrinth = SuccessfulEscape(currentMoves);
                        }

                        Labyrinth.DisplayLabyrinth(labyrinth);
                        break;
                    case "r":
                        if (this.labyrinth[this.Player.PositionX, this.Player.PositionY + 1] == "-")
                        {
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY] = "-";
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY + 1] = "*";
                            this.Player.PositionY++;
                            currentMoves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        if (this.Player.PositionY == rightSuccesfulFinish)
                        {
                            this.isInLabyrinth = SuccessfulEscape(currentMoves);
                        }

                        Labyrinth.DisplayLabyrinth(this.labyrinth);
                        break;
                    case "l":
                        if (labyrinth[this.Player.PositionX, this.Player.PositionY - 1] == "-")
                        {
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY] = "-";
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY - 1] = "*";
                            this.Player.PositionY--;
                            currentMoves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        if (this.Player.PositionY == leftSuccessfulFinish)
                        {
                            this.isInLabyrinth = SuccessfulEscape(currentMoves);
                        }

                        Labyrinth.DisplayLabyrinth(this.labyrinth);
                        break;
                    case "top":
                        Player_(scores);
                        Console.WriteLine("\n");
                        Labyrinth.DisplayLabyrinth(labyrinth);
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

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}