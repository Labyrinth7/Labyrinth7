namespace LabyrinthGameUnitTests
{
    using LabyrinthGameEngine;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TestRankingTopPlayers
    {
        private RankingTopPlayers topRankingTable;

        [TestInitialize]
        public void InitializeTopRankingTable()
        {
            topRankingTable = RankingTopPlayers.Instance;
        }

        [TestMethod]
        public void TestRankingTopPlayerSingleton()
        {
            RankingTopPlayers secondRanking = RankingTopPlayers.Instance;

            Assert.AreSame(topRankingTable, secondRanking);
        }

        [TestMethod]
        public void TestRankingTopGetDrawableDataWithEmptyTable()
        {
            string expectedOutput = Environment.NewLine + "The scoreboard is empty!";
            string actualOutput = topRankingTable.GetDrawableData().ToString();

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestAddingAPlayerToTheTopRankingPlayersTable()
        {
            string expectedOutput = Environment.NewLine + "Top 5: " + Environment.NewLine + "1. Pesho ---> 10 moves" + Environment.NewLine;
            Player player1 = new Player(1, 2);
            player1.Name = "Pesho";
            player1.Moves = 10;
            topRankingTable.AddToTopResults(player1);

            string actualOutput = topRankingTable.GetDrawableData().ToString();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestRankingTopPlayersWithMoreThan5Players()
        {
            int numberOfTestedPlayers = 10;
            for (int i = 0; i < numberOfTestedPlayers; i++)
            {
                Player player = new Player(1, 0);
                player.Moves = numberOfTestedPlayers - i;
                player.Name = "Player" + i;

                topRankingTable.AddToTopResults(player);
            }

            string expectedOutput = Environment.NewLine + "Top 5: " + Environment.NewLine +
                "1. Player9 ---> 1 moves" + Environment.NewLine +
                "2. Player8 ---> 2 moves" + Environment.NewLine +
                "3. Player7 ---> 3 moves" + Environment.NewLine +
                "4. Player6 ---> 4 moves" + Environment.NewLine +
                "5. Player5 ---> 5 moves" + Environment.NewLine;

            string actualOutput = topRankingTable.GetDrawableData().ToString();

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
