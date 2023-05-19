using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Content/New Key")]
public class Key : Item
{
	public override bool Use(GameObject user)
	{
		if (user.GetComponent<InteractionController>().CurrentInteractable is DoorInteractionTrigger doorInteractionTrigger)
		{
			if (doorInteractionTrigger.Door.Locked)
			{
				doorInteractionTrigger.Door.Unlock();
				return true;
			}
		}

		return false;
	}
}
