namespace LabyrinthGameEngine
{
    using System;

    public static class GameEntry
    {
        public static void Main(string[] args)
        {
            LabyrinthGame.positionX = LabyrinthGame.positionY = 3;  // player position
            LabyrinthGame.flag2 = LabyrinthGame.isPlaying = true;
            string[,] labyrinth = new string[7, 7];

            while (LabyrinthGame.isPlaying) // IsRunning or IsPlaying
            {
                Console.WriteLine("Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top \nscoreboard,'restart' to start a new game and 'exit' to quit the game.\n ");
                Labyrinth.isLabyrinthValid = LabyrinthGame.isEscapedNaturally = false;

                while (Labyrinth.isLabyrinthValid == false) // isLabyrinthIntializationIsValid
                {
                    Labyrinth.LabyrinthGenerator(labyrinth, LabyrinthGame.positionX, LabyrinthGame.positionY);
                    // Check is the generated labyrinth has a right path
                    Labyrinth.SolutionChecker(labyrinth, LabyrinthGame.positionX, LabyrinthGame.positionY);
                }

                // After intializing, here the labyrinth is still not rendered
                // Printing Labyrinth on console
                Labyrinth.DisplayLabyrinth(labyrinth);

                // The actual game - moving and checking
                LabyrinthGame.Test(labyrinth, LabyrinthGame.flag2, LabyrinthGame.positionX, LabyrinthGame.positionY);

                while (LabyrinthGame.isEscapedNaturally) //used for adding score only when game is finished naturally and not by the restart command.
                {
                    LabyrinthGame.Add(LabyrinthGame.scores, LabyrinthGame.currentMoves);
                }
            }
        }
    }
}
