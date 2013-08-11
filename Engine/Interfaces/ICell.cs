using System.Collections.Generic;

namespace Engine.Interfaces
{
    public interface ICell
    {
        IActor Actor { get; set;}
        List<IItem> Items {get; }
        int Elevation { get; }
        void AddItem(IItem item);
        // add special here
    }
}
