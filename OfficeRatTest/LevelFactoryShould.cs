using System.Linq;
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

        [Test]
        public void CreateGridLargerThanOneByOne()
        {
            var levelFactory = new LevelFactory();
            IGrid grid = levelFactory.GenerateGrid();


            Assert.Greater(grid.Grid.Count,1);
            Assert.True(grid.Grid.All(m =>m.Count > 0));
        }

        //[Test]
        //public void
    }
}
