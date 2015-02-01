using System;
using System.Collections.Generic;
using Engine.Contracts;
using Engine;
using Game.Cells;

namespace Game.Rules
{
    public class RoomWallRule : MapRule
    {
        private int MaxXSize = 8;
        private int MaxYSize = 8;
        private int MinXSize = 3;
        private int MinYSize = 3;
        
        public override void Process(Grid grid)
        {
            var size = grid.GetSize();
            int MaxRoomCount = size._x/MaxXSize*size._y/MaxYSize;
            int roomCount;
            if (IsDefined("ROOMCOUNT"))
                roomCount = int.Parse(GetValue("ROOMCOUNT"));
            else
                roomCount = Random.Next(MaxRoomCount) + 1;

            var rooms = new List<Room>();

            for (int i = 0; i < roomCount; i++)
            {
                int RoomSizeX = Random.Next(MaxXSize - MinXSize) +MinXSize;
                int RoomSizeY = Random.Next(MaxYSize - MinYSize) +MinYSize;
                int x = Random.Next(size._x - RoomSizeX) + 1;
                int y = Random.Next(size._y - RoomSizeY) + 1;

                var currentRoom = new Room(x, y, RoomSizeX, RoomSizeY);

                foreach (var room in rooms)
                {
                    if (room.Intersects(currentRoom))
                        currentRoom.Reduce();
                }
                
                //Console.WriteLine("Room no {0}", i + 1);
                //Console.WriteLine("({0},{1})[{2},{3}]",x,y,RoomSizeX,RoomSizeY);
                for (int k = x; k < x + RoomSizeX;k++)
                    for (int j = y; j < y + RoomSizeY; j++)
                    {

                        if ((k == x || k == x + RoomSizeX - 1) || (j == y || j == y + RoomSizeY - 1))
                        {
                  //          Console.WriteLine("Wall at: ({0},{1})",k,j);
                            grid.Set(new Wall(new Vector(k, j)));
                        }
                        
                    }


            }
        }
    }

    public class Room
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int RoomSizeX { get; private set; }
        public int RoomSizeY { get; private set; }

        public Room(int x, int y, int roomSizeX, int roomSizeY)
        {
            X = x;
            Y = y;
            RoomSizeX = roomSizeX;
            RoomSizeY = roomSizeY;
        }

        public bool Intersects(Room otherRoom)
        {
            bool intersectX = GetIntersect(X,RoomSizeX,otherRoom.X,otherRoom.RoomSizeX);
            bool intersectY = GetIntersect(Y,RoomSizeY,otherRoom.Y,otherRoom.RoomSizeY);
            return intersectX && intersectY;
        }

        private bool GetIntersect(int thisVar, int thisSize, int otherVar, int otherSize)
        {
            bool intersect = false;
            if (thisVar > otherVar) 
            {
                if (otherVar + otherSize > thisVar)
                {
                    intersect = true;
                }
            }
            else
            {
                if (thisVar + thisSize > otherVar)
                {
                    intersect = true;
                }
            }
            return intersect;
        }

        public void Reduce()
        {
            X++;
            Y++;
            RoomSizeX--;
            RoomSizeY--;
        }
    }
}