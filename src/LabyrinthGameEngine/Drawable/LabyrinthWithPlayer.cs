namespace LabyrinthGameEngine
{
    using GameHandler.Interfaces;
    using LabyrinthGameEngine.Interfaces;
    using System;

    internal class LabyrinthWithPlayer : IDrawable
    {
        private ILabyrinth labyrinth = null;
        private int[] playerPosition = null;

        internal LabyrinthWithPlayer(ILabyrinth labyrinth, int[] playerPosition)
        {
            this.labyrinth = labyrinth;
            this.playerPosition = playerPosition;
        }
        
        /// <summary>
        /// Adds the player to the labyrinth at a given position.
        /// </summary>
        /// <param name="playerPosition">Given position of the player.</param>
        /// <returns>The Labyrinth with added player as string.</returns>

        public object GetDrawableData()
        {
            string labyrinthAsString = this.labyrinth.ToString();

            char[] whitespace = new char[] { ' ' };
            string[] cells = labyrinthAsString.Split(whitespace);

            int rowNumber = this.playerPosition[1];
            int colNumber = this.playerPosition[0];

            int newLinesNumber = rowNumber;
            int cellWithPlayerNumber = rowNumber * this.labyrinth.Cols + colNumber - newLinesNumber;

            cells[cellWithPlayerNumber] = Labyrinth.PLAYER_SYMBOL.ToString();
            string labyrinthWithPlayer = String.Join(" ", cells);

            return labyrinthWithPlayer;
        }
    }
}
