using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using SFML.Graphics;
using SFML.Window;
using Contracts;
using Font = SFML.Graphics.Font;

namespace Infrastructure
{
    class Renderer
    {
        private Font font;
        public Renderer()
        {
            char s = System.IO.Path.DirectorySeparatorChar;
            font = new Font(@".." + s + ".." + s + ".." + s + "Resources" + s + "Fonts" + s + "kongtext.ttf");

        }

        public void RenderScene(RenderWindow window, IScene scene, uint X, uint Y)
        {
            var map = (scene as IStage).Map;

            var playerCoords = (scene as IStage).GetCenterOfInterest();
            var maxResolution = (scene as IStage).GetMaxResolution();
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
                            var text = new Text("@", font) { Position = new Vector2f(v._x, v._y) };
                            text.Draw(window, RenderStates.Default);
                            continue;
                        }
                    if (cell is Wall)
                    {
                        var text = new Text("#", font) { Position = new Vector2f(v._x, v._y) };
                        text.Draw(window, RenderStates.Default);
                        continue;
                    }
                    if (cell.Actor == null)
                    {
                        var text = new Text(".", font) { Position = new Vector2f(v._x, v._y) };
                        text.Draw(window, RenderStates.Default);
                        continue;
                    }


                }
            }
        }

        private bool InRange(int x, int y, int X, int Y)
        {
            if (x > X) return false;
            if (y > Y) return false;
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
