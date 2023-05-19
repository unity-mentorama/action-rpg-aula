using UnityEngine;

public class DoorInteractionTrigger : Interactable
{
	public Door Door;
	public bool CanOpenDoorFromThisSide;

	public override bool Interact(GameObject user, out string failureReason)
	{
		failureReason = "";

		if (CanOpenDoorFromThisSide)
		{
			if (Door.Open())
			{
				Door.DisableInteractionTriggers();
				return true;
			}
			else
			{
				failureReason = "It's locked.";
				return false;
			}
		}
		else
		{
			failureReason = "Can't be opened from this side.";
			return false;
		}
	}
}
