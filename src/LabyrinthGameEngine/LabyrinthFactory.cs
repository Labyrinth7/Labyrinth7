namespace LabyrinthGameEngine
{
    using System;

    internal class LabyrinthFactory : LabyrinthFactoryAbstract
    {
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
    }
}
