using Engine;

namespace CursesTest.Acts
{
    public interface IDirectionalAct : IAct
    {
        Vector Direction { get; }
    }
}
