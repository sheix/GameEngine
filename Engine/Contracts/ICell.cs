﻿using System.Collections.Generic;

namespace Engine.Contracts
{
    public interface ICell
    {
        IActor Actor { get; set;}
        List<IItem> Items {get; }
        int Elevation { get; }
        Vector Coordinates { get; }
        List<ICellSpecial> Specials { get; }
        void AddItem(IItem item);
        void AddSpecial(ICellSpecial special);
        bool IsPassable();
    }
}
