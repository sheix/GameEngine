using Engine.Interfaces;

namespace CursesTest.Factories
{
    public interface ILevelFactory
    {
        IGrid GenerateGrid();
    }
}
