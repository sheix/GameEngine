using Engine.Contracts;
using Engine;

namespace Game.Acts
{
	public class AttackAct : BaseAct
	{
		private IActor Target;
		public AttackAct (string name, IActor actor)
		{
			Name = name;
			Self = actor;
		}

        
		public override ActResult Do (IScene scene)
		{
			(scene as IStage).Attack (Self as IPlacableActor, Name);
            
			return new ActResult {
				TimePassed = 10,
				Message = string.Format ("{0} attacked {1}", Self.Name, Target.Name)
			};
		}

		public override bool CanDo (IActor actor, IScene scene)
		{
			Target = (scene as IStage).ActorInDirection (Self as IPlacableActor, Name);
			return Target != null;
		}
	}
}
