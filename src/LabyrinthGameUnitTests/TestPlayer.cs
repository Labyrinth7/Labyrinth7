namespace LabyrinthGameUnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LabyrinthGameEngine;

    [TestClass]
    public class TestPlayer
    {
        private Player testPlayer;

        [TestInitialize]
        public void InitializeInstances()
        {
            testPlayer = new Player(1, 2);
        }
        [TestMethod]
        public void TestInitialiazingInstance()
        {
            Player testPlayer = new Player(1, 3);
        }

        [TestMethod]
        public void TestNameGetter()
        {
            testPlayer.Name = "George";
        }

        [TestMethod]
        public void TestNameSetterStraightPath()
        {
            testPlayer.Name = "Maria";
            string expectedName = "Maria";
            Assert.AreEqual(expectedName, testPlayer.Name);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void TestNameSetterWrongName()
        {
            testPlayer.Name = "";
        }

        [TestMethod]
        public void TestMovesSetter()
        {
            testPlayer.Moves = 3;
            int expectedResult = 3;
            Assert.AreEqual(expectedResult, testPlayer.Moves);
        }

        [TestMethod]
        public void TestMovesGetter()
        {
            testPlayer.Moves = 5;
            int expectedResult = testPlayer.Moves;
            Assert.AreEqual(expectedResult, testPlayer.Moves);
        }

        [TestMethod]
        public void TestPositionGetter()
        {
            testPlayer.Position = new int[] { 1, 3 };
            int[] expectedPosition = testPlayer.Position;
            CollectionAssert.AreEqual(expectedPosition, testPlayer.Position);
        }
    }
}
