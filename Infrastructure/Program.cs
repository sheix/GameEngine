#region Using Statements
using System;
using Castle.Windsor;
using SFML.Graphics;
using SFML.Window;

#endregion

namespace Infrastructure
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			Console.WriteLine ("Launch host app!!");

            var window = new RenderWindow(VideoMode.DesktopMode, "Test");
            window.Closed += OnClosed;
            window.KeyPressed += OnKeyPressed;
			char s = System.IO.Path.DirectorySeparatorChar;
			Font font = new Font(@".."+s+".."+s+".."+s+"Resources"+s+"Fonts"+s+"kongtext.ttf");
			Text text = new Text("Test", font);
			text.Position = new Vector2f (200f, 200f);
            while (window.IsOpen())
            {
                window.DispatchEvents();
				window.Clear ();
                text.Draw(window,RenderStates.Default);
				text.Rotation += 0.1f;

                window.Display();
            }

			container.Dispose ();
        }

        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
                window.Close();

        }

        private static void OnClosed(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Close();

        }

        public static IWindsorContainer container;

		static Program()
		{
			container = new WindsorContainer();
			//container.Register();
		}
    }
}
