using Engine.Contracts;
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
            var endPoint = s.Split('-');
            EndPointItem = new EndPoint(endPoint[0]);
            EndPointVector = Vector.Parse(endPoint[1]);

        }

        public override void Process(Grid grid)
        {
            grid.At(EndPointVector).AddSpecial(EndPointItem);
        }
    }
}