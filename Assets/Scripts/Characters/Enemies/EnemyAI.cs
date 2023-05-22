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
		// O bug que encontramos na aula acontecia somente quando dois inimigos atacavam
		// juntos. O HurtBox deles por estarem ambos na Layer de Player colidiam uma com
		// a do outro e causava esse OnTriggerEnter no objeto parent a disparar. Isso pois
		// o comportamento padrão dos triggers é repassar seus eventos aos objetos parent.
		// Para resolver isso adicionei uma comparação de tag adicional para garantir
		// que a colisão veio do Hurtbox do Player.
		if (other.CompareTag("Hurt") && other.transform.parent.CompareTag("Player"))
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
