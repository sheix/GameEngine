using Contracts;
using Engine;

namespace Game.Cells
{
    public class Wall : BaseCell
    {
        public Wall(Vector coordinates) : base(coordinates)
        {
        }

        override public void AddItem(IItem item)
        {
            // can't add item
        }

        override public bool IsPassable()
        {
            return false;
        }
    }
}