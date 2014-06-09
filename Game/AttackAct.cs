using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using Engine;

namespace Game
{
    public class AttackAct : BaseAct
    {
        public AttackAct(string name, IActor self, IActor target, IItem first, IItem second) : base(name, self, target, first, second)
        {
            
        }

        public override ActResult Do(IScene scene)
        {
            (scene as IStage).Attack(Self as IPlacableActor, Name);
            
            return new ActResult { TimePassed = 1, Message = string.Format("{0} attacked {1}", Self.Name, Target.Name) };
        }

        public override bool CanDo(IActor actor, IScene scene)
        {
            Target = (scene as IStage).ActorInDirection(Self as IPlacableActor, Name);
            return Target != null;
        }
    }
}
