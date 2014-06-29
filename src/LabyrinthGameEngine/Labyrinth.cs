namespace LabyrinthGameEngine
{
    using System;
    using System.Linq;
    using System.Text;

    [Serializable]
    public class Labyrinth
    {
        private char[,] matrix;

        public Labyrinth(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
            this.CenterOfCols = this.Cols / 2;
            this.CenterOfRows = this.Rows / 2;

            matrix = new char[this.Rows, this.Cols];

            FillMatrix();
        }

        public char WallSymbol
        {
            get
            {
                return 'x';
            }
        }
        public char BlankSymbol
        {
            get
            {
                return '-';
            }
        }
        public char PlayerSymbol
        {
            get
            {
                return '*';
            }
        }
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int CenterOfCols { get; private set; }
        public int CenterOfRows { get; private set; }

        public char this[int row, int col]
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

        public string Display(int[] playerPosition)
        {
            StringBuilder matrixAsStringBuilder = new StringBuilder();

            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Cols; col++)
                {
                    if (playerPosition[1] == row && playerPosition[0] == col)
                    {
                        matrixAsStringBuilder.Append(this.PlayerSymbol);
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

            return matrixAsStringBuilder.ToString();
        }

        public void FillMatrix()
        {
            Random randomInt = new Random();

            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Cols; col++)
                {
                    int randomNumber = randomInt.Next(2);

                    if (randomNumber == 0)
                    {
                        this.matrix[row, col] = '-';
                    }
                    else if (randomNumber == 1)
                    {
                        this.matrix[row, col] = 'x';
                    }
                }
            }

            this.matrix[this.CenterOfRows, this.CenterOfCols] = '-';
        }
    }
}