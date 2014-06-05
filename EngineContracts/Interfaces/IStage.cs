using System;

namespace Contracts
{
	public interface IStage
    {
        IGrid Map { get; }
        void PlaceActorToGrid(IPlacableActor actor);
        void PlaceActorToGrid(IPlacableActor actor, Vector v);
	    Vector GetCenterOfInterest();
	    Vector GetMapDimensions();
        void Move(IPlacableActor self, string direction);
	    bool IsFreeInDirection(IPlacableActor actor, string direction);
    }
}

