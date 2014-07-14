namespace LabyrinthGameEngine
{
    using LabyrinthGameEngine.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    internal sealed class RankingTopPlayers
    {
        private const int numberOfTopPlayers = 5;

        private static RankingTopPlayers instance = null;

        private List<IPlayer> topPlayers = new List<IPlayer>();

        private RankingTopPlayers()
        {
        }

        internal static RankingTopPlayers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RankingTopPlayers();
                }
                return instance;
            }
        }

        internal void AddToTopResults(IPlayer currentPlayer)
        {
            int currentNumberOfPlayersInTop = this.topPlayers.Count;

            if (currentNumberOfPlayersInTop >= 0 && currentNumberOfPlayersInTop < numberOfTopPlayers)
            {
                AddPlayer(currentPlayer);
            }
            else if (currentNumberOfPlayersInTop == numberOfTopPlayers)
            {
                this.topPlayers.Sort(delegate(IPlayer player1, IPlayer player2)
                {
                    return player1.Moves.CompareTo(player2.Moves);
                });

                IPlayer lastPlayerInRanking = this.topPlayers[this.topPlayers.Count - 1];

                if (lastPlayerInRanking.Moves > currentPlayer.Moves)
                {
                    this.topPlayers.RemoveAt(currentNumberOfPlayersInTop - 1);
                    AddPlayer(currentPlayer);
                }
            }
        }

        internal string GetTopResults()
        {
            StringBuilder topResults = new StringBuilder();

            topResults.Append("\n");

            if (this.topPlayers.Count == 0)
            {
                topResults.Append("The scoreboard is empty! ");
            }
            else
            {
                int i = 1;
                this.topPlayers.Sort(delegate(IPlayer s1, IPlayer s2)
                {
                    return s1.Moves.CompareTo(s2.Moves);
                });

                topResults.Append("Top 5: \n");

                this.topPlayers.ForEach(delegate(IPlayer player)
                {
                    topResults.AppendLine(String.Format(i + ". {1} ---> {0} moves", player.Moves, player.Name));
                    i++;
                });

                topResults.Append("\n");
            }

            return topResults.ToString();
        }

        private void AddPlayer(IPlayer currentPlayer)
        {
            Console.WriteLine("Please enter your nickname");
            string name = Console.ReadLine().Trim();
            currentPlayer.Name = name;

            this.topPlayers.Add(currentPlayer);
        }
    }
}
