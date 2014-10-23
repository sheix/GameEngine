using System;
using Contracts;
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
            var x = grid.GetSize()._x;
            var y = grid.GetSize()._y;

            Split(0, 0, x, y);

        }

        private void Split(int startX, int startY, int endX, int endY)
        {
            Console.WriteLine("Got room ({0}:{1})({2}:{3})",startX,startY,endX,endY);
            int minRoomSize = IsDefined("MinRoomSize") ? int.Parse(GetValue("MinRoomSize")) : 3;
            if ((endX - startX < minRoomSize) || (endY - startY < minRoomSize))
            {
                Console.WriteLine("End recursion");
                return;
            }

            var splitByX = _rnd.Next(2)==0;

            Console.WriteLine(splitByX ? "Split By X" : "Split By Y");

            int split, door;
            if (splitByX)
            {
                if (endX - startX - minRoomSize * 2 < 0) return;
                split = _rnd.Next(endX - startX-minRoomSize*2) + startX+1 + minRoomSize ;
                door = _rnd.Next(endY - startY -2 )  + startY + 1 ;
                Console.WriteLine("Split = {0}, Door = {1}",split,door);
                for (int i = startY; i < endY; i++)
                {
                    if (i != door)
                        _grid.Set(new Wall(new Vector(split, i)));
                    else 
                        _grid.Set(new Cell(new Vector(split,i)));
                }
                Split(startX,startY,split-1,endY);
                Split(split+1,startY,endX,endY);
            }
            else
            {
                if (endY - startY - minRoomSize * 2 < 0) return;
                split = _rnd.Next(endY - startY -minRoomSize*2)+startY +minRoomSize+  1;
                door = _rnd.Next(endX - startX -2 )+ startX + 1;
                Console.WriteLine("Split = {0}, Door = {1}", split, door);

                for (int i = startX; i < endX; i++)
                {
                    if (i != door)
                        _grid.Set(new Wall(new Vector(i, split)));
                    else
                        _grid.Set(new Cell(new Vector(i,split)));
                }
                Split(startX,startY,endX,split-1);
                Split(startX, split+1,endX,endY);

            }


        }
    }
}