using System.Collections.Generic;

namespace Engine.Interfaces
{
    public interface IGrid
    {
        List<List<ICell>> Grid { get; }
    }
}
