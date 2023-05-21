using System;
using UnityEngine;
using static EnemyMachineState;

public class EnemyAI : MonoBehaviour
{
	public AIData Data;

	private EnemyMachineState _currentMachineState;

	public event Action OnHurt;
	public event Action OnAttack;

	// Atualmente serve só para podermos ver no Inspector qual o estado atual do NPC
	[SerializeField]
	private EnemyState _currentState;

	public void Stagger()
	{
		OnHurt?.Invoke();
	}

	public void TriggerAttackEvent()
	{
		OnAttack?.Invoke();
	}

	private void Start()
	{
		Data.PlayerTransform = GameObject.FindWithTag("Player").transform;
		_currentMachineState = new IdleState(this);
	}

	private void Update()
	{
		_currentMachineState = _currentMachineState.Handle();
		_currentState = _currentMachineState.Name;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Hurt"))
		{
			var damage = other.GetComponent<AttackPower>().Power;
			Data.HealthComponent.DealDamage(damage);
			OnHurt?.Invoke();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, Data.VisibleDistance);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, Data.AttackDistance);
	}
}
