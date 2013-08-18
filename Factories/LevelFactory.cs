using System;
using System.Collections.Generic;
using System.Linq;
using CursesTest.Data;
using CursesTest.Factories;
using Engine;
using Engine.Interfaces;

namespace OfficeRatTest
{
    public class LevelFactory : ILevelFactory
    {
        public IGrid GenerateGrid()
        {
            const int maxx = 25;
            const int maxy = 25;
            var grid = new Grid(maxx, maxy);
            return grid;
        }
    }

    public class Grid : IGrid
    {
        List<List<ICell>> IGrid.Grid
        {
            get { return _grid; }
            set { throw new NotImplementedException(); }
        }

        public bool Contains(IActor actor)
        {
            return _grid.Any(m => m.Any(n => n.Actor == actor));
        }

        public void GetActorCoordinates(IActor actor)
        {
            int x, y;
            x = _grid.FindIndex(m => m.Any(c => c.Actor == actor));
            var list = _grid.Find(m => m.Any(c => c.Actor == actor));
            y = list.FindIndex(m => m.Equals(actor));
            var vector = new Vector(x,y);
        }

        private readonly List<List<ICell>> _grid;

        public Grid(int maxx, int maxy)
        {
            _grid = new List<List<ICell>>();
            for (int i = 0; i < maxx; i++)
            {
                _grid.Add(new List<ICell>());
                for (int j = 0; j < maxy; j++)
                {
                    _grid[i].Add(new Cell());
                }
            }
        }
    }

    public class Cell : ICell
    {
        private List<IItem> items;
        private int elevation;

        public IActor Actor { get; set; }

        public List<IItem> Items
        {
            get { return items; }
            set { throw new NotImplementedException(); }
        }

        public int Elevation
        {
            get { return elevation; }
        }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }
    }
}