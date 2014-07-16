namespace LabyrinthGameEngine
{
    using LabyrinthGameEngine.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal sealed class RankingTopPlayers
    {
        private const int NUMBER_OF_TOP_PLAYERS = 5;

        private List<IPlayer> topPlayers = new List<IPlayer>();

        private static RankingTopPlayers instance = null;
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

            if (currentNumberOfPlayersInTop >= 0 && currentNumberOfPlayersInTop < RankingTopPlayers.NUMBER_OF_TOP_PLAYERS)
            {
                AddPlayer(currentPlayer);
            }
            else if (currentNumberOfPlayersInTop == RankingTopPlayers.NUMBER_OF_TOP_PLAYERS)
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

            topResults.Append(Environment.NewLine);

            if (this.topPlayers.Count == 0)
            {
                topResults.Append("The scoreboard is empty! ");
            }
            else
            {
                String title = "Top " + RankingTopPlayers.NUMBER_OF_TOP_PLAYERS + ": " + Environment.NewLine;
                topResults.Append(title);

                this.topPlayers.Sort((player1, player2) => player1.Moves.CompareTo(player2.Moves));

                int positionNumber = 1;
                this.topPlayers.ForEach(player => {
                    String line = String.Format(positionNumber + ". {1} ---> {0} moves", player.Moves, player.Name);
                    topResults.AppendLine(line);
                    positionNumber++;
                });

                topResults.Append(Environment.NewLine);
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
