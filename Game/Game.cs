using System;
using System.Threading.Tasks;
using Contracts;
using Engine;

namespace Game
{
    /// <summary>
    /// Game should in general handle the time, 
    /// Create scenes, pass scene to UI renderer
    /// </summary>
	public class Game : IGame
    {
        public IScene Scene;
        //public K

        public void Start()
        {
            Scene = (new SceneGenerator()).GenerateScene("Default");
            var strategy = new ManualStrategy();
            var player = new PlacableActor("Player", strategy);
            Scene.AddActor(player);
            (Scene as IStage).PlaceActorToGrid(player);
            Task.Factory.StartNew(() => Scene.Play());
        }
    }

    
}
