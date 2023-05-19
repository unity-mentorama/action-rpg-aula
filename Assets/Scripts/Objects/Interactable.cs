using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public abstract bool Interact(GameObject user, out string failureReason);
}
