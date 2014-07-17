using System;
using Contracts;

namespace EngineContracts.Interfaces
{
    public interface ISceneFactory
    {
        IScene GetScene(String ID);
    }
}