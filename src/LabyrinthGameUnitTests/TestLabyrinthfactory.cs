namespace LabyrinthGameUnitTests
{
    using LabyrinthGameEngine;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestLabyrinthFactory
    {
        [TestMethod]
        public void TestLabyrinthNoExit1()
        {
            int labyrinthRows = 3;
            int labyrinthCols = 3;

            int positionX = labyrinthRows / 2;
            int positionY = labyrinthCols / 2;

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

            LabyrinthFactory factory = new LabyrinthFactory();
            PrivateObject obj = new PrivateObject(factory);
            var returnedvalue = obj.Invoke("CheckIfAnyExit", labyrinth, positionX, positionY);

            Assert.AreEqual(false, returnedvalue);
        }

        [TestMethod]
        public void TestLabyrinthNoExit2()
        {
            int labyrinthRows = 7;
            int labyrinthCols = 7;

            int positionX = labyrinthRows / 2;
            int positionY = labyrinthCols / 2;

            Labyrinth labyrinth = new Labyrinth(labyrinthRows, labyrinthCols);
            labyrinth[0, 0] = '-';
            labyrinth[0, 1] = 'x';
            labyrinth[0, 2] = '-';
            labyrinth[0, 3] = '-';
            labyrinth[0, 4] = '-';
            labyrinth[0, 5] = '-';
            labyrinth[0, 6] = 'x';

            labyrinth[1, 0] = 'x';
            labyrinth[1, 1] = 'x';
            labyrinth[1, 2] = '-';
            labyrinth[1, 3] = '-';
            labyrinth[1, 4] = '-';
            labyrinth[1, 5] = 'x';
            labyrinth[1, 6] = 'x';

            labyrinth[2, 0] = '-';
            labyrinth[2, 1] = '-';
            labyrinth[2, 2] = 'x';
            labyrinth[2, 3] = 'x';
            labyrinth[2, 4] = 'x';
            labyrinth[2, 5] = 'x';
            labyrinth[2, 6] = 'x';

            labyrinth[3, 0] = '-';
            labyrinth[3, 1] = 'x';
            labyrinth[3, 2] = '-';
            labyrinth[3, 3] = '-';
            labyrinth[3, 4] = '-';
            labyrinth[3, 5] = '-';
            labyrinth[3, 6] = 'x';

            labyrinth[4, 0] = 'x';
            labyrinth[4, 1] = 'x';
            labyrinth[4, 2] = 'x';
            labyrinth[4, 3] = '-';
            labyrinth[4, 4] = 'x';
            labyrinth[4, 5] = 'x';
            labyrinth[4, 6] = '-';

            labyrinth[5, 0] = '-';
            labyrinth[5, 1] = 'x';
            labyrinth[5, 2] = 'x';
            labyrinth[5, 3] = '-';
            labyrinth[5, 4] = 'x';
            labyrinth[5, 5] = 'x';
            labyrinth[5, 6] = '-';

            labyrinth[6, 0] = 'x';
            labyrinth[6, 1] = 'x';
            labyrinth[6, 2] = 'x';
            labyrinth[6, 3] = 'x';
            labyrinth[6, 4] = '-';
            labyrinth[6, 5] = 'x';
            labyrinth[6, 6] = 'x';

            LabyrinthFactory factory = new LabyrinthFactory();
            PrivateObject obj = new PrivateObject(factory);
            var returnedvalue = obj.Invoke("CheckIfAnyExit", labyrinth, positionX, positionY);

            Assert.AreEqual(false, returnedvalue);
        }
    }
}
