using System.Collections.Generic;

namespace Contracts
{
    public interface IGrid
    {
        List<List<ICell>> Grid { get; set; }
        bool Contains(IActor actor);
        Vector GetActorCoordinates(IActor actor);
        ICell At(int x, int y);
        ICell At(Vector v);
    }
}
