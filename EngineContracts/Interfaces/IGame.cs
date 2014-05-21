namespace Contracts
{
    public interface IGame
    {
        void Start();
        IScene Scene { get; }
        void KeyPressed(string key);
    }
}