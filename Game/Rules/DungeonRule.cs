using System;
using Engine.Contracts;
using Engine;
using Game.Cells;

namespace Game.Rules
{
    internal class DungeonRule : MapRule
    {
        readonly Random _rnd = new Random();
        private Grid _grid;

        public override void Process(Grid grid)
        {
            _grid = grid;
            var x = grid.GetSize()._x-1;
            var y = grid.GetSize()._y-1;

            Split(0, 0, x, y);

        }

        private void Split(int startX, int startY, int endX, int endY)
		{
			int deltaX, deltaY;
			deltaX = endX - startX;
			deltaY = endY - startY;
			Console.WriteLine ("Splitting room ({0},{1})-({2},{3})", startX, startY,endX,endY);
			int minRoomSize = IsDefined ("MinRoomSize") ? int.Parse (GetValue ("MinRoomSize")) : 3;

			if ((deltaX < minRoomSize *2+1) || (deltaY < minRoomSize*2+1)) {
				return;
			}

			var splitByX = _rnd.Next(2)==0;

			int split, door;

			if (splitByX)
            {

				split = _rnd.Next(deltaX-minRoomSize*2-1) + startX+1 + minRoomSize ;
                door = _rnd.Next(deltaY-2 )  + startY + 1 ;
				for (int i = startY + 1; i < endY; i++)
                {
					if (i != door) {
						Console.WriteLine ("{0},{1} - Wall", split,i);
						_grid.Set (new Wall (new Vector (split, i)));
					} else {
						Console.WriteLine ("{0},{1} - Door",split,i);
						_grid.Set (new Cell (new Vector (split, i)));
					}
                }
                Split(startX,startY,split,endY);
                Split(split,startY,endX,endY);
            }
            else
            {
			  split = _rnd.Next(deltaY -minRoomSize*2-1)+startY +minRoomSize+  1;
                door = _rnd.Next(deltaX -2 )+ startX + 1;

                for (int i = startX + 1; i < endX; i++)
                {
					if (i != door) {
						Console.WriteLine ("{0},{1} - Wall", i, split);
						_grid.Set (new Wall (new Vector (i, split)));
					} else {
						Console.WriteLine ("{0},{1} - Door", i, split);
						_grid.Set (new Cell (new Vector (i, split)));
					}
                }
                Split(startX,startY,endX,split);
                Split(startX, split,endX,endY);

            }

        }
    }
}