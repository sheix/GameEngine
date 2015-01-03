using System.Collections.Generic;
using Engine.Contracts;

namespace Engine
{
    public abstract class BaseCell : ICell
    {
        private readonly List<IItem> _items;
        private readonly List<ICellSpecial> _specials;
        private int elevation = 0;

        protected BaseCell(Vector coordinates)
        {
            _items = new List<IItem>();
            _specials = new List<ICellSpecial>();
            Coordinates = coordinates;
        }

        public IActor Actor { get; set; }

        public List<ICellSpecial> Specials { get { return _specials; } }
        public List<IItem> Items { get { return _items; } }

        public int Elevation
        {
            get { return elevation; }
        }

        public Vector Coordinates
        {
            get;
            private set;
        }

        public virtual void AddItem(IItem item)
        {
            Items.Add(item);
        }

        public void AddSpecial(ICellSpecial special)
        {
            _specials.Add(special);
        }

        public virtual bool IsPassable()
        {
            return true;
        }
    }
}