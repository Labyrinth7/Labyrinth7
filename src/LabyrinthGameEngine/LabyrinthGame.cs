namespace LabyrinthGameEngine
{
    using GameHandler;
    using System;

    public class LabyrinthGame : IGame
    {
        public const int LABYRINTH_COLS = 7;
        public const int LABYRINTH_ROWS = 7;

        private LabyrinthFacade fascade = null;

        public LabyrinthGame()
        {
            this.fascade = LabyrinthFacade.GetInstance();
            this.fascade.GameState = GameState.New;
        }

        public GameState GameState
        {
            get
            {
                return this.fascade.GameState;
            }
        }

        public void Initialize()
        {
            fascade.InitializeGame();
        }

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

        public void Draw()
        {
            fascade.DrawGameBoard();
        }
    }
}