using Contracts;
using Engine;

namespace Game
{
    public class MoveAct : BaseAct
    {
        public MoveAct(string name, IActor self, IActor target, IItem first, IItem second) : base(name, self, target, first, second)
        {
            
        }

        #region Overrides of BaseAct

        public override int Do(IScene scene)
        {
            (scene as IStage).Move(Self as IPlacableActor, Name);
            return 1;
        }

        public override bool CanDo(IActor actor, IScene scene)
        {
            return (scene as IStage).IsFreeInDirection(Self as IPlacableActor, Name);
        }

        #endregion
    }
}