using System;

namespace Engine.Contracts
{
    public interface ISceneFactory
    {
        IScene GetScene(String ID, String previousSceneID);
    }
}