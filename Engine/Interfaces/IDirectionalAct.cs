using Engine;

namespace OfficeRat.Acts
{
    public interface IDirectionalAct : IAct
    {
        Vector Direction { get; }
    }
}
