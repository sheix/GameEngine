using Engine.Contracts;
using Engine;
using Game.Cells;

namespace Game.Rules
{
    public class BorderWallsRule : MapRule
    {
        public override void Process(Grid grid)
        {
			if (GetValue ("rule") == "None")
				return;
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
    }
}