namespace LabyrinthGameEngine
{
    using GameHandler;
    using System;

    public static class GameEntry
    {
        public static void Main()
        {
            Type gameType = new LabyrinthGame().GetType();
            UserInterface userInterface = UserInterface.Console;

            Game.Instance.Run(gameType, userInterface);
        }
    }
}
