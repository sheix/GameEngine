using System;
using CursesTest.Data;
using CursesTest.OfficeRatScene;
using Engine;
using Engine.Interfaces;

namespace CursesTest.Acts
{
    public class MoveAct : IDirectionalAct, IKeyedAct
    {
        private readonly Vector _direction;
        private readonly ConsoleKeyInfo _key;

        public MoveAct(Vector direction, ConsoleKeyInfo key)
        {
            _direction = direction;
            _key = key;
        }

        public bool CanDo(IScene scene)
        {
            if (scene is IOfficeRatScene)
                ((IOfficeRatScene) scene).Grid1.GetActorCoordinates(Self);
            return true;
        }

        public void Do(IScene scene)
        {
            throw new NotImplementedException();
        }

        public IActor Self
        {
            get; set;
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
            get { return _key; }
        }
    }
}
