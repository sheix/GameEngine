using System;
using System.Collections.Generic;
using Contracts;
using EngineContracts.Interfaces;

namespace Engine
{
    public class GridGenerator
    {
        public Grid Generate(MapRule[] rules)
        {
            var grid = new Grid();
            foreach (var rule in rules)
            {
				System.Console.WriteLine (rule + " Processing");
                rule.Process(grid);
            }
            return grid;
        }

    }

    public abstract class MapRule : IRule 
    {
        public abstract void Process(Grid grid);
    }

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

    public class BorderWalls : MapRule
    {
        #region Overrides of MapRule

        public override void Process(Grid grid)
        {
            Vector gridSize = grid.GetSize();
            int x, y;

            for (x = 0; x < gridSize._x; x++)
            {
                grid.Set(new Wall(new Vector(x,0)));
            }
            for (x = 0; x < gridSize._x; x++)
				grid.Set(new Wall(new Vector(x,gridSize._y-1)));

            for (y = 0; y < gridSize._y; y++)
                grid.Set(new Wall(new Vector(0, y)));
            for (y = 0; y < gridSize._y; y++)
                grid.Set(new Wall(new Vector(gridSize._x - 1, y)));

        }

        #endregion
    }

    public abstract class BaseCell : ICell
    {
        private readonly List<IItem> _items;
        private readonly List<ICellSpecial> _specials;
        private int elevation = 0;

        protected BaseCell(Vector coordinates)
        {
            _items = new List<IItem>();
            _specials = new List<ICellSpecial>();
            Coordinates = coordinates;
        }

        public IActor Actor { get; set; }

        public List<ICellSpecial> Specials { get { return _specials; } }
        public List<IItem> Items { get { return _items; } }

        public int Elevation
        {
            get { return elevation; }
        }

        public Vector Coordinates
        {
            get;
            private set;
        }

        public virtual void AddItem(IItem item)
        {
            Items.Add(item);
        }

        public void AddSpecial(ICellSpecial special)
        {
            _specials.Add(special);
        }
    }

    public class Wall : BaseCell
    {
        public Wall(Vector coordinates) : base(coordinates)
        {
        }

        override public void AddItem(IItem item)
        {
            // can't add item
        }

    }
}
