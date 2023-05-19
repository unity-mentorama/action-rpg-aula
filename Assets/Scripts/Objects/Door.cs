using DG.Tweening;
using Modulo19;
using UnityEngine;

public class Door : MonoBehaviour
{
	[Header("Values")]
	public float OpenAngle = 90f;
	public bool Locked;

	[Header("References")]
	public DoorInteractionTrigger[] InteractionTriggers;
	public AudioSource AudioSource;
	public SimpleAudioEvent OpenSound;

	public void Unlock()
	{
		Locked = false;
	}

	public virtual bool Open()
	{
		if (Locked)
		{
			return false;
		}

		//transform.Rotate(0, OpenAngle, 0);
		transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, OpenAngle, 0), 1f).SetEase(Ease.OutBounce);
		OpenSound.Play(AudioSource);

		return true;
	}

	public void DisableInteractionTriggers()
	{
		foreach (var trigger in InteractionTriggers)
		{
			trigger.GetComponent<BoxCollider>().enabled = false;
		}
	}
}
