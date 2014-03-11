using System.Collections.Generic;

namespace Contracts
{
    public interface ICell
    {
        IActor Actor { get; set;}
        List<IItem> Items {get; }
        int Elevation { get; }
        void AddItem(IItem item);
    }
}
