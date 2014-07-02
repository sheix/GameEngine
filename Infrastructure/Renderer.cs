using System;
using System.Collections.Generic;
using Engine;
using Game;
using SFML.Graphics;
using SFML.Window;
using Contracts;
using Font = SFML.Graphics.Font;

namespace Infrastructure
{
    class Renderer
    {
        private readonly Font font;
        private Dictionary<string, Color> _moonColors = new Dictionary<string, Color> {{"Mun",Color.Cyan}, {"Lun",Color.Yellow}, {"Sput", Color.Green}};
        private const uint CharacterSize = 24;

        public Renderer()
        {
            char s = System.IO.Path.DirectorySeparatorChar;
            font = new Font(@".." + s + ".." + s + ".." + s + "Resources" + s + "Fonts" + s + "kongtext.ttf");

        }

        public void RenderMessage(RenderWindow window, string message)
        {
            var text = new Text(message, font,CharacterSize) { Position = new Vector2f(0, 0) };
            text.Draw(window, RenderStates.Default);
        }

        public void RenderCalendar(RenderWindow window, ICalendar calendar)
        {
            RenderMoons(window, calendar);
            RenderMissions(window, calendar);
        }

        private void RenderMissions(RenderWindow window, ICalendar calendar)
        {
            char order = '0';
            var missions = calendar.GetAvailableMissions();
            Vector2f position = new Vector2f(0, window.Size.Y - font.GetGlyph(100, CharacterSize, false).Bounds.Height * missions.Count - window.Size.Y/2);
            
            foreach (var mission in missions)
            {
                Text text = new Text(order + " :"+ mission, font,CharacterSize)
                               {
                                   Position = position
                               };
                text.Draw(window, RenderStates.Default);
                position.Y += text.GetLocalBounds().Height;
                order++;
            }
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

        public void RenderScene(RenderWindow window, IScene scene)
        {
            uint X = window.Size.X;
            uint Y = window.Size.Y;

            var map = (scene as IStage).Map;

            var playerCoords = (scene as IStage).GetCenterOfInterest();
            var maxResolution = (scene as IStage).GetMapDimensions();
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

                    ICell cell;
                    if (InRange(x, y, maxResolution._x ,maxResolution._y))
                        cell = map.At(x, y);
                    else continue;

                    Vector v = GetScreenPosition(x, y, xNumber, yNumber, X, Y, xOffset, yOffset);
                    if (cell.Actor != null)
                        if (cell.Actor.Name == "Player")
                        {
                            var text = new Text("@", font, CharacterSize) { Position = new Vector2f(v._x, v._y) };
                            text.Draw(window, RenderStates.Default);
                            continue;
                        }
                    if (cell is Wall)
                    {
                        var text = new Text("#", font,CharacterSize) { Position = new Vector2f(v._x, v._y) };
                        text.Draw(window, RenderStates.Default);
                        continue;
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
            double MarginY = Y * 0.2;
            double MarginX = X * 0.4;

            double startX = MarginX / 2;
            double startY = MarginY / 2;

            double xn = (X - MarginX) / xNumber;
            double yn = (Y - MarginY) / yNumber;

            double actual_x = startX + xn * (x - xOffset);
            double actual_y = startY + yn * (y - yOffset);
            return new Vector((int)actual_x, (int)actual_y);
        }
    }
}
