using Engine.Contracts;

namespace Engine
{
	public abstract class BaseAct : IAct
	{
		public string Name { get ; set ; }

		public IActor Self { get; set; }

		public abstract ActResult Do (IScene scene);

		public abstract bool CanDo (IActor actor, IScene scene);
	}
}

