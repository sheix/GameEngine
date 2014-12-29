namespace Engine.Contracts
{
	public interface IAct
	{
	    /// <summary>
	    /// Makes an act (or try) and returns how much time it takes
	    /// </summary>
	    /// <param name="scene">Scene on which act plays</param>
	    /// <returns>Time passed</returns>
	    ActResult Do(IScene scene);
        string Name { get; set; }
	    bool CanDo(IActor actor, IScene scene);
	}

    public class ActResult
    {
        public int TimePassed;
        public string Message;
    }
}

