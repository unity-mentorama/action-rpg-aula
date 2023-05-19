using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AIData
{
	[Header("Detection")]
	public float VisibleDistance;
	public float ViewAngle;

	[Header("Movement Speeds")]
	public float WalkSpeed;
	public float ChaseSpeed;

	[Header("Timers")]
	public float IdleDuration;

	[Header("Attack")]
	public float AttackDistance;
	public float AttackDelay;
	public float AttackCooldown;

	[Header("References")]
	public NavMeshAgent CharacterMotor;
	public Transform[] PatrolPoints;
	public Animator Animator;
	public AnimationEvents AnimationEvents;
	public GameObject HurtBox;
	public Health HealthComponent;

	// Assessíveis somente por código
	[HideInInspector]
	public Transform PlayerTransform;

	[HideInInspector]
	public float NextAttackCooldown = 0;
	public Stack<EnemyMachineState> PreviousStates = new Stack<EnemyMachineState>();
}

