namespace Contracts
{
	public interface IAct
	{
		bool CanDo(IScene scene);
		void Do(IScene scene);

	    IActor Self { get; set; }
	    string Name { get; set; }
	}
}

