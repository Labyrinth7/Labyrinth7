﻿namespace LabyrinthGameEngine
{
    using System;

    public class LabyrinthFactory
    {
        public Labyrinth CreateLabyrinth(int labyrinthRows, int labyrinthCols)
        {
            Labyrinth currentLabyrinth = new Labyrinth(labyrinthRows, labyrinthCols);

            int initialPlayerPositionX = currentLabyrinth.CenterOfCols;
            int initialPlayerPositionY = currentLabyrinth.CenterOfRows;

            while (!CheckIfAnyExit(currentLabyrinth.Clone<Labyrinth>(), initialPlayerPositionX, initialPlayerPositionY))
            {
                currentLabyrinth.FillMatrix();
            }

            return currentLabyrinth;
        }

        private bool CheckIfAnyExit(Labyrinth labyrinth, int positionX, int positionY)
        {
            char wallSymbol = labyrinth.WallSymbol;
            char blankSymbol = labyrinth.BlankSymbol;
            char visitedSymbol = '0';

            int botStartPositionX = positionX;
            int botStartPositionY = positionY;

            labyrinth[botStartPositionY, botStartPositionX] = visitedSymbol;

            // Border cases START
            if (botStartPositionX == 0 ||
                botStartPositionX == labyrinth.Rows - 1 ||
                botStartPositionY == 0 ||
                botStartPositionY == labyrinth.Cols - 1)
            {
                return true;
            }

            if (labyrinth[botStartPositionY + 1, botStartPositionX] == wallSymbol &&
                labyrinth[botStartPositionY, botStartPositionX + 1] == wallSymbol &&
                labyrinth[botStartPositionY - 1, botStartPositionX] == wallSymbol &&
                labyrinth[botStartPositionY, botStartPositionX - 1] == wallSymbol)
            {
                return false;
            }
            // Border cases END

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (Check(labyrinth, botStartPositionX, botStartPositionY, direction, visitedSymbol))
                {
                    return true;
                }
            }

            return false;
        }

        private bool Check(Labyrinth labyrinth, int botStartPositionX, int botStartPositionY, Direction direction, char visitedSymbol)
        {
            int nextPositionY = botStartPositionY;
            int nextPositionX = botStartPositionX;

            switch(direction)
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

            if (labyrinth[nextPositionY, nextPositionX] == labyrinth.BlankSymbol)
            {
                labyrinth[nextPositionY, nextPositionX] = visitedSymbol;
                
                if (CheckIfAnyExit(labyrinth, nextPositionX, nextPositionY))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
