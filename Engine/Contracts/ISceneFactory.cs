using System;
using Engine.Contracts;

namespace EngineContracts.Interfaces
{
    public interface ISceneFactory
    {
        IScene GetScene(String ID, String previousSceneID);
    }
}