using Engine;
using System;

namespace SimpleGame
{
	public class Rock : Act
	{
		public Rock (String name, IActor self) : base(name,self,null)
		{
		}

		public override bool CanDo (IScene scene)
		{
			return true;
		}

		public override void Do (IScene scene)
		{
			((PlayerActor)Self).selection = Name;
		}

	}
}

