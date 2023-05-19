using Modulo19;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
	[Header("References")]
	public AnimationEvents AnimationEvents;
	public CombatController CombatController;
	public AudioSource AudioSource;

	[Header("Audio Events")]
	public SimpleAudioEvent Footstep;
	public SimpleAudioEvent Slash;
	public SimpleAudioEvent Hit;
	public SimpleAudioEvent Defend;
	public SimpleAudioEvent Die;

	private void Start()
	{
		AnimationEvents.OnStep += AnimationEvents_OnStep;
		CombatController.OnSwingWeapon += CombatController_OnSwingWeapon;
		CombatController.OnTakeHit += CombatController_OnTakeHit;
		CombatController.OnDefendHit += CombatController_OnDefendHit;
		CombatController.OnDeath += CombatController_OnDeath;
	}

	private void CombatController_OnSwingWeapon()
	{
		Slash.Play(AudioSource);
	}

	private void CombatController_OnTakeHit()
	{
		Hit.Play(AudioSource);
	}

	private void CombatController_OnDefendHit()
	{
		Defend.Play(AudioSource);
	}

	private void CombatController_OnDeath()
	{
		Die.Play(AudioSource);
	}

	private void AnimationEvents_OnStep()
	{
		Footstep.Play(AudioSource);
	}
}
