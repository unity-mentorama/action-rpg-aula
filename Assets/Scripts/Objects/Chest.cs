using DG.Tweening;
using Modulo19;
using UnityEngine;

public class Chest : Interactable
{
	public Transform Lid;
	public Item Item;
	public ParticleSystem Particles;
	public AudioSource AudioSource;
	public SimpleAudioEvent ChestOpenedSound;

	public override bool Interact(GameObject user, out string failureReason)
	{
		user.GetComponent<Inventory>().AddItem(Item);

		//Lid.transform.Rotate(-70, 0, 0);
		Lid.transform.DORotate(Lid.transform.rotation.eulerAngles + new Vector3(-70, 0, 0), 1f).SetEase(Ease.OutBack);
		Particles.Stop();

		GetComponent<BoxCollider>().enabled = false;

		ChestOpenedSound.Play(AudioSource);

		failureReason = "";
		return true;
	}
}
