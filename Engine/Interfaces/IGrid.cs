using System.Collections.Generic;

namespace Engine.Interfaces
{
    public interface IGrid
    {
        List<List<ICell>> Grid { get; set; }
        bool Contains(IActor actor);
        void GetActorCoordinates(IActor actor);
    }
}
