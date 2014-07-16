namespace LabyrinthGameEngine
{
    using System.Collections;

    internal interface ILabyrinth : IEnumerable
    {
        IEnumerator GetEnumerator();

        char this[int row, int col]
        {
            get;
            set;
        }

        string AddPlayerToLabyrinth(int[] playerPosition);

        string ToString();
    }
}