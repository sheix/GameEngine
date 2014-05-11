#region Using Statements
using System;
using Castle.Windsor;
using SFML.Graphics;
using SFML.Window;
using Contracts;
#endregion

namespace Infrastructure
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

		static IActor player;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			Console.WriteLine ("Launch host app!!");
			var scene = (new Engine.SceneGenerator()).GenerateScene("Default");
			player = new Engine.Actor("Player", new Engine.ManualStrategy());
			scene.AddActor(player);
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
				RenderScene(scene);

                window.Display();
            }

			container.Dispose ();
        }

		private static void RenderScene(IScene scene)
		{
			var map = (scene as IStage).Map;

			var playerCoords = map.GetActorCoordinates(player);



			foreach (var item in map.Grid) {

				foreach (var item1 in item) {
					if (item1.Actor != null) {
						Console.Write ('@');
						continue;
					}
					if (item1.Items.Count > 0) {
						Console.Write ('%');
						continue;
					}
					Console.Write ('.');
				}
			}

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
