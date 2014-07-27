namespace LabyrinthGameEngine.Interfaces
{
    using System.Collections;

    public interface ILabyrinth : IEnumerable
    {
        new IEnumerator GetEnumerator();

        char this[int row, int col]
        {
            get;
            set;
        }

        int Cols { get; }
        int Rows { get; }

        string ToString();
    }
}