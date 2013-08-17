using System;
using CursesTest.Data;
using Engine;
using Engine.Interfaces;

namespace CursesTest.Acts
{
    class MoveAct : IDirectionalAct, IKeyedAct
    {
        private readonly Vector _direction;

        public MoveAct(Vector direction)
        {
            _direction = direction;
        }

        public bool CanDo(IScene scene)
        {
            throw new NotImplementedException();
        }

        public void Do(IScene scene)
        {
            throw new NotImplementedException();
        }

        public IActor Self
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { return "Move(" + _direction._x + ", " + _direction._y + ")"; }
            set {  }
        }

        public Vector Direction
        {
            get { return _direction; }
        }

        public ConsoleKeyInfo Key
        {
            get { throw new NotImplementedException(); }
        }
    }
}
