using System;
using Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
	public class Grid : IGrid
	{
		List<List<ICell>> IGrid.Grid {
			get { return _grid; }
			set { throw new NotImplementedException (); }
		}

		public bool Contains (IActor actor)
		{
			return _grid.Any (m => m.Any (n => n.Actor == actor));
		}

		public Vector GetActorCoordinates (IActor actor)
		{
			int x, y;
		    try
		    {
                x = _grid.FindIndex(m => m.Any(c => c.Actor == actor));
                y = _grid[x].FindIndex(m => m.Actor == actor);
		    }
		    catch (Exception)
		    {
		        return Vector.None;
		    }
			
				             
			return new Vector (x, y);
		}

	    public Vector GetActorCoordinates(string name)
	    {
            int x, y;
            try
            {
                x = _grid.FindIndex(m => m.Any(c => c.Actor != null && c.Actor.Name == name));
                y = _grid[x].FindIndex(m => m.Actor != null && m.Actor.Name == name);
            }
            catch (Exception)
            {
                return Vector.None;
            }


            return new Vector(x, y);
	    }

	    private List<List<ICell>> _grid;

	    public void SetMaxSize(int x,int y)
        {
            _grid = new List<List<ICell>>();
            for (int i = 0; i < x; i++)
            {
                _grid.Add(new List<ICell>());
                for (int j = 0; j < y; j++)
                {
                    _grid[i].Add(new Cell());
                }
            }
        }

		public ICell At (int x, int y)
		{
			return _grid [x] [y];
		}

		public ICell At (Vector v)
		{
			return _grid [v._x] [v._y];
		}


	    public Vector GetSize()
	    {
	        return new Vector(_grid.Count,_grid[0].Count);
	    }

	    public void Set(int x, int y, ICell cell)
	    {
	        _grid[x][y] = cell;
	    }
	}

	public class Cell : ICell
	{
		private List<IItem> items;
		private int elevation = 0;

		public Cell ()
		{
			items = new List<IItem> ();
		}

		public IActor Actor { get; set; }

		public List<IItem> Items {
			get { return items; }
			set { throw new NotImplementedException (); }
		}

		public int Elevation {
			get { return elevation; }
		}

		public void AddItem (IItem item)
		{
			Items.Add (item);
		}
	}
}

