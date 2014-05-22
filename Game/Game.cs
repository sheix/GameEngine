using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Engine;

namespace Game
{
    /// <summary>
    /// Game should in general handle the time, 
    /// Create scenes, pass scene to UI renderer
    /// </summary>
	public class Game : IGame
    {
        private IScene _scene;
        private ManualStrategy _strategy;
        public event EventHandler KeyPressed;

        private void InvokeKeyPressed(KeyPressedEventArgs e)
        {
            EventHandler handler = KeyPressed;
            if (handler != null) handler(this, e);
        }

        public void Start()
        {
            _scene = (new SceneGenerator()).GenerateScene("Default");
            _strategy = new ManualStrategy(this);
            var player = new Player(_strategy);
            _scene.AddActor(player);
            (_scene as IStage).PlaceActorToGrid(player);
            Task.Factory.StartNew(() => _scene.Play());
        }

        public IScene Scene
        {
            get { return _scene; }
        }

        public void _KeyPressed(string key)
        {
            InvokeKeyPressed(new KeyPressedEventArgs { Key = key});
        }
    }

    public class Player : PlacableActor
    {
        public Player(IStrategy strategy) : base("Player", strategy)
        {
            AllActions = new List<IAct>(new List<IAct> {new MoveAct("UP", this, null, null, null),
                                                             new MoveAct("DOWN", this, null, null, null),
                                                             new MoveAct("LEFT", this, null, null, null),
                                                             new MoveAct("RIGHT", this, null, null, null),
            });
        }

        
    }
    public class MoveAct : BaseAct
    {
        public MoveAct(string name, IActor self, IActor target, IItem first, IItem second) : base(name, self, target, first, second)
        {
            
        }

        #region Overrides of BaseAct

        public override int Do(IScene scene)
        {
            (scene as IStage).Move(Self as IPlacableActor, Name);
            return 1;
        }

        public override bool CanDo(IActor actor, IScene scene)
        {
            return true;
        }

        #endregion
    }
}
