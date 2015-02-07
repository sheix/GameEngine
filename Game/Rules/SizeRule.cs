using Engine;

namespace Game.Rules
{
    public class SizeRule : MapRule
    {
        private int _x;
        private int _y;

        public override void Process(Grid grid)
        {
			var dimensions = GetValue("Size").Split('X');
			_x = int.Parse(dimensions[0]);
			_y = int.Parse(dimensions[1]);
            grid.SetMaxSize(_x, _y);
        }

        
    }
}