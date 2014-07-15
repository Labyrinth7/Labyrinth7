namespace LabyrinthGameEngine
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;

    [Serializable]
    internal class Labyrinth : IEnumerable
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

        internal string AddPlayerToLabyrinth(int[] playerPosition)
        {
            string labyrinthAsString = this.ToString();

            char[] whitespace = new char[] { ' ' };
            string[] cells = labyrinthAsString.Split(whitespace);

            int rowNumber = playerPosition[1];
            int colNumber = playerPosition[0];

            int newLinesNumber = rowNumber;
            int cellWithPlayerNumber = rowNumber * this.Cols + colNumber - newLinesNumber;

            cells[cellWithPlayerNumber] = Labyrinth.PLAYER_SYMBOL.ToString();
            string labyrinthWithPlayer = String.Join(" ", cells);

            return labyrinthWithPlayer;
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

        internal string ToString()
        {
            StringBuilder matrixAsStringBuilder = new StringBuilder();
            
            int index = 1;
            int lastCellNumber = this.Cols * this.Rows;

            foreach (var cell in this) {
                matrixAsStringBuilder.Append(cell);

                if (index % this.Cols != 0)
                {
                    matrixAsStringBuilder.Append(" ");
                }
                else if (index != lastCellNumber)
                {
                    matrixAsStringBuilder.Append(Environment.NewLine);
                }

                index++;
            }

            return matrixAsStringBuilder.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Cols; col++)
                {
                    yield return this.matrix[row, col];
                }
            }
        }
    }
}