namespace Engine.Contracts
{
    public interface IPlacableActor : IActor
    {
        int InitialX { get; set; }
        int InitialY { get; set; }
        event onMove Move;
    }

    public delegate void onMove(object sender, onMoveArgs args);

    public class onMoveArgs
    {
    }
}