using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IGrid
    {
        List<List<ICell>> Grid { get; set; }
        bool Contains(IActor actor);
        Vector GetActorCoordinates(IActor actor);
        Vector GetActorCoordinates(string name);
        ICell At(int x, int y);
        ICell At(Vector v);
        Dictionary<String, Vector> GetCells(Func<ICell, bool> func, Func<ICell, string> namer);
    }
}
