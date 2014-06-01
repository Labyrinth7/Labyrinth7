namespace LabyrinthGameEngine
{
    using GameHandler;
    using LabyrinthGameEngine.Interfaces;
    using System;

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

        public LabyrinthGame()
        {
            this.GameState = GameState.New;

            int initialPlayerPositionX = labyrinthCols / 2;
            int initialPlayerPositionY = labyrinthRows / 2;

            Player = new Player(initialPlayerPositionX, initialPlayerPositionY);

            labyrinth = new string[7, 7];

            Labyrinth.isLabyrinthValid = false;

            while (Labyrinth.isLabyrinthValid == false)
            {
                Labyrinth.LabyrinthGenerator(labyrinth, initialPlayerPositionX, initialPlayerPositionY);
                // Check is the generated labyrinth has a right path
                Labyrinth.SolutionChecker(labyrinth, initialPlayerPositionX, initialPlayerPositionY);
            }

            this.isInLabyrinth = true;
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

        public void Update()
        {
            this.Player.Moves = 0;

            while (this.isInLabyrinth)
            {
                Console.Write("\nEnter your move (L=left, R=right, D=down, U=up): ");
                string direction = string.Empty;
                direction = Console.ReadLine().ToLower();
           
                // All these cases can be combined in one
                switch (direction)
                {
                    case "d":
                        if (this.labyrinth[this.Player.PositionX + 1, this.Player.PositionY] == "-")
                        {
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY] = "-";
                            this.labyrinth[this.Player.PositionX + 1, this.Player.PositionY] = "*";
                            this.Player.PositionX++;
                            this.Player.Moves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");

                        }

                        Labyrinth.DisplayLabyrinth(this.labyrinth);

                        if (this.Player.PositionX == downSuccessfulFinish)
                        {
                            isInLabyrinth = SuccessfulEscape();
                        }

                        break;
                        // Rendering the nethis.Player.PositionXt labyrinth
                    case "u":
                        if (this.labyrinth[this.Player.PositionX - 1, this.Player.PositionY] == "-")
                        {
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY] = "-";
                            this.labyrinth[this.Player.PositionX - 1, this.Player.PositionY] = "*";
                            this.Player.PositionX--;
                            this.Player.Moves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        Labyrinth.DisplayLabyrinth(this.labyrinth);

                        if (this.Player.PositionX == upperSuccessfulFinish)
                        {
                            this.isInLabyrinth = SuccessfulEscape();
                        }

                        break;
                    case "r":
                        if (this.labyrinth[this.Player.PositionX, this.Player.PositionY + 1] == "-")
                        {
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY] = "-";
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY + 1] = "*";
                            this.Player.PositionY++;
                            this.Player.Moves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        Labyrinth.DisplayLabyrinth(this.labyrinth);

                        if (this.Player.PositionY == rightSuccesfulFinish)
                        {
                            this.isInLabyrinth = SuccessfulEscape();
                        }

                        break;
                    case "l":
                        if (labyrinth[this.Player.PositionX, this.Player.PositionY - 1] == "-")
                        {
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY] = "-";
                            this.labyrinth[this.Player.PositionX, this.Player.PositionY - 1] = "*";
                            this.Player.PositionY--;
                            this.Player.Moves++;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid move! \n ");
                        }

                        Labyrinth.DisplayLabyrinth(this.labyrinth);

                        if (this.Player.PositionY == leftSuccessfulFinish)
                        {
                            this.isInLabyrinth = SuccessfulEscape();
                        }

                        break;
                    case "top":
                        RankingTopPlayers.Instance.PrintTopResults();
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

        private bool SuccessfulEscape()
        {
            // this.GameBoard.Display();
            Console.WriteLine("\nCongratulations you escaped with {0} moves.\n", this.Player.Moves);
            this.GameState = GameState.Over;
            RankingTopPlayers.Instance.AddToTopResults(this.Player);
            RankingTopPlayers.Instance.PrintTopResults();

            return false;
        }

        public void Initialize()
        {
            Console.WriteLine("Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top \nscoreboard, 'restart' to start a new game and 'exit' to quit the game.\n ");
            this.GameState = GameState.Running;
        }

        public void Draw()
        {
            Labyrinth.DisplayLabyrinth(labyrinth);
        }
    }
}