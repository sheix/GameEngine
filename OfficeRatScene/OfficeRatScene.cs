using CursesTest.Factories;
using Engine;
using Engine.Interfaces;
using OfficeRatTest;

namespace CursesTest.OfficeRatScene
{
    public class OfficeRatScene : Scene
    {
        IGrid _grid;
        public OfficeRatScene():base()
        {
            ILevelFactory factory = new LevelFactory();
            
            _grid = factory.GenerateGrid();
        }

        public IGrid Grid1
        {
            get { return _grid; }
        }

        public override void Render()
        {
            base.Render();
        }

        
    }
}
