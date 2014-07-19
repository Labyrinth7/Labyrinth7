namespace LabyrinthGameEngine
{
    using LabyrinthGameEngine.Interfaces;
    using System;

    /// <summary>
    /// Class representing the player
    /// </summary>
    internal class Player : IPlayer
    {
        private int moves = 0;
        private string name = string.Empty;

        public Player(int initialPositionX, int initialPositionY)
        {
            this.PositionX = initialPositionX;
            this.PositionY = initialPositionY;
            this.Position = new int[] { this.PositionX, this.PositionY };
        }

        public int Moves
        {
            get
            {
                return this.moves;
            }
            set
            {
                this.moves = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value.Length < 2 || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The name must be more than 2 letters long", "Name");
                }
                this.name = value;
            }
        }

        public int PositionX
        {
            get;
            set;
        }

        public int PositionY
        {
            get;
            set;
        }

        public int[] Position
        {
            get
            {
                return new int[] { this.PositionX, this.PositionY };
            }
            set
            {
                if (value is int[]) {
                    this.PositionX = value[0];
                    this.PositionY = value[1];
                }
                else
                {
                    throw new ArgumentException("The value type for the player position is not correct.");
                }
            }
        }
    }
}