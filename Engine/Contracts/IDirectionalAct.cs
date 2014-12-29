namespace Engine.Contracts
{
    public interface IDirectionalAct : IAct
    {
        Vector Direction { get; }
    }
}
