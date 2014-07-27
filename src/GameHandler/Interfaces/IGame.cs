namespace GameHandler.Interfaces
{
    using GameHandler.DrawEngine;

    public interface IGame
    {
        GameState GameState { get; }

        void Initialize();
        void Update();
        void Restart();
        void TopResults();
        void SuccessfulEscape();
        DrawableDataBuffer GetBuffer();
    }
}
