﻿namespace LabyrinthGameEngine
{
    using System;
    using System.Linq;

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

            bool isTrue = false;

            // Border cases STARTS
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
            // Border cases STOPS

            int nextPositionY;
            int nextPositionX;

            // up
            nextPositionY = botStartPositionY - 1;
            nextPositionX = botStartPositionX;

            if (isTrue == false &&
                labyrinth[nextPositionY, nextPositionX] != visitedSymbol &&
                labyrinth[nextPositionY, nextPositionX] == blankSymbol)
            {
                labyrinth[nextPositionY, nextPositionX] = visitedSymbol;
                isTrue = CheckIfAnyExit(labyrinth, nextPositionY, nextPositionX);

                if (isTrue)
                {
                    return true;
                }
            }

            // right
            nextPositionY = botStartPositionY;
            nextPositionX = botStartPositionX + 1;

            if (isTrue == false &&
                labyrinth[botStartPositionY, nextPositionX] != visitedSymbol &&
                labyrinth[botStartPositionY, nextPositionX] == blankSymbol)
            {
                labyrinth[botStartPositionY, nextPositionX] = visitedSymbol;
                isTrue = CheckIfAnyExit(labyrinth, nextPositionY, nextPositionX);

                if (isTrue)
                {
                    return true;
                }
            }

            // down
            nextPositionY = botStartPositionY + 1;
            nextPositionX = botStartPositionX;

            if (isTrue == false &&
                labyrinth[nextPositionY, nextPositionX] != visitedSymbol &&
                labyrinth[nextPositionY, nextPositionX] == blankSymbol)
            {
                labyrinth[nextPositionY, nextPositionX] = visitedSymbol;
                isTrue = CheckIfAnyExit(labyrinth, nextPositionY, nextPositionX);

                if (isTrue)
                {
                    return true;
                }
            }

            // left
            nextPositionY = botStartPositionY;
            nextPositionX = botStartPositionX - 1;

            if (isTrue == false &&
                labyrinth[nextPositionY, nextPositionX] != visitedSymbol &&
                labyrinth[nextPositionY, nextPositionX] == blankSymbol)
            {
                labyrinth[nextPositionY, nextPositionX] = visitedSymbol;
                isTrue = CheckIfAnyExit(labyrinth, nextPositionY, nextPositionX);

                if (isTrue)
                {
                    return true;
                }
            }

            return false;
        }
    }
}