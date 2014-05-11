using System.Collections.Generic;
using Contracts;

namespace Engine
{
    public class GridGenerator
    {
        public Grid Generate(GridRule[] rules)
        {
            var grid = new Grid();
            foreach (var rule in rules)
            {
				System.Console.WriteLine (rule.ToString() + " Processing");
                rule.Process(grid);
            }
            return grid;
        }

    }

    public abstract class GridRule
    {
        public abstract void Process(Grid grid);
    }

    public class SizeRule : GridRule
    {
        private readonly int _x;
        private readonly int _y;

        public SizeRule(int x, int y)
        {
            _x = x;
            _y = y;
        }

        #region Overrides of GridRule

        public override void Process(Grid grid)
        {
            grid.SetMaxSize(_x, _y);
        }

        #endregion
    }

    public class BorderWalls : GridRule
    {
        #region Overrides of GridRule

        public override void Process(Grid grid)
        {
            Vector gridSize = grid.GetSize();
            int x, y;

            for (x = 0; x < gridSize._x; x++)
                grid.Set(x, 0, new Wall());
            for (x = 0; x < gridSize._x; x++)
				grid.Set(x, gridSize._y-1, new Wall());

            for (y = 0; y < gridSize._y; y++)
                grid.Set(0, y, new Wall());
            for (y = 0; y < gridSize._y; y++)
				grid.Set(gridSize._x-1, y, new Wall());

        }

        #endregion
    }

    public class Wall : ICell
    {
        #region Implementation of ICell

        public IActor Actor
        {
            get { return null; }
            set { /*can't set actor here*/ }
        }

        public List<IItem> Items
        {
            get { return new List<IItem>(); }
        }

        public int Elevation
        {
            get { return 0; }
        }

        public void AddItem(IItem item)
        {
            // can't add item
        }

        #endregion
    }
}
