using Contracts;

namespace EngineContracts.Interfaces
{
    public interface ISceneFactory
    {
        IScene Generate(ISceneTemplate template);
        void UpdateScene(IScene scene, ISceneTemplate template);
    }
}