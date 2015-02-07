using System.Collections.Generic;
using System.Linq;
using Engine;
using Game;
using Game.Cells;
using SFML.Graphics;
using SFML.Window;
using Engine.Contracts;
using Font = SFML.Graphics.Font;
using System;

namespace Infrastructure
{
	public class Renderer : IRenderable
    {
        private readonly Font font;
        private Dictionary<string, Color> _moonColors = new Dictionary<string, Color> {{"Mun",Color.Cyan}, {"Lun",Color.Yellow}, {"Sput", Color.Green}};
        private const uint CharacterSize = 20;
		private RenderWindow window;
		private Random _random;

		private Dictionary<String, byte[,]> sceneColors = new Dictionary<string, byte[,]>();

		public Renderer(RenderWindow _window)
        {
			_random = new Random ();
			window = _window;
            font = new Font(FileSystemHelper.PathToResources + "Fonts" + FileSystemHelper.FileSystemSeparator + "kongtext.ttf");
        }

        public void RenderMessage(string message)
        {
            var text = new Text(message, font,CharacterSize) { Position = new Vector2f(0, 0) };
            text.Draw(window, RenderStates.Default);
        }

        public void RenderCalendar(ICalendar calendar)
        {
            RenderMoons(window, calendar);
            RenderMissions(window, calendar);
        }

        private void RenderMissions(RenderWindow window, ICalendar calendar)
        {
//            char order = '0';
//            var missions = calendar.GetAvailableMissions();
//            Vector2f position = new Vector2f(0, window.Size.Y - font.GetGlyph(100, CharacterSize, false).Bounds.Height * missions.Count - window.Size.Y/2);
//            
//            foreach (var mission in missions)
//            {
//                Text text = new Text(order + " :"+ mission, font,CharacterSize)
//                               {
//                                   Position = position
//                               };
//                text.Draw(window, RenderStates.Default);
//                position.Y += text.GetLocalBounds().Height;
//                order++;
//            }
        }

        private void RenderMoons(RenderWindow window, ICalendar calendar)
        {
            Vector2f position = new Vector2f(0, window.Size.Y - font.GetGlyph(100, CharacterSize, false).Bounds.Height- 100);
            foreach (Moon moon in calendar.Moons)
            {
                char c = GetGlyphForMoonState(moon);
                var text = new Text(moon.Name + " "+ c, font,CharacterSize)
                               {
                                   Color = _moonColors[moon.Name],
                                   Position = position
                               };
                text.Draw(window, RenderStates.Default);
                position.X += text.GetGlobalBounds().Width;
            }
        }

        private char GetGlyphForMoonState(Moon moon)
        {
            if (moon.Position == moon.Period) return '0';
            if (moon.Position == moon.Period/2) return 'O';
            if (moon.Position < moon.Period/2) return '(';
            if (moon.Position > moon.Period/2) return ')';
            return 'E';
        }

		byte[,] GetColors (GameScene gameScene)
		{
			var colors = generateColors(gameScene); 
			sceneColors.Add(gameScene.Name, colors); 
			return colors; 
		}

		byte[,] generateColors (GameScene gameScene)
		{

			var dimensions = gameScene.GetMapDimensions ();
			var result = new byte[dimensions._x,dimensions._y];
			for(int x = 0; x< dimensions._x;x++)
				for(int y = 0; y< dimensions._y;y++)
					result[x,y] = (byte)(_random.Next (100) + 155);
		return result;
		}

        public void RenderScene(IScene scene)
        {
			var gameScene = scene as GameScene; 

			var sceneColoring = sceneColors.ContainsKey (scene.Name) ? sceneColors [scene.Name] : GetColors (gameScene);

            uint X = window.Size.X;
            uint Y = window.Size.Y;

            var centerOfInterest = gameScene.GetCenterOfInterest();
			if (centerOfInterest == Vector.None)
				return;

			var maxMapSize = gameScene.GetMapDimensions();
			var normalCenterOfInterest = Normalize (centerOfInterest,maxMapSize);

            int leftX, rightX, upY, downY;

			leftX = normalCenterOfInterest._x - 5;
			rightX = normalCenterOfInterest._x + 6;
			upY = normalCenterOfInterest._y - 5;
			downY = normalCenterOfInterest._y + 6;

            int xNumber = rightX - leftX + 1;
            int yNumber = downY - upY + 1;

            int xOffset = leftX;
            int yOffset = upY;

            for (int x = leftX; x < rightX; x++)
            {
                for (int y = upY; y < downY; y++)
                {
                    ICell cell;
                    if (InRange(x, y, maxMapSize._x ,maxMapSize._y))
                        cell = gameScene.At(x, y);
                    else continue;

                    Vector v = GetScreenPosition(x, y, xNumber, yNumber, X, Y, xOffset, yOffset);
                    if (cell.Actor != null)
                        if (cell.Actor.Name == "Player")
                        {
                            var text = new Text("@", font, CharacterSize) { Position = new Vector2f(v._x, v._y) , Color = new Color(255,153,0)};
                            text.Draw(window, RenderStates.Default);
                            continue;
                        }
                    if (cell is Wall)
                    {
					byte grayLevel = sceneColoring [x,y];
                        var text = new Text("#", font,CharacterSize) { Position = new Vector2f(v._x, v._y), Color = new Color(grayLevel, grayLevel, grayLevel)};
                        text.Draw(window, RenderStates.Default);
                        continue;
                    }
                    if (cell.Specials != null)
                    {
                        if (cell.Specials.Any(m => m is EndPoint))
                        {
                            var text = new Text(">", font, CharacterSize) { Position = new Vector2f(v._x, v._y) };
                            text.Draw(window, RenderStates.Default);
                            continue;
                        }
                    }
                    if (cell.Actor == null)
                    {
                        var text = new Text(".", font,CharacterSize) { Position = new Vector2f(v._x, v._y) };
                        text.Draw(window, RenderStates.Default);
                        continue;
                    }
                    


                }
            }
        }

		Vector Normalize (Vector centerOfInterest, Vector resoluton)
		{
			int x=centerOfInterest._x, y=centerOfInterest._y;
			if (centerOfInterest._x < 5)
				x = 5;
			if (centerOfInterest._x > resoluton._x - 6)
				x = resoluton._x - 6;

			if (centerOfInterest._y < 5)
				y = 5;
			if (centerOfInterest._y > resoluton._y - 6)
				y = resoluton._y - 6;

			return new Vector (x, y);
		}

        private bool InRange(int x, int y, int X, int Y)
        {
            if (x > X-1) return false;
            if (y > Y-1) return false;
            if (x < 0) return false;
            if (y < 0) return false;
            return true;
        }

        private static Vector GetScreenPosition(int x, int y, int xNumber, int yNumber, uint X, uint Y, int xOffset, int yOffset)
        {
            // x       - position in the map
            // xNumber - count of items in the window
            // X       - screen resolution
            // xOffset - offset in window
            double marginY = Y * 0.2;
            double marginX = X * 0.4;

            double startX = marginX / 2;
            double startY = marginY / 2;

            double xn = (X - marginX) / xNumber;
            double yn = (Y - marginY) / yNumber;

            double actualX = startX + xn * (x - xOffset);
            double actualY = startY + yn * (y - yOffset);
            
			return new Vector((int)actualX, (int)actualY);
        }
    }
}
