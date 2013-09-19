using System;
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
            var coords = (scene as IOfficeRatScene).Grid1.GetActorCoordinates(Self);
			Console.WriteLine ("Self: [" + coords._x +","+ coords._y+"]");
			Console.WriteLine ("Dir : [" + _direction._x +","+ _direction._y+"]");
			var cell = (scene as IOfficeRatScene).Grid1.At(coords._x + _direction._x,coords._y + _direction._y);
			if (cell.Actor != null) return false;
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
