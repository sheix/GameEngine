using Engine;

namespace Game.Rules
{
    public class SizeRule : MapRule
    {
        private readonly int _x;
        private readonly int _y;

        public SizeRule(string size)
        {
            var dimensions = size.Split('x');
            _x = int.Parse(dimensions[0]);
            _y = int.Parse(dimensions[1]);
        }

        public SizeRule(int x, int y)
        {
            _x = x;
            _y = y;
        }

        #region Overrides of MapRule

        public override void Process(Grid grid)
        {
            grid.SetMaxSize(_x, _y);
        }

        #endregion
    }
}