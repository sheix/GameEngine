using System.Collections.Generic;

namespace Engine.Interfaces
{
    public interface ICell
    {
        IActor Actor { get; set;}
        List<IItem> Items {get;set;}
        int Elevation { get; }
        // add special here
    }
}
