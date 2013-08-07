using System;

namespace Engine
{
	public class Act : IAct
	{
		protected IActor _self;
		protected IActor _target;
		protected IItem _first;
		protected IItem _second;
		protected string _name;

		public Act (string name, IActor self, IActor target, IItem first = null, IItem second = null)
		{
			_name = name;
			_self = self;
			_target = target;
			_first = first;
			_second = second;
		}

		public virtual void Do(IScene scene)
		{

		}

		public virtual bool CanDo(IScene scene)
		{
			return false;
		}

	}
}

