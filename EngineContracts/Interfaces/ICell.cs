using System.Collections.Generic;

namespace Contracts
{
    public interface ICell
    {
        IActor Actor { get; set;}
        List<IItem> Items {get; }
        int Elevation { get; }
        Vector Coordinates { get; }
        void AddItem(IItem item);
    }
}
