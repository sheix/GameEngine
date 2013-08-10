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
			((PlayerActor)Self).selection = Name;
		}

		public override bool CanDo (IScene scene)
		{
			return true;
		}
	}
}

