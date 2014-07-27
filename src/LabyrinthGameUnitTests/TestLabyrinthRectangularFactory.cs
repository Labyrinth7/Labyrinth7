namespace LabyrinthGameUnitTests
{
    using LabyrinthGameEngine;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestLabyrinthRectangularFactory
    {
        [TestMethod]
        public void TestLabyrinthNoExit1()
        {
            int labyrinthRows = 3;
            int labyrinthCols = 3;

            int positionX = labyrinthRows / 2;
            int positionY = labyrinthCols / 2;

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

            LabyrinthFactory factory = new LabyrinthRectangularFactory();
            PrivateObject factoryPrivate = new PrivateObject(factory);
            var returnedvalue = factoryPrivate.Invoke("CheckIfAnyExit", testMatrix, positionX, positionY);

            Assert.AreEqual(false, returnedvalue);
        }

        [TestMethod]
        public void TestLabyrinthNoExit2()
        {
            int labyrinthRows = 7;
            int labyrinthCols = 7;

            int positionX = labyrinthRows / 2;
            int positionY = labyrinthCols / 2;

            char[,] testMatrix = new char[labyrinthRows, labyrinthCols];

            testMatrix[0, 0] = '-';
            testMatrix[0, 1] = 'x';
            testMatrix[0, 2] = '-';
            testMatrix[0, 3] = '-';
            testMatrix[0, 4] = '-';
            testMatrix[0, 5] = '-';
            testMatrix[0, 6] = 'x';

            testMatrix[1, 0] = 'x';
            testMatrix[1, 1] = 'x';
            testMatrix[1, 2] = '-';
            testMatrix[1, 3] = '-';
            testMatrix[1, 4] = '-';
            testMatrix[1, 5] = 'x';
            testMatrix[1, 6] = 'x';

            testMatrix[2, 0] = '-';
            testMatrix[2, 1] = '-';
            testMatrix[2, 2] = 'x';
            testMatrix[2, 3] = 'x';
            testMatrix[2, 4] = 'x';
            testMatrix[2, 5] = 'x';
            testMatrix[2, 6] = 'x';

            testMatrix[3, 0] = '-';
            testMatrix[3, 1] = 'x';
            testMatrix[3, 2] = '-';
            testMatrix[3, 3] = '-';
            testMatrix[3, 4] = '-';
            testMatrix[3, 5] = '-';
            testMatrix[3, 6] = 'x';

            testMatrix[4, 0] = 'x';
            testMatrix[4, 1] = 'x';
            testMatrix[4, 2] = 'x';
            testMatrix[4, 3] = '-';
            testMatrix[4, 4] = 'x';
            testMatrix[4, 5] = 'x';
            testMatrix[4, 6] = '-';

            testMatrix[5, 0] = '-';
            testMatrix[5, 1] = 'x';
            testMatrix[5, 2] = 'x';
            testMatrix[5, 3] = '-';
            testMatrix[5, 4] = 'x';
            testMatrix[5, 5] = 'x';
            testMatrix[5, 6] = '-';

            testMatrix[6, 0] = 'x';
            testMatrix[6, 1] = 'x';
            testMatrix[6, 2] = 'x';
            testMatrix[6, 3] = 'x';
            testMatrix[6, 4] = '-';
            testMatrix[6, 5] = 'x';
            testMatrix[6, 6] = 'x';

            LabyrinthFactory factory = new LabyrinthRectangularFactory();
            PrivateObject factoryPrivate = new PrivateObject(factory);
            var returnedvalue = factoryPrivate.Invoke("CheckIfAnyExit", testMatrix, positionX, positionY);

            Assert.AreEqual(false, returnedvalue);
        }

        [TestMethod]
        public void TestGenerateLabyrinthMethod()
        {
            LabyrinthFactory factory = new LabyrinthRectangularFactory();
            var labyrinth = factory.CreateLabyrinth(6, 6);
            Assert.IsInstanceOfType(labyrinth, typeof(Labyrinth));
        }
    }
}
