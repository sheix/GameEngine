using System;

namespace Engine
{
    public class GridGenerator
    {
        public Grid Generate(MapRule[] rules)
        {
            var grid = new Grid();
            foreach (var rule in rules)
            {
				Console.WriteLine (rule + " Processing");
                rule.Process(grid);
            }
            return grid;
        }

    }
}
