namespace LabyrinthGameUnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LabyrinthGameEngine;

    [TestClass]
    public class TestLabyrinth
    {
        [TestMethod]
        public void TestDisplayMethod()
        {
            string expectedResult = "- x -" + Environment.NewLine + "x * x" + Environment.NewLine + "- x -";

            int labyrinthRows = 3;
            int labyrinthCols = 3;

            Labyrinth labyrinth = new Labyrinth(labyrinthRows, labyrinthCols);
            labyrinth[0, 0] = '-';
            labyrinth[0, 1] = 'x';
            labyrinth[0, 2] = '-';
            labyrinth[1, 0] = 'x';
            labyrinth[1, 1] = '-';
            labyrinth[1, 2] = 'x';
            labyrinth[2, 0] = '-';
            labyrinth[2, 1] = 'x';
            labyrinth[2, 2] = '-';

            int[] playerPosition = new int[] { 1, 1 };
            string actualResult = labyrinth.AddPlayerToLabyrinth(playerPosition);

            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
