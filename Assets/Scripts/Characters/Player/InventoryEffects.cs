using DG.Tweening;
using Modulo19;
using TMPro;
using UnityEngine;

public class InventoryEffects : MonoBehaviour
{
	[Header("References")]
	public Inventory PlayerInventory;
	public ParticleSystem HealParticle;
	public AudioSource AudioSource;
	public TextMeshProUGUI InteractionFeedbackLabel;

	[Header("Audio Events")]
	public SimpleAudioEvent HealSound;
	public SimpleAudioEvent UseKeySound;
	public SimpleAudioEvent EquipSwordSound;
	public SimpleAudioEvent NextItemSelectedSound;

	private void Start()
	{
		PlayerInventory.OnItemUsed += PlayerInventory_OnItemUsed;
		PlayerInventory.OnNextItemSelected += PlayerInventory_OnNextItemSelected;
	}

	private void PlayerInventory_OnItemUsed(ItemType itemType)
	{
		switch (itemType)
		{
			case ItemType.Potion:
				HealParticle.Play();
				HealSound.Play(AudioSource);
				break;

			case ItemType.Key:
				UseKeySound.Play(AudioSource);

				// Esse código ficou um pouco fora de lugar, ele mexe em UI numa classe de efeitos,
				// mas era a solução mais prática no momento
				InteractionFeedbackLabel.text = "Door unlocked.";
				InteractionFeedbackLabel.color = new Color(1, 1, 1, 150f / 255f);
				DOTween.ToAlpha(() => InteractionFeedbackLabel.color,
					x => InteractionFeedbackLabel.color = x,
					0, 1).SetDelay(3f);

				break;

			case ItemType.Sword:
				EquipSwordSound.Play(AudioSource);
				break;
		}
	}

	private void PlayerInventory_OnNextItemSelected()
	{
		NextItemSelectedSound.Play(AudioSource);
	}
}
