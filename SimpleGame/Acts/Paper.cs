using System;
using Engine;

namespace SimpleGame
{
	public class Paper : Act
	{
		public Paper (String name, IActor self) : base(name,self,null)
		{
		}
		public override void Do (IScene scene)
		{
			((PlayerActor)_self).selection = _name;
		}

		public override bool CanDo (IScene scene)
		{
			return true;
		}
	}
}

