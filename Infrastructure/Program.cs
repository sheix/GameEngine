#region Using Statements
using System;
using Castle.Windsor;
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
			Console.WriteLine ("Launch host app!");

            var window = new Window(VideoMode.DesktopMode, "Test");

            while (window.IsOpen())
            {
                
                //window.

                window.Display();
            }

			container.Dispose ();
        }

		public static IWindsorContainer container;

		static Program()
		{
			container = new WindsorContainer();
			//container.Register();
		}
    }
}
