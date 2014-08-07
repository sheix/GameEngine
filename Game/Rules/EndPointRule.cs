using Contracts;
using Engine;
using EngineContracts.Interfaces;

namespace Game.Rules
{
    public class EndPointRule : MapRule
    {
        private ICellSpecial EndPointItem;
        private Vector EndPointVector;

        public EndPointRule(string s)
        {
            var startPoint = s.Split('-');
            EndPointItem = new EndPoint(startPoint[0]);
            EndPointVector = Vector.Parse(startPoint[1]);

        }

        public override void Process(Grid grid)
        {
            grid.At(EndPointVector).AddSpecial(EndPointItem);
        }
    }
}