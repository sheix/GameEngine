using Engine.Interfaces;

namespace Engine
{
	public class Act : ITargetedAct
	{
	    private IActor _self;
	    private IActor _target;
	    private IItem _first;
	    private IItem _second;
	    private string _name;

		public Act (string name, IActor self, IActor target, IItem first = null, IItem second = null)
		{
			_name = name;
			_self = self;
			_target = target;
			_first = first;
			_second = second;
		}

	    public IActor Self
	    {
	        get { return _self; }
	        set { _self = value; }
	    }

	    public IActor Target
	    {
	        get { return _target; }
	        set { _target = value; }
	    }

	    public IItem First
	    {
	        get { return _first; }
	        set { _first = value; }
	    }

	    public IItem Second
	    {
	        get { return _second; }
	        set { _second = value; }
	    }

	    public string Name
	    {
	        get { return _name; }
	        set { _name = value; }
	    }

	    public virtual void Do(IScene scene)
		{
            //Must override
		}

		public virtual bool CanDo(IScene scene)
		{
            //Must override
			return false;
		}

	}
}

