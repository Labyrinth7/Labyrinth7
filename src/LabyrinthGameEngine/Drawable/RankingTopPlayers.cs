namespace LabyrinthGameEngine
{
    using GameHandler.Interfaces;

    using LabyrinthGameEngine.Interfaces;

    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class representing the Player ranking system
    /// </summary>
    internal sealed class RankingTopPlayers : IDrawable
    {
        private const int NUMBER_OF_TOP_PLAYERS = 5;

        private List<IPlayer> topPlayers = new List<IPlayer>();

        private static RankingTopPlayers instance = null;

        private RankingTopPlayers()
        {
        }

        /// <summary>
        /// Returns the instance of the RankingTopPlayer object.
        /// </summary>
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

        /// <summary>
        /// Adds a given player to the top players table.
        /// </summary>
        /// <param name="currentPlayer">Given player.</param>
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

        /// <summary>
        /// Returns the top rated players.
        /// </summary>
        /// <returns>Score table with the top players as string.</returns>
        public object GetDrawableData()
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
            }

            return topResults.ToString();
        }

        /// <summary>
        /// Adds a given player to the score table.
        /// </summary>
        /// <param name="currentPlayer">Given player.</param>
        private void AddPlayer(IPlayer currentPlayer)
        {
            Console.Write("Please enter your nickname: ");
            string name = Console.ReadLine().Trim();
            currentPlayer.Name = name;

            this.topPlayers.Add(currentPlayer);
        }
    }
}
