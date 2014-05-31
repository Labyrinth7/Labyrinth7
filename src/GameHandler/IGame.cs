namespace GameHandler
{
    public interface IGame
    {
        GameState GameState { get; }

        void Initialize();
        void Update();
        void Draw();
    }
}
