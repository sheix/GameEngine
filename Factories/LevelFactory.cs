using System;
using System.Collections.Generic;
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
        }

        private List<List<ICell>> _grid;

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
        private IActor actor;
        private List<IItem> items;
        private int elevation;

        public IActor Actor
        {
            get { return actor; }
            set { actor = value; }
        }

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