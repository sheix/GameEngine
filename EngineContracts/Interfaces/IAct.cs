namespace Contracts
{
	public interface IAct
	{
		void Do(IScene scene);
	    string Name { get; set; }
	    bool CanDo(IActor actor, IScene scene);
	}
}

