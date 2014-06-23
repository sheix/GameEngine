#region Using Statements
using System;
using System.Drawing;
using System.Threading.Tasks;
using Castle.Windsor;
using Engine;
using EngineContracts;
using SFML.Graphics;
using SFML.Window;
using Contracts;
using Font = SFML.Graphics.Font;

#endregion

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
			Console.WriteLine ("Launch host app!!");

			_game = new Game.Game();
			_game.Start();
            _game.SendMessage += RenderMessage;
            
            _window = new RenderWindow(VideoMode.DesktopMode, "Test");
            _window.Closed += OnClosed;
            _window.KeyPressed += OnKeyPressed;
            _renderer = new Renderer();
            while (_window.IsOpen())
            {
                _window.DispatchEvents();
				_window.Clear ();

                _renderer.RenderCalendar(_window, _game.Calendar);
                _renderer.RenderScene(_window, _game.Scene);
                _renderer.RenderMessage(_window, _message);

                _window.Display();
            }

			_container.Dispose ();
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

        static Program()
		{
			_container = new WindsorContainer();
			//_container.Register();
		}
    }
}
