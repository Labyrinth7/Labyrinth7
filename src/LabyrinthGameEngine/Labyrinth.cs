namespace LabyrinthGameEngine
{
    using GameHandler.Interfaces;
    using LabyrinthGameEngine.Interfaces;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;

    [Serializable]
    internal class Labyrinth : ILabyrinth, IEnumerable
    {
        internal const char WALL_SYMBOL = 'x';
        internal const char BLANK_SYMBOL = '-';
        internal const char PLAYER_SYMBOL = '*';

        private char[,] matrix;

        internal Labyrinth(char[,] matrix)
        {
            this.Rows = matrix.GetLength(0);
            this.Cols = matrix.GetLength(1);

            this.matrix = matrix;
        }

        public int Rows { get; private set; }
        public int Cols { get; private set; }

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

        public override string ToString()
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
    }
}