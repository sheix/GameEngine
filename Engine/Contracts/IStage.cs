using System;

namespace Engine.Contracts
{
	public interface IStage
    {
        void PlaceActorToGrid(IPlacableActor actor);
        void PlaceActorToGrid(IPlacableActor actor, Vector v);
	    Vector GetCenterOfInterest();
	    Vector GetMapDimensions();
        void Move(IPlacableActor self, string direction);
	    bool IsFreeInDirection(IPlacableActor actor, string direction);
	    void Attack(IPlacableActor placableActor, string name);
        IActor ActorInDirection(IPlacableActor actor, string direction);
		void SetMap (IGrid grid);
    }
}

