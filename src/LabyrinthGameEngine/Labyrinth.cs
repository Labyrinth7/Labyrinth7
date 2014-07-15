namespace LabyrinthGameEngine
{
    using System;
    using System.Linq;
    using System.Text;

    [Serializable]
    internal class Labyrinth
    {
        internal const char WALL_SYMBOL = 'x';
        internal const char BLANK_SYMBOL = '-';
        internal const char PLAYER_SYMBOL = '*';

        private char[,] matrix;

        internal Labyrinth(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
            this.CenterOfCols = this.Cols / 2;
            this.CenterOfRows = this.Rows / 2;

            matrix = new char[this.Rows, this.Cols];

            FillMatrix();
        }

        internal int Rows { get; private set; }
        internal int Cols { get; private set; }
        internal int CenterOfCols { get; private set; }
        internal int CenterOfRows { get; private set; }

        internal char this[int row, int col]
        {
            get
            {
                return this.matrix[row, col];
            }
            set
            {
                this.matrix[row, col] = value;
            }
        }

        internal void Display(int[] playerPosition)
        {
            StringBuilder matrixAsStringBuilder = new StringBuilder();

            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Cols; col++)
                {
                    if (playerPosition[1] == row && playerPosition[0] == col)
                    {
                        matrixAsStringBuilder.Append(Labyrinth.PLAYER_SYMBOL);
                    }
                    else
                    {
                        matrixAsStringBuilder.Append(this.matrix[row, col]);
                    }

                    if (col < this.Cols - 1)
                    {
                        matrixAsStringBuilder.Append(" ");
                    }
                }
                matrixAsStringBuilder.Append("\n");
            }

            Console.WriteLine(matrixAsStringBuilder.ToString());
        }

        internal void FillMatrix()
        {
            Random randomInt = new Random();

            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Cols; col++)
                {
                    int randomNumber = randomInt.Next(2);

                    if (randomNumber == 0)
                    {
                        this.matrix[row, col] = Labyrinth.BLANK_SYMBOL;
                    }
                    else if (randomNumber == 1)
                    {
                        this.matrix[row, col] = Labyrinth.WALL_SYMBOL;
                    }
                }
            }

            this.matrix[this.CenterOfRows, this.CenterOfCols] = Labyrinth.BLANK_SYMBOL;
        }
    }
}