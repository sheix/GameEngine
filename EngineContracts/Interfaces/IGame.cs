using System;

namespace Contracts
{
    public interface IGame
    {
        void Start();
        IScene Scene { get; }
        void _KeyPressed(string key);
        event EventHandler KeyPressed;
        event EventHandler SendMessage;
    }
}