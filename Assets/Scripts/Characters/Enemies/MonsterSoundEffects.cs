using Modulo19;
using UnityEngine;

public class MonsterSoundEffects : MonoBehaviour
{
	[Header("References")]
	public AnimationEvents AnimationEvents;
	public EnemyAI EnemyAI;
	public Health HealthComponent;
	public AudioSource AudioSource;

	[Header("Audio Events")]
	public SimpleAudioEvent Footstep;
	public SimpleAudioEvent Attack;
	public SimpleAudioEvent Hit;
	public SimpleAudioEvent Die;

	private void Start()
	{
		AnimationEvents.OnStep += AnimationEvents_OnStep;
		EnemyAI.OnAttack += EnemyAI_OnAttack;
		EnemyAI.OnHurt += EnemyAI_OnHurt;
		HealthComponent.OnHealthDepleted += HealthComponent_OnHealthDepleted;
	}

	private void AnimationEvents_OnStep()
	{
		Footstep.Play(AudioSource);
	}

	private void EnemyAI_OnAttack()
	{
		Attack.Play(AudioSource);
	}

	private void EnemyAI_OnHurt()
	{
		Hit.Play(AudioSource);
	}

	private void HealthComponent_OnHealthDepleted()
	{
		Die.Play(AudioSource);
	}
}
