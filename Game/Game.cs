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
        private IScene _scene;
        private ManualStrategy _strategy;

        public void Start()
        {
            _scene = (new SceneGenerator()).GenerateScene("Default");
            _strategy = new ManualStrategy();
            var player = new PlacableActor("Player", _strategy);
            _scene.AddActor(player);
            (_scene as IStage).PlaceActorToGrid(player);
            Task.Factory.StartNew(() => _scene.Play());
        }

        public IScene Scene
        {
            get { return _scene; }
        }

        public void KeyPressed(string key)
        {
            _strategy.LastPressedKey(key);
        }
    }

    
}
