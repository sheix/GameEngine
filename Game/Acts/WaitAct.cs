using Contracts;
using Engine;

namespace Game.Acts
{
    public class WaitAct : BaseAct
    {
        public WaitAct(IActor actor) : base("Wait",actor,null)
        {
            
        }

        public override ActResult Do(IScene scene)
        {
            return new ActResult {Message = "Wait", TimePassed = 10};
        }

        public override bool CanDo(IActor actor, IScene scene)
        {
            return true;
        }
    }
}