using System;
using Engine;
namespace SimpleGame
{
	public class Scissors : Act
	{
		public Scissors (String name, IActor self) : base(name,self,null)
		{
		}
		#region IAct implementation
		public override bool CanDo (IScene scene)
		{
			return true;
		}

		public override void Do (IScene scene)
		{
			((PlayerActor)_self).selection = _name;
		}
		#endregion

	}
}

