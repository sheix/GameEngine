﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CursesTest.OfficeRatScene;
using Engine;
using Moq;
using NUnit.Framework;

namespace OfficeRatTest
{
    [TestFixture]
    class OfficeRatSceneShould
    {
        private Mock<IActor> _actor;
        private Mock<IStrategy> _strategy;
        [Test]
        public void PutPlayerActorInSomeRandomPlaceOnTheGrid()
        {
            _actor = new Mock<IActor>();
            _strategy = new Mock<IStrategy>();

            var scene = new OfficeRatScene();
            scene.AddActor(_actor.Object);

            Assert.IsTrue(scene.Grid1.Contains(_actor.Object));
        }

    }
}
