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

        public override ActResult Do(IScene scene)
        {
            (scene as IStage).Move(Self as IPlacableActor, Name);
            // How much it really takes should be calculated here
            return new ActResult {TimePassed = 1, Message = string.Format("{0} is moved",Self.Name)};
        }

        public override bool CanDo(IActor actor, IScene scene)
        {
            return (scene as IStage).IsFreeInDirection(Self as IPlacableActor, Name);
        }

        #endregion
    }
}