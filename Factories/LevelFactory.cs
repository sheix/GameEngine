using System;
using System.Collections.Generic;
using System.Linq;
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

    public class Grid : IGrid, IRenderable
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

        public Vector GetActorCoordinates(IActor actor)
        {
            int x, y;
            x = _grid.FindIndex(m => m.Any(c => c.Actor == actor));
			y = _grid[x].FindIndex(m => m.Actor == actor);
            
			return new Vector(x,y);
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

		public ICell At (int x, int y)
		{
			return _grid[x][y];
		}

		public void Render()
		{
			foreach (var item in _grid) {
				Console.WriteLine ();
				foreach (var item1 in item) {
					if (item1.Actor != null)
					{
						Console.Write('@');
						continue;
					}
					if (item1.Items.Count > 0) {
						Console.Write ('%');
						continue;
					}
					Console.Write ('.');
				}
			}
			Console.WriteLine ();
		}

    }

    public class Cell : ICell
    {
        private List<IItem> items;
        private int elevation = 0;

		public Cell ()
		{
			items = new List<IItem>();
		}

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