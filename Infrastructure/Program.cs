#region Using Statements
using System;
using System.Drawing;
using Castle.Windsor;
using SFML.Graphics;
using SFML.Window;
using Contracts;
using Font = SFML.Graphics.Font;

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
			player = new Engine.PlacableActor("Player", new Engine.ManualStrategy());
			//scene.AddActor(player);
            (scene as IStage).PlaceActorToGrid((IPlacableActor) player);
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
				RenderScene(window, scene,window.Size.X,window.Size.Y);

                window.Display();
            }

			container.Dispose ();
        }


        private static void RenderScene(RenderWindow window, IScene scene, uint X, uint Y)
		{
            char s = System.IO.Path.DirectorySeparatorChar;
            Font font = new Font(@".."+s+".."+s+".."+s+"Resources"+s+"Fonts"+s+"kongtext.ttf");
			var map = (scene as IStage).Map;

			var playerCoords = map.GetActorCoordinates(player);

            if (playerCoords == Vector.None)
                return;

		    int leftX, rightX, upY, downY;

		    leftX = playerCoords._x - 5;
		    rightX = playerCoords._x + 5;
		    upY = playerCoords._y - 5;
		    downY = playerCoords._y + 5;

            int xNumber = rightX - leftX + 1;
            int yNumber = downY - upY + 1;

            int xOffset = leftX;
            int yOffset = upY;

		    for (int x = leftX; x < rightX; x++)
		    {
		        for (int y = upY; y < downY; y++)
		        {

		            var cell = map.At(x,y);
                    Vector v = GetScreenPosition(x, y,xNumber, yNumber, X, Y, xOffset, yOffset);
		            Console.WriteLine("{0}:{1}",v._x,v._y);
                    if (cell.Actor != null)
                    if (cell.Actor.Name == "Player")
                    {
                        Text text = new Text("@", font);
                        text.Position = new Vector2f(v._x,v._y);
                        text.Draw(window, RenderStates.Default);
                    }


		        }
		    }

			foreach (var item in map.Grid) {

				foreach (var item1 in item) {
					if (item1.Actor != null) {
						
						continue;
					}
					if (item1.Items.Count > 0) {
						
						continue;
					}
					
				}
			}

		}

        private static Vector GetScreenPosition(int x, int y,int xNumber, int yNumber, uint X, uint Y,int xOffset, int yOffset)
        {
            // x       - position in the map
            // xNumber - count of items in the window
            // X       - screen resolution
            // xOffset - offset in window
            double MarginY = Y*0.2;
            double MarginX = X*0.4;

            double startX = MarginX/2;
            double startY = 0;

            double xn = (X - MarginX)/xNumber;
            double yn = (Y - MarginY)/yNumber;

            double actual_x = startX + xn*(x - xOffset);
            double actual_y = startY + yn*(y - xOffset);
            return new Vector((int)actual_x,(int)actual_y);
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
