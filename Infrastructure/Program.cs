using System;
using System.Drawing;
using System.Threading.Tasks;
using Castle.Windsor;
using Engine;
using SFML.Graphics;
using SFML.Window;
using Engine.Contracts;
using Font = SFML.Graphics.Font;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Game;



namespace Infrastructure
{
    /// <summary>
    /// The main class - create game object, use game start/load/save etc.
    /// </summary>
    public static class Program
    {
        private static IGame _game;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			_container = new WindsorContainer ();
			_container.Install (FromAssembly.This ());
			_game = _container.Resolve<IGame>();
			_game.SendMessage += RenderMessage;
			_game.Start ();
            //Task.Factory.StartNew(() => _game.Start());
            //_window = new RenderWindow(VideoMode.DesktopMode, "Test");
            //_window.Closed += OnClosed;
            //_window.KeyPressed += OnKeyPressed;
			//_renderer = new Renderer(_window);
//            while (_window.IsOpen())
//            {
//
//                _window.DispatchEvents();
//				_window.Clear ();
//
//                _renderer.RenderCalendar(_game.Calendar);
//                if (_game.Scene != null) _renderer.RenderScene(_game.Scene);
//                _renderer.RenderMessage(_message);
//
//                _window.Display();
//            }
//
			_container.Dispose ();
			Console.ReadKey ();
        }

        private static void RenderMessage(object sender, EventArgs e)
        {
            _message += ((MessageEventArgs)e).Message += "\n";
        }


        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = (Window)sender;
            Console.WriteLine((e.Code).ToString());
            if (e.Code == Keyboard.Key.Escape)
                window.Close();

            _game._KeyPressed((e.Code).ToString());
            _message = "";
        }

        private static void OnClosed(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Close();

        }

        public static IWindsorContainer _container;
        private static Renderer _renderer;
        private static RenderWindow _window;
        private static string _message = "";
		}

	public class Installer: IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register (Component.For<IGame> ().ImplementedBy (typeof(Game.Game)).LifeStyle.Singleton);
			container.Register (Component.For<IStrategy> ().ImplementedBy (typeof(Game.ManualStrategy)).LifeStyle.Singleton);
			container.Register (Component.For<ICalendar> ().ImplementedBy(typeof(Game.Calendar)).LifeStyle.Singleton);
			container.Register (Component.For<ISceneFactory> ().ImplementedBy (typeof(SceneFactory)).LifeStyle.Singleton);
		}
	}
}
