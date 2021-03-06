using Engine.Contracts;
using System;
using Engine;

namespace Game.Acts
{
	public class TakeAct : BaseAct
	{
		#region implemented abstract members of BaseAct
		public override ActResult Do (IScene scene)
		{
			throw new NotImplementedException ();
		}
		public override bool CanDo (IActor actor, IScene scene)
		{
			if (scene.HaveItemsBeneath (actor))
				return true;
			return false;
		}
		#endregion
	}
}

