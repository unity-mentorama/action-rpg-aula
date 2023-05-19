using DG.Tweening;
using Modulo19;
using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
	[Header("UI")]
	public TextMeshProUGUI InteractionLabel;
	public TextMeshProUGUI InteractionFeedbackLabel;

	[Header("Audio")]
	public AudioSource AudioSource;
	public SimpleAudioEvent InteractionFailedSound;

	private InteractionController _interactionController;

	private void Start()
	{
		_interactionController = GameObject.FindWithTag("Player").GetComponent<InteractionController>();

		_interactionController.OnInteractionToggled += InteractionController_OnInteractionToggled;
		_interactionController.OnInteractionFailed += InteractionController_OnInteractionFailed;
	}

	private void InteractionController_OnInteractionToggled(bool hasActiveInteraction)
	{
		InteractionLabel.enabled = hasActiveInteraction;
	}

	private void InteractionController_OnInteractionFailed(string failureReason)
	{
		InteractionFeedbackLabel.text = failureReason;
		InteractionFeedbackLabel.color = new Color(1, 1, 1, 150f / 255f);
		DOTween.ToAlpha(() => InteractionFeedbackLabel.color,
			x => InteractionFeedbackLabel.color = x,
			0, 1).SetDelay(3f);

		InteractionFailedSound.Play(AudioSource);
	}
}
