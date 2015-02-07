using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.Contracts;
using Engine;
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

		public Game (ICalendar calendar, ISceneFactory sceneFactory)
		{
			_calendar = calendar;
			_sceneFactory = sceneFactory;
		}

		public void InvokeSendMessage (object sender, EventArgs eventArgs)
		{
			if (SendMessage != null)
				SendMessage (this, eventArgs);
		}

		private void InvokeKeyPressed (KeyPressedEventArgs e)
		{
			if (KeyPressed != null)
				KeyPressed (this, e);
		}

		public void Start ()
		{
			Console.WriteLine ("Game.Start");
			string currentSceneName = "None";
			string newSceneName = "DEFAULT";
			//Task.Factory.StartNew (() => _calendar.Play ());

			while (true) {
				_scene = _sceneFactory.GetScene (newSceneName, currentSceneName);
				currentSceneName = newSceneName;
				_scene.MessageSent += InvokeSendMessage;
				_calendar.AttachScene (_scene);
				while (true) {
					var outcome = _scene.Play ();
					_scene.MessageSent -= InvokeSendMessage;
					if (outcome == "Calendar") {
						_calendar.DetachScene (_scene);
						currentSceneName = "None";
						break;
					}
					_scene = _sceneFactory.GetScene (outcome, currentSceneName);
					_scene.MessageSent += InvokeSendMessage;
				}
			}
		}

		public IScene Scene {
			get { return _scene; }
		}

		public ICalendar Calendar {
			get { return _calendar; }
		}

		public void _KeyPressed (string key)
		{
			InvokeKeyPressed (new KeyPressedEventArgs { Key = key });
		}
	}
}
