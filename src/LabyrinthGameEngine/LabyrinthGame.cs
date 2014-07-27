namespace LabyrinthGameEngine
{
    using GameHandler;
    using GameHandler.DrawEngine;
    using GameHandler.Interfaces;
    using System;

    public class LabyrinthGame : IGame
    {
        public const int LABYRINTH_COLS = 7;
        public const int LABYRINTH_ROWS = 15;

        private LabyrinthFacade fascade = null;

        public LabyrinthGame()
        {
            this.fascade = LabyrinthFacade.GetInstance();
            this.fascade.GameState = GameState.New;
        }

        /// <summary>
        /// Returns the current state of the game.
        /// </summary>
        public GameState GameState
        {
            get
            {
                return this.fascade.GameState;
            }
        }

        /// <summary>
        /// Initializes and starts the game.
        /// </summary>
        public void Initialize()
        {
            this.fascade.InitializeGame();
        }

        /// <summary>
        /// Updates the game.
        /// </summary>
        public void Update()
        {
            Console.Write("\nEnter your move (L=left, R=right, D=down, U=up): ");

            string inputCommand = Console.ReadLine().Trim().ToLower();

            try
            {
                switch (inputCommand)
                {
                    case "u":
                        this.fascade.MoveUp();
                        break;
                    case "r":
                        this.fascade.MoveRight();
                        break;
                    case "d":
                        this.fascade.MoveDown();
                        break;
                    case "l":
                        this.fascade.MoveLeft();
                        break;
                    case "top":
                        this.fascade.DisplayTopPlayers();
                        break;
                    case "restart":
                        this.fascade.RestartGame();
                        break;
                    case "exit":
                        this.fascade.ExitGame();
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }

        public void SuccessfulEscape()
        {
            this.fascade.SuccessfulEscape();
        }

        public void TopResults()
        {
            this.fascade.TopResults();
        }

        public void Restart()
        {
            this.fascade.RestartGame();
        }

        public DrawableDataBuffer GetBuffer()
        {
            return this.fascade.GetBuffer();
        }
    }
}