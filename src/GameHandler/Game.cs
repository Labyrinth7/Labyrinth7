namespace GameHandler
{
    using GameHandler.DrawEngine;
    using System;

    public sealed class Game
    {
        private static Game instance = null;
        private UserInterface currentUi = UserInterface.Console;
        private IDrawHandler drawHandler = null;
        private IGame currentGame = null;
        private bool running = true;

        private Game()
        {
        }

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

        public IGame CurrentGame
        {
            get
            {
                return this.currentGame;
            }
            private set
            {
                this.currentGame = value;
            }
        }

        public void Run(Type gameType, UserInterface userInterface)
        {
            this.currentUi = userInterface;
            this.drawHandler = new DrawHandler(currentUi);
            this.GameType = gameType;

            this.currentGame = (IGame)Activator.CreateInstance(this.GameType);

            while (this.running)
            {
                switch (this.CurrentGame.GameState)
                {
                    case GameState.New:
                        Initialize();
                        break;
                    case GameState.Running:
                        Draw();
                        Update();
                        break;
                    case GameState.Quit:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void Initialize()
        {
            this.CurrentGame = (IGame)Activator.CreateInstance(this.GameType);
            this.CurrentGame.Initialize();
        }

        private void Update()
        {
            this.CurrentGame.Update();

            if (this.CurrentGame.GameState == GameState.Over)
            {
                this.CurrentGame = (IGame)Activator.CreateInstance(this.GameType);
            }
        }

        private void Draw()
        {
            //drawHandler.Draw();
            this.CurrentGame.Draw();
        }
    }
}


