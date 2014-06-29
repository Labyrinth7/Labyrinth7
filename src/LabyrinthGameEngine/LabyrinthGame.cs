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

        public LabyrinthGame()
        {
            this.GameState = GameState.New;

            int initialPlayerPositionX = labyrinthCols / 2;
            int initialPlayerPositionY = labyrinthRows / 2;

            Player = new Player(initialPlayerPositionX, initialPlayerPositionY);

            LabyrinthFactory labyrinthFactory = new LabyrinthFactory();
            this.GameBoard = labyrinthFactory.CreateLabyrinth(labyrinthRows, labyrinthCols);
        }

        public IPlayer Player
        {
            get;
            set;
        }

        public Labyrinth GameBoard
        {
            get;
            private set;
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

        public void Initialize()
        {
            Console.WriteLine("Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top \nscoreboard, 'restart' to start a new game and 'exit' to quit the game.\n ");
            this.GameState = GameState.Running;
        }

        public void Update()
        {
            Console.Write("\nEnter your move (L=left, R=right, D=down, U=up): ");

            string inputCommand = string.Empty;
            inputCommand = Console.ReadLine().ToLower();

            switch (inputCommand)
            {
                case "u":
                    this.Move(this.GameBoard, this.Player.Position, Direction.Up);
                    break;

                case "r":
                    this.Move(this.GameBoard, this.Player.Position, Direction.Right);
                    break;

                case "d":
                    this.Move(this.GameBoard, this.Player.Position, Direction.Down);
                    break;

                case "l":
                    this.Move(this.GameBoard, this.Player.Position, Direction.Left);
                    break;

                case "top":
                    string topResults = RankingTopPlayers.Instance.GetTopResults();
                    Console.WriteLine(topResults);
                    Console.WriteLine();
                    break;

                case "restart":
                    this.GameState = GameState.New;
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

        public void Draw()
        {
            Console.WriteLine(this.GameBoard.
                ConvertLabyrinthToString(this.Player.Position));
        }

        private void Move(Labyrinth labyrinth, int[] oldPosition, Direction direction)
        {
            int[] newPosition = null;

            switch (direction)
            {
                case Direction.Up:
                    newPosition = new int[] { oldPosition[0], oldPosition[1] - 1 };
                    break;
                case Direction.Right:
                    newPosition = new int[] { oldPosition[0] + 1, oldPosition[1] };
                    break;
                case Direction.Down:
                    newPosition = new int[] { oldPosition[0], oldPosition[1] + 1 };
                    break;
                case Direction.Left:
                    newPosition = new int[] { oldPosition[0] - 1, oldPosition[1] };
                    break;
            }

            bool isValidMove = labyrinth[newPosition[1], newPosition[0]] == labyrinth.BlankSymbol;

            if (isValidMove)
            {
                this.Player.PositionX = newPosition[0];
                this.Player.PositionY = newPosition[1];
                this.Player.Moves++;

                if (this.Player.PositionX == 0 ||
                    this.Player.PositionX == labyrinthRows - 1 ||
                    this.Player.PositionY == 0 ||
                    this.Player.PositionY == labyrinthCols - 1)
                {
                    SuccessfulEscape();
                }
            }
            else
            {
                Console.WriteLine("\nInvalid move! \n ");
            }
        }

        private bool SuccessfulEscape()
        {
            this.GameBoard.ConvertLabyrinthToString(this.Player.Position);

            Console.WriteLine("\nCongratulations you escaped with {0} moves.\n", this.Player.Moves);

            this.GameState = GameState.Over;

            RankingTopPlayers.Instance.AddToTopResults(this.Player);

            string topResults = RankingTopPlayers.Instance.GetTopResults();
            Console.WriteLine(topResults);

            return false;
        }
    }
}