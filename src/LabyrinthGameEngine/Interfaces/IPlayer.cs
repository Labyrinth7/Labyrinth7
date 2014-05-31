namespace LabyrinthGameEngine.Interfaces
{
    public interface IPlayer
    {
        int Moves { get; set; }
        string Name { get; set; }
        int PositionY { get; set; }
        int PositionX { get; set; }
        int[] Position { get; }
    }
}
