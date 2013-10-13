using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeRat.OfficeRatScene;
using Engine;
using Engine.Interfaces;
using Moq;
using NUnit.Framework;

namespace OfficeRatTest
{
    [TestFixture]
    class OfficeRatSceneShould
    {
        private Mock<IPlacableActor> _actor;
        private Mock<IStrategy> _strategy;

        [Test]
        public void PutPlayerActorInSomeRandomPlaceOnTheGrid()
        {
            _actor = new Mock<IPlacableActor>();
            _strategy = new Mock<IStrategy>();

            var scene = new OfficeRatScene();
            scene.AddActor(_actor.Object);

            Assert.IsTrue(scene.Grid1.Contains(_actor.Object));
        }

    }
}
