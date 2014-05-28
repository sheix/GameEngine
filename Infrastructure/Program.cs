#region Using Statements
using System;
using System.Drawing;
using System.Threading.Tasks;
using Castle.Windsor;
using Engine;
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
			
            
            var window = new RenderWindow(VideoMode.DesktopMode, "Test");
            window.Closed += OnClosed;
            window.KeyPressed += OnKeyPressed;
            Renderer renderer = new Renderer();
            while (window.IsOpen())
            {
                window.DispatchEvents();
				window.Clear ();

                renderer.RenderScene(window, _game.Scene,window.Size.X,window.Size.Y);

                window.Display();
            }

			_container.Dispose ();
        }


        


        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = (Window)sender;
            Console.WriteLine((e.Code).ToString());
            if (e.Code == Keyboard.Key.Escape)
                window.Close();

            _game._KeyPressed((e.Code).ToString());
            

        }

        private static void OnClosed(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Close();

        }

        public static IWindsorContainer _container;

        static Program()
		{
			_container = new WindsorContainer();
			//_container.Register();
		}
    }
}
