using Engine.Interfaces;
using NUnit.Framework;

namespace OfficeRatTest
{
    [TestFixture]
    public class LevelFactoryShould
    {
        
        [Test]
        public void FillGrid()
        {
            var levelFactory = new LevelFactory();
            IGrid grid = levelFactory.GenerateGrid();

            Assert.NotNull(grid);
        }

        //[Test]
        //public void
    }
}
