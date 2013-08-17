namespace Engine.Interfaces
{
    public interface IPlacableActor : IActor
    {
        int InitialX { get; set; }
        int InitialY { get; set; }
    }
}