namespace LabyrinthGameEngine
{
    using GameHandler;
    using GameHandler.DrawEngine;
    using GameHandler.Interfaces;

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

        private GameState gameState;
        private IPlayer player = null;
        private DrawableDataBuffer buffer = new DrawableDataBuffer();

        private int[] newPosition = null;

        private LabyrinthFacade()
        {
            LabyrinthFactory labyrinthFactory = new LabyrinthRectangularFactory();
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
            StringBuilder initialMessageBuilder = new StringBuilder();

            initialMessageBuilder.AppendLine("Welcome to \"Labyrinth\" game. Your goal is to escape!");
            initialMessageBuilder.AppendLine(new String('-', 50));
            initialMessageBuilder.AppendLine("Command 'top' : ".PadLeft(20) + "prints the top scoreboard.");
            initialMessageBuilder.AppendLine("Command 'restart' : ".PadLeft(20) + "starts a new game.");
            initialMessageBuilder.AppendLine("Command 'exit' : ".PadLeft(20) + "quits the game.");
            initialMessageBuilder.AppendLine(new String('-', 50));

            GameMessage initialMessage = new GameMessage(initialMessageBuilder.ToString());
            this.buffer.AddData(initialMessage);

            this.AddDrawableObjectsToBuffer();

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
            this.buffer.AddData(RankingTopPlayers.Instance);
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
        internal void AddDrawableObjectsToBuffer()
        {
            switch (this.gameState)
            {
                case GameState.New:
                case GameState.Running:
                    this.AddLabyrinthAndPlayerToDrawingBuffer();
                    break;
                case GameState.Win:
                    this.AddLabyrinthAndPlayerToDrawingBuffer();
                    this.SuccessfulEscapeMessage();
                    break;
                case GameState.TopResults:
                    this.buffer.AddData(RankingTopPlayers.Instance);
                    break;
            }
        }

        internal DrawableDataBuffer GetBuffer()
        {
            return this.buffer;
        }

        internal void SuccessfulEscape()
        {
            RankingTopPlayers.Instance.AddToTopResults(this.Player);
            this.GameState = GameState.TopResults;
            this.AddDrawableObjectsToBuffer();
        }

        internal void TopResults()
        {
            this.RestartGame();
        }

        private void AddLabyrinthAndPlayerToDrawingBuffer()
        {
            IDrawable labyrinthWithPlayer = new LabyrinthWithPlayer(this.GameBoard, this.Player.Position);
            this.buffer.AddData(labyrinthWithPlayer);
        }
        private void SuccessfulEscapeMessage()
        {
            String sucessfulEscapingString = String.Format("\nCongratulations you escaped with {0} moves.\n", this.Player.Moves);
            GameMessage sucessfulEscapingMessage = new GameMessage(sucessfulEscapingString);
            this.buffer.AddData(sucessfulEscapingMessage);
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

            this.CheckIfEscaped();

            this.AddDrawableObjectsToBuffer();
        }

        private void CheckIfEscaped()
        {
            if (this.Player.PositionX == 0 ||
                this.Player.PositionX == LabyrinthGame.LABYRINTH_COLS - 1 ||
                this.Player.PositionY == 0 ||
                this.Player.PositionY == LabyrinthGame.LABYRINTH_ROWS - 1)
            {
                this.GameState = GameState.Win;
            }

        }
    }
}
