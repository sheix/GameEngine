namespace Engine
{
	public class Act : IAct
	{
		protected IActor Self;
		protected IActor Target;
		protected IItem First;
		protected IItem Second;
		protected string Name;

		public Act (string name, IActor self, IActor target, IItem first = null, IItem second = null)
		{
			Name = name;
			Self = self;
			Target = target;
			First = first;
			Second = second;
		}

		public virtual void Do(IScene scene)
		{
            //Must override
		}

		public virtual bool CanDo(IScene scene)
		{
            //Must override
			return false;
		}

	}
}

