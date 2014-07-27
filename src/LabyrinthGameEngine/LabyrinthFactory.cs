namespace LabyrinthGameEngine
{
    using LabyrinthGameEngine.Interfaces;
    using System;

    public abstract class LabyrinthFactory
    {
        protected char[,] currentMatrix = null;

        protected int labyrinthRows = 0;
        protected int labyrinthCols = 0;

        protected int centerOfCols = 0;
        protected int centerOfRows = 0;

        public abstract ILabyrinth CreateLabyrinth(int labyrinthRows, int labyrinthCols);

        protected abstract char[,] GenerateMatrix();

        protected abstract bool CheckIfAnyExit(char[,] labyrinth, int positionX, int positionY);

        protected abstract bool CheckForExitInTheGivenDirections(char[,] labyrinth, int botStartPositionX, int botStartPositionY, Direction direction, char visitedSymbol);
    }
}
