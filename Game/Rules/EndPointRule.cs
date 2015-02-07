using Engine.Contracts;
using Engine;

namespace Game.Rules
{
    public class EndPointRule : MapRule
    {
        public override void Process(Grid grid)
        {
			var parameters = GetAllParameterNames ().ToArray ();
			foreach (var item in parameters) {
				var startingPointItem = new EndPoint(item);
				var startingPointVector = Vector.Parse(GetValue(item));
				grid.At(startingPointVector).AddSpecial(startingPointItem);	
			}
        }
    }
}