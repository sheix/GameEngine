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
        private ICalendar _calendar;
        public event EventHandler KeyPressed;
        public event EventHandler SendMessage;

        public void InvokeSendMessage(object sender, EventArgs eventArgs)
        {
            if (SendMessage != null) SendMessage(this, eventArgs);
        }

        private void InvokeKeyPressed(KeyPressedEventArgs e)
        {
            if (KeyPressed != null) KeyPressed(this, e);
        }

        public void Start()
        {
            _calendar = new Calendar(this);
            


            _scene = (new SceneGenerator()).GenerateScene("Default");
            _strategy = new ManualStrategy(this);
            var actorFactory = new ActorFactory(_strategy);
            var player = actorFactory.GetPlayer();
            var random = actorFactory.GetActor();
            _scene.AddActor(player);
            _scene.AddActor(random);
            _scene.MessageSent += InvokeSendMessage;
            (_scene as IStage).PlaceActorToGrid(player);
            (_scene as IStage).PlaceActorToGrid(random);
            Task.Factory.StartNew(() => _scene.Play());
        }

        
        public IScene Scene
        {
            get { return _scene; }
        }

        public ICalendar Calendar
        {
            get { return _calendar; }
        }

        public void _KeyPressed(string key)
        {
            InvokeKeyPressed(new KeyPressedEventArgs { Key = key});
        }
    }
}
