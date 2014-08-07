using Contracts;
using Engine;
using EngineContracts.Interfaces;

namespace Game.Rules
{
    public class StartPointRule : MapRule
    {
        private ICellSpecial startingPointItem;
        private Vector startingPointVector;
        public StartPointRule(string s)
        {
            var startPoint = s.Split('-');
            startingPointItem = new StartPoint(startPoint[0]);
            startingPointVector = Vector.Parse(startPoint[1]);
        }

        public override void Process(Grid grid)
        {
            grid.At(startingPointVector).AddSpecial(startingPointItem);
        }
    }
}