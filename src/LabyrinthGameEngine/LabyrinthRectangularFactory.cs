namespace LabyrinthGameEngine
{
    using LabyrinthGameEngine.Interfaces;
    using System;

    internal class LabyrinthRectangularFactory : LabyrinthFactory
    {
        public override ILabyrinth CreateLabyrinth(int labyrinthRows, int labyrinthCols)
        {
            base.labyrinthRows = labyrinthRows;
            base.labyrinthCols = labyrinthCols;

            base.centerOfCols = base.labyrinthCols / 2;
            base.centerOfRows = base.labyrinthRows / 2;

            int initialPlayerPositionX = centerOfCols;
            int initialPlayerPositionY = centerOfRows;

            base.currentMatrix = this.GenerateMatrix();

            while (!CheckIfAnyExit((char[,])this.currentMatrix.Clone(), initialPlayerPositionX, initialPlayerPositionY))
            {
                this.currentMatrix = this.GenerateMatrix();
            }

            return new Labyrinth(this.currentMatrix);
        }

        /// <summary>
        /// Generates a new labyrinth.
        /// </summary>
        /// <returns>A new matrix representing the generated labyrinth.</returns>
        protected override char[,] GenerateMatrix()
        {
            char[,] generatedMatrix = new char[base.labyrinthRows, base.labyrinthCols];

            Random randomInt = new Random();

            for (int row = 0; row < base.labyrinthRows; row++)
            {
                for (int col = 0; col < base.labyrinthCols; col++)
                {
                    int randomNumber = randomInt.Next(2);

                    if (randomNumber == 0)
                    {
                        generatedMatrix[row, col] = Labyrinth.BLANK_SYMBOL;
                    }
                    else if (randomNumber == 1)
                    {
                        generatedMatrix[row, col] = Labyrinth.WALL_SYMBOL;
                    }
                }
            }

            generatedMatrix[base.centerOfRows, base.centerOfCols] = Labyrinth.BLANK_SYMBOL;

            return generatedMatrix;
        }

        /// <summary>
        /// Checks if given labyrinth has valid exit.
        /// </summary>
        /// <param name="labyrinth">Given matrix, representing a labyrinth.</param>
        /// <param name="positionX">Col position of the matrix.</param>
        /// <param name="positionY">Row position of the matrix.</param>
        /// <returns>True if the given labyrinth has exit and False if it hasnt.</returns>
        protected override bool CheckIfAnyExit(char[,] labyrinth, int positionX, int positionY)
        {
            char visitedSymbol = '0';

            int botStartPositionX = positionX;
            int botStartPositionY = positionY;

            labyrinth[botStartPositionY, botStartPositionX] = visitedSymbol;

            #region Border cases START
            if (botStartPositionX == 0 ||
                botStartPositionX == this.labyrinthCols - 1 ||
                botStartPositionY == 0 ||
                botStartPositionY == this.labyrinthRows - 1)
            {
                return true;
            }

            if ((labyrinth[botStartPositionY + 1, botStartPositionX] == Labyrinth.WALL_SYMBOL || labyrinth[botStartPositionY + 1, botStartPositionX] == visitedSymbol) &&
                (labyrinth[botStartPositionY, botStartPositionX + 1] == Labyrinth.WALL_SYMBOL || labyrinth[botStartPositionY, botStartPositionX + 1] == visitedSymbol) &&
                (labyrinth[botStartPositionY - 1, botStartPositionX] == Labyrinth.WALL_SYMBOL || labyrinth[botStartPositionY - 1, botStartPositionX] == visitedSymbol) &&
                (labyrinth[botStartPositionY, botStartPositionX - 1] == Labyrinth.WALL_SYMBOL || labyrinth[botStartPositionY, botStartPositionX - 1] == visitedSymbol))
            {
                return false;
            }
            #endregion Border cases END

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (CheckForExitInTheGivenDirections(labyrinth, botStartPositionX, botStartPositionY, direction, visitedSymbol))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if any exit from the labyrinth in the given direction.
        /// </summary>
        /// <param name="labyrinth">Matrix, representing the given labyrinth.</param>
        /// <param name="botStartPositionX">Current col position.</param>
        /// <param name="botStartPositionY">Current row position.</param>
        /// <param name="direction">Direction of movement.</param>
        /// <param name="visitedSymbol">Character for visited position.</param>
        /// <returns>True if the position is empty or False if the position is not empty.</returns>
        protected override bool CheckForExitInTheGivenDirections(char[,] labyrinth, int botStartPositionX, int botStartPositionY, Direction direction, char visitedSymbol)
        {
            bool isExit = false;

            int nextPositionY = botStartPositionY;
            int nextPositionX = botStartPositionX;

            switch (direction)
            {
                case Direction.Up:
                    nextPositionY = botStartPositionY - 1;
                    break;
                case Direction.Right:
                    nextPositionX = botStartPositionX + 1;
                    break;
                case Direction.Down:
                    nextPositionY = botStartPositionY + 1;
                    break;
                case Direction.Left:
                    nextPositionX = botStartPositionX - 1;
                    break;
            }

            if (labyrinth[nextPositionY, nextPositionX] == Labyrinth.BLANK_SYMBOL)
            {
                if (CheckIfAnyExit(labyrinth, nextPositionX, nextPositionY))
                {
                    isExit = true;
                }
            }
            return isExit;
        }
    }
}
