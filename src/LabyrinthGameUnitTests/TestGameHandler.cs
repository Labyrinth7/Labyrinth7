using System;
using GameHandler;
using LabyrinthGameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using  LabyrinthGameEngine;

namespace LabyrinthGameUnitTests
{
    [TestClass]
    public class TestGameHandler
    {
        [TestMethod]
        public void TestGameInstance()
        {
            Type gameType = new LabyrinthGame().GetType();
            UserInterface userInterface = UserInterface.Console;

            Game.Instance.Run(gameType, userInterface);
        }
    }
}
