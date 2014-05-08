using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;

namespace Engine
{
    public class GridGenerator
    {
        public Grid Generate(GridRule[] rules)
        {
            return new Grid();
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
                grid.Set(x, gridSize._y, new Wall());

            for (y = 0; y < gridSize._y; y++)
                grid.Set(0, y, new Wall());
            for (y = 0; y < gridSize._y; y++)
                grid.Set(gridSize._x, y, new Wall());

        }

        #endregion
    }

    public class Wall : ICell
    {
        #region Implementation of ICell

        public IActor Actor
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public List<IItem> Items
        {
            get { throw new NotImplementedException(); }
        }

        public int Elevation
        {
            get { throw new NotImplementedException(); }
        }

        public void AddItem(IItem item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
