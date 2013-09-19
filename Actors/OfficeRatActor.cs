using System;
using System.Collections.Generic;
using CursesTest.Acts;
using Engine;
using Engine.Interfaces;

namespace CursesTest.Actors
{
    class OfficeRatActor : Actor,  IPlacableActor
    {
        public OfficeRatActor(IStrategy strategy) : base(strategy)
        {
            allActions = new List<IAct>();
            AddMoves(allActions);
        }

        private static void AddMoves(List<IAct> actions)
        {
            actions.Add( new MoveAct(new Vector(1,1),new ConsoleKeyInfo('3',ConsoleKey.NumPad3,false,false,false)));
            actions.Add( new MoveAct(new Vector(1,0),new ConsoleKeyInfo('6',ConsoleKey.NumPad6,false,false,false)));
            actions.Add( new MoveAct(new Vector(0,1),new ConsoleKeyInfo('2',ConsoleKey.NumPad2,false,false,false)));
            actions.Add( new MoveAct(new Vector(-1,-1),new ConsoleKeyInfo('7',ConsoleKey.NumPad7,false,false,false)));
            actions.Add( new MoveAct(new Vector(-1,0),new ConsoleKeyInfo('4',ConsoleKey.NumPad4,false,false,false)));
            actions.Add( new MoveAct(new Vector(0,-1),new ConsoleKeyInfo('8',ConsoleKey.NumPad8,false,false,false)));
            actions.Add( new MoveAct(new Vector(-1,1),new ConsoleKeyInfo('1',ConsoleKey.NumPad1,false,false,false)));
            actions.Add( new MoveAct(new Vector(1,-1),new ConsoleKeyInfo('9',ConsoleKey.NumPad9,false,false,false)));
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
