namespace LabyrinthGameEngine
{
    using System;
    using System.Linq;

    public class LabyrinthFactory
    {
        public Labyrinth CreateLabyrinth(int labyrinthRows, int labyrinthCols)
        {
            Labyrinth currentLabyrinth = new Labyrinth(labyrinthRows, labyrinthCols);

            while (!CheckIfAnyExit(currentLabyrinth.Clone<Labyrinth>()))
            {
                currentLabyrinth.FillMatrix();
            }

            return currentLabyrinth;
        }

        public bool CheckIfAnyExit(Labyrinth labyrinth)
        {
            char wallSymbol = labyrinth.WallSymbol;
            char blankSymbol = labyrinth.BlankSymbol;

            int x = labyrinth.CenterOfCols;
            int y = labyrinth.CenterOfRows;

            // isCorrectPath
            bool checking = true;

            if (labyrinth[x + 1, y] == wallSymbol && labyrinth[x, y + 1] == wallSymbol && labyrinth[x - 1, y] == wallSymbol && labyrinth[x, y - 1] == wallSymbol)
            {
                checking = false;
            }

            // If there is a correct path while is correct path
            while (checking)
            {
                try
                {
                    if (labyrinth[x + 1, y] == blankSymbol)
                    {
                        labyrinth[x + 1, y] = '0';
                        x++;
                    }
                    else if (labyrinth[x, y + 1] == blankSymbol)
                    {
                        labyrinth[x, y + 1] = '0';
                        y++;
                    }
                    else if (labyrinth[x - 1, y] == blankSymbol)
                    {
                        labyrinth[x - 1, y] = '0';
                        x--;
                    }
                    else if (labyrinth[x, y - 1] == blankSymbol)
                    {
                        labyrinth[x, y - 1] = '0';
                        y--;
                    }
                    else
                    {
                        checking = false;
                    }
                }
                catch (Exception)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
