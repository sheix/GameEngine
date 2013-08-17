using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Interfaces;

namespace CursesTest.Actors
{
    class OfficeRatActor : Actor,  IPlacableActor
    {
        public OfficeRatActor(IStrategy strategy) : base(strategy)
        {

        }

        public int InitialX
        {
            get; set;
        }

        public int InitialY
        {
            get; set;
        }
    }
}
