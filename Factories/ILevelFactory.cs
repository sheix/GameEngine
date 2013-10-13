using Engine.Interfaces;

namespace OfficeRat.Factories
{
    public interface ILevelFactory
    {
        IGrid GenerateGrid();
    }
}
