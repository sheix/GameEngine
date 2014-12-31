using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.Contracts;
using Engine;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace Game
{
	public class Game : IGame
    {
        private IScene _scene;
        private ICalendar _calendar;

        private ISceneFactory _sceneFactory;
	    public event EventHandler KeyPressed;
        public event EventHandler SendMessage;

		public Game(ICalendar calendar)
		{
			_calendar = calendar;
		}

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
            Console.WriteLine("Game.Start");
            string currentSceneName = "None";
            Task.Factory.StartNew(() => _calendar.Play());

            while (true)
            {
                if (_calendar.SetMission != null)
                {
                    _scene = _sceneFactory.GetScene(_calendar.SetMission, currentSceneName);
                    currentSceneName = _calendar.SetMission;
                    _scene.MessageSent += InvokeSendMessage;
                    _calendar.AttachScene(_scene);
                    while (true)
                    {
                        var outcome = _scene.Play();
                        _scene.MessageSent -= InvokeSendMessage;
                        if (outcome == "Calendar")
                        {
                            _calendar.DetachScene(_scene);
                            currentSceneName = "None";
                            break;
                        }
                        _scene = _sceneFactory.GetScene(outcome, currentSceneName);
                        _scene.MessageSent += InvokeSendMessage;
                        
                    }

                }

            }
            //var random = _actorFactory.GetActor();
            //_scene.AddActor(player);
            //_scene.AddActor(random);
            
            //(_scene as IStage).PlaceActorToGrid(player);
            //(_scene as IStage).PlaceActorToGrid(random);
            //var result = Task.Factory.StartNew(() => _scene.Play());
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
