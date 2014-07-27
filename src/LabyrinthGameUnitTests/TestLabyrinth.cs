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

            char[,] testMatrix = new char[labyrinthRows, labyrinthCols];
            testMatrix[0, 0] = '-';
            testMatrix[0, 1] = 'x';
            testMatrix[0, 2] = '-';
            testMatrix[1, 0] = 'x';
            testMatrix[1, 1] = '-';
            testMatrix[1, 2] = 'x';
            testMatrix[2, 0] = '-';
            testMatrix[2, 1] = 'x';
            testMatrix[2, 2] = '-';

            Labyrinth labyrinth = new Labyrinth(testMatrix);
            
            int[] playerPosition = new int[] { 1, 1 };

            LabyrinthWithPlayer labyrinthWithPlayer = new LabyrinthWithPlayer(labyrinth, playerPosition);
            string actualResult = (String) labyrinthWithPlayer.GetDrawableData();

            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
