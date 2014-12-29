using System;

namespace Engine.Contracts
{
    public class Vector
    {
        
        public int _x;
        public int _y;
        public static Vector None;

        public Vector(int x, int y)
        {
            _x = x;
            _y = y;
        }

		public static Vector operator+(Vector v1, Vector v2)
		{
			return new Vector(v1._x+v2._x,v1._y+v2._y);
		}

        public override string ToString()
        {
            return string.Format("({0},{1})", _x,_y);
        }

        public static Vector Parse(string c)
        {
            var vertices = c.Split(',');
            return new Vector(int.Parse(vertices[0]),int.Parse(vertices[1]));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector)) return false;
            var o = obj as Vector;
            return _x == o._x && _y == o._y;
        }

        
    }

}
