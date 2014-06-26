using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Engine;

namespace Game
{
	public class Game : IGame
    {
        private IScene _scene;
        private ManualStrategy _strategy;
        private ICalendar _calendar;
        private ActorFactory _actorFactory;
	    private IScenarioLoader _scenarioLoader;
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
            _strategy = new ManualStrategy(this);
            _actorFactory = new ActorFactory(_strategy);
            
            while (true)
            {
                var missions = _calendar.GetAvailableMissions();

                //Scenario scenario  = _scenarioLoader.Load(missions);
                

                _calendar.NextDay();
            }

            _scene = (new SceneFactory()).GetScene("Default");
            
            var player = _actorFactory.GetPlayer();
            var random = _actorFactory.GetActor();
            _scene.AddActor(player);
            _scene.AddActor(random);
            _scene.MessageSent += InvokeSendMessage;
            (_scene as IStage).PlaceActorToGrid(player);
            (_scene as IStage).PlaceActorToGrid(random);
            var result = Task.Factory.StartNew(() => _scene.Play());
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
