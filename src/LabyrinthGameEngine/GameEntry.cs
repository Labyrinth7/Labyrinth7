namespace LabyrinthGameEngine
{
    using GameHandler;
    using GameHandler.DrawEngine;
    using System;

    public static class GameEntry
    {
        /// <summary>
        /// Entry point for Labyrith game
        /// </summary>
        public static void Main()
        {
            Type gameType = new LabyrinthGame().GetType();
            UserInterface userInterface = UserInterface.Console;

            Game.Instance.Run(gameType, userInterface);
        }
    }
}
