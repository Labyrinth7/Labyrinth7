namespace LabyrinthGameEngine
{
    using GameHandler;
    using LabyrinthGameEngine.Interfaces;
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Facade pattern for hte Labyrinth class.
    /// </summary>
    internal sealed class LabyrinthFacade
    {
        private static LabyrinthFacade instance;

        private IPlayer player;
        private GameState gameState;

        private int[] newPosition = null;

        private LabyrinthFacade()
        {
            LabyrinthFactory labyrinthFactory = new LabyrinthFactory();
            this.GameBoard = labyrinthFactory.CreateLabyrinth(LabyrinthGame.LABYRINTH_ROWS, LabyrinthGame.LABYRINTH_COLS);

            int initialPlayerPositionX = LabyrinthGame.LABYRINTH_COLS / 2;
            int initialPlayerPositionY = LabyrinthGame.LABYRINTH_ROWS / 2;

            this.Player = new Player(initialPlayerPositionX, initialPlayerPositionY);
        }

        /// <summary>
        /// Gets an instance of the Labyrinth.
        /// </summary>
        /// <returns>An instance of class Labyrinth.</returns>
        internal static LabyrinthFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new LabyrinthFacade();
            }
            return instance;
        }

        /// <summary>
        /// Gets or sets the current game state.
        /// </summary>
        internal GameState GameState
        {
            get
            {
                return this.gameState;
            }
            set
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

        private IPlayer Player
        {
            get
            {
                return this.player;
            }
            set
            {
                if (value is IPlayer)
                {
                    this.player = value;
                }
                else
                {
                    throw new ArgumentException("Inappropriate value for Player is set!");
                }
            }
        }

        private ILabyrinth GameBoard
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes and starts the game.
        /// </summary>
        internal void InitializeGame()
        {
            StringBuilder initialMessage = new StringBuilder();

            initialMessage.AppendLine("Welcome to \"Labyrinth\" game. Your goal is to escape!");
            initialMessage.AppendLine(new String('-', 50));
            initialMessage.AppendLine("Command 'top' : ".PadLeft(20) + "prints the top scoreboard.");
            initialMessage.AppendLine("Command 'restart' : ".PadLeft(20) + "starts a new game.");
            initialMessage.AppendLine("Command 'exit' : ".PadLeft(20) + "quits the game.");
            initialMessage.AppendLine(new String('-', 50));

            Console.WriteLine(initialMessage.ToString());

            this.GameState = GameState.Running;
        }

        /// <summary>
        /// Moves the player Up.
        /// </summary>
        internal void MoveUp()
        {
            newPosition = new int[] { this.Player.Position[0], this.Player.Position[1] - 1 };
            this.Move(this.GameBoard, newPosition);
        }

        /// <summary>
        /// Moves the player Right.
        /// </summary>
        internal void MoveRight()
        {
            newPosition = new int[] { this.Player.Position[0] + 1, this.Player.Position[1] };
            this.Move(this.GameBoard, newPosition);
        }

        /// <summary>
        /// Moves the player Down.
        /// </summary>
        internal void MoveDown()
        {
            newPosition = new int[] { this.Player.Position[0], this.Player.Position[1] + 1 };
            this.Move(this.GameBoard, newPosition);
        }

        /// <summary>
        /// Moves the player Left.
        /// </summary>
        internal void MoveLeft()
        {
            newPosition = new int[] { this.Player.Position[0] - 1, this.Player.Position[1] };
            this.Move(this.GameBoard, newPosition);
        }

        /// <summary>
        /// Displays the top ranked players.
        /// </summary>
        internal void DisplayTopPlayers()
        {
            string topResults = RankingTopPlayers.Instance.GetTopResults();
            Console.WriteLine(topResults);
            Console.WriteLine();
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        internal void RestartGame()
        {
            Console.WriteLine("\n");
            this.GameState = GameState.New;
            LabyrinthFacade.instance = null;
        }

        /// <summary>
        /// Quits the game.
        /// </summary>
        internal void ExitGame()
        {
            this.GameState = GameState.Quit;
        }

        /// <summary>
        /// Draws the labyrinth.
        /// </summary>
        internal void DrawGameBoard()
        {
            string labyrinthWithPlayer = this.GameBoard.AddPlayerToLabyrinth(this.Player.Position);
            Console.WriteLine(labyrinthWithPlayer);
        }

        private void Move(ILabyrinth labyrinth, int[] newPosition)
        {
            bool isValidMove = labyrinth[newPosition[1], newPosition[0]] == Labyrinth.BLANK_SYMBOL;

            if (!isValidMove)
            {
                throw new ArgumentException("Invalid move!");
            }

            this.Player.PositionX = newPosition[0];
            this.Player.PositionY = newPosition[1];
            this.Player.Moves++;

            if (this.Player.PositionX == 0 ||
                this.Player.PositionX == LabyrinthGame.LABYRINTH_ROWS - 1 ||
                this.Player.PositionY == 0 ||
                this.Player.PositionY == LabyrinthGame.LABYRINTH_COLS - 1)
            {
                SuccessfulEscape();
            }
        }

        private void SuccessfulEscape()
        {
            this.GameBoard.AddPlayerToLabyrinth(this.Player.Position);

            Console.WriteLine("\nCongratulations you escaped with {0} moves.\n", this.Player.Moves);

            RankingTopPlayers.Instance.AddToTopResults(this.Player);
            string topResults = RankingTopPlayers.Instance.GetTopResults();
            Console.WriteLine(topResults);

            LabyrinthFacade.instance = null;
            this.GameState = GameState.Over;
        }
    }
}
