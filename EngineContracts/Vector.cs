namespace Contracts
{
    public class Vector
    {
        public readonly int _x;
        public readonly int _y;

        public Vector(int x, int y)
        {
            _x = x;
            _y = y;
        }

		public static Vector operator+(Vector v1, Vector v2)
		{
			return new Vector(v1._x+v2._x,v1._y+v2._y);
		}
    }

}
