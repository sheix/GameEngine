using CursesTest.Data;
using Engine;

namespace CursesTest.Acts
{
    interface IDirectionalAct : IAct
    {
        Vector Direction { get; }
    }
}
