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
        public event EventHandler KeyPressed;

        private void InvokeKeyPressed(KeyPressedEventArgs e)
        {
            EventHandler handler = KeyPressed;
            if (handler != null) handler(this, e);
        }

        public void Start()
        {
            _scene = (new SceneGenerator()).GenerateScene("Default");
            _strategy = new ManualStrategy(this);
            var actorFactory = new ActorFactory(_strategy);
            var player = actorFactory.GetPlayer();
            _scene.AddActor(player);
            (_scene as IStage).PlaceActorToGrid(player);
            Task.Factory.StartNew(() => _scene.Play());
        }

        public IScene Scene
        {
            get { return _scene; }
        }

        public void _KeyPressed(string key)
        {
            InvokeKeyPressed(new KeyPressedEventArgs { Key = key});
        }
    }
}
