using System.Collections.Generic;

namespace Engine.Interfaces
{
    public interface IGrid : IRenderable
    {
        List<List<ICell>> Grid { get; set; }
        bool Contains(IActor actor);
        Vector GetActorCoordinates(IActor actor);
        ICell At(int x, int y);
        ICell At(Vector v);
    }
}
