namespace LabyrinthGameEngine
{
    using LabyrinthGameEngine.Interfaces;

    public class Player : IPlayer
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
                this.PositionX = value[0];
                this.PositionY = value[1];
            }
        }
    }
}