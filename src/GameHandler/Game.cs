namespace GameHandler
{
    using GameHandler.DrawEngine;
    using GameHandler.Interfaces;
    using System;

    public sealed class Game
    {
        private IDrawHandler drawHandler = null;
        private IGame currentGame = null;
        private bool running = true;
        private DrawableDataBuffer buffer = null;

        private static Game instance = null;
        private Game() { }

        /// <summary>
        /// Returns the single instance of the Game object.
        /// </summary>
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }

                return instance;
            }
        }

        public Type GameType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the current game object.
        /// </summary>
        public IGame CurrentGame
        {
            get
            {
                return this.currentGame;
            }
            private set
            {
                if (value is IGame) {
                    this.currentGame = value;
                }
            }
        }

        /// <summary>
        /// Runs the game
        /// </summary>
        /// <param name="gameType">Selected gametype.</param>
        /// <param name="userInterface">Selected UI.</param>
        public void Run(Type gameType, UserInterface userInterface)
        {
            this.GameType = gameType;
            this.currentGame = (IGame)Activator.CreateInstance(this.GameType);

            switch (userInterface)
            {
                case UserInterface.Console:
                    this.drawHandler = new ConsoleUiDrawHandler();
                    break;
                default:
                    throw new ArgumentException("The selected user interface is not available.");
            }

            while (this.running)
            {
                switch (this.CurrentGame.GameState)
                {
                    case GameState.New:
                    case GameState.Over:
                        Initialize();
                        break;
                    case GameState.Running:
                        Update();
                        break;
                    case GameState.Win:
                        this.CurrentGame.SuccessfulEscape();
                        break;
                    case GameState.TopResults:
                        this.CurrentGame.TopResults();
                        break;
                    case GameState.Quit:
                        Console.WriteLine("Good bye!");
                        Environment.Exit(0);
                        break;
                }

                this.Draw();
            }
        }

        /// <summary>
        /// Initializes a new game.
        /// </summary>
        private void Initialize()
        {
            this.CurrentGame = (IGame)Activator.CreateInstance(this.GameType);
            this.CurrentGame.Initialize();
        }

        /// <summary>
        /// Updates the current game.
        /// </summary>
        private void Update()
        {
            this.CurrentGame.Update();

            if (this.CurrentGame.GameState == GameState.Over)
            {
                this.CurrentGame = (IGame)Activator.CreateInstance(this.GameType);
            }
        }

        /// <summary>
        /// Draws the game board.
        /// </summary>
        private void Draw()
        {
            this.buffer = this.currentGame.GetBuffer();
            this.drawHandler.Draw(this.buffer);
            this.buffer.EmptyBuffer();
        }
    }
}


