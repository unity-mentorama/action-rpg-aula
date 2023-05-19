using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
	private enum InputType
	{
		Attack,
		Defend
	}

	[Header("Values")]
	public float AttackMoveSpeed = 0.5f;
	public float AttackBufferReset = 1f;
	public float ComboCooldown = 0.5f;

	[Header("References")]
	public GameObject HurtBox;
	public GameObject FollowTransform;
	public Health HealthComponent;
	public Animator Animator;
	public AnimationEvents AnimationEvents;

	private float _lastAttackCommandTimer;
	private float _comboCooldownTimer;
	// Event Queue
	private Queue<InputType> _inputBuffer = new Queue<InputType>();
	private bool _gotHit;
	private bool _defending;

	public event Action OnSwingWeapon;
	public event Action OnTakeHit;
	public event Action OnDefendHit;
	public event Action OnDeath;

	public bool Attacking { get; private set; }

	void Start()
	{
		Attacking = false;

		AnimationEvents.OnAttackStarted += AnimationEvents_OnAttackStarted;
		AnimationEvents.OnAttackEnded += AnimationEvents_OnAttackEnded;
		AnimationEvents.OnAttackAnimatioEnded += AnimationEvents_OnAttackAnimatioEnded;
		AnimationEvents.OnHitAnimatioEnded += AnimationEvents_OnHitAnimatioEnded;

		HurtBox.SetActive(false);
	}

	private void Update()
	{
		// Morto não ataca.
		if (!HealthComponent.IsAlive)
		{
			return;
		}

		if (Input.GetMouseButtonUp(1))
		{
			Animator.SetBool("ShieldUp", false);
			_defending = false;
		}

		if (_gotHit)
		{
			return;
		}

		// Enfileira input de defesa.
		if (Input.GetMouseButtonDown(1))
		{
			_inputBuffer.Clear();
			_inputBuffer.Enqueue(InputType.Defend);
		}

		// Enfileira input de ataque se não estiver defendendo e nem no cooldown.
		if (Input.GetMouseButtonDown(0) && _comboCooldownTimer < Time.time && !_defending)
		{
			_inputBuffer.Clear();
			_inputBuffer.Enqueue(InputType.Attack);
		}

		// Reseta o index do combo.
		if (Time.time > _lastAttackCommandTimer)
		{
			Animator.SetInteger("AttackIndex", 0);
		}

		// Consome a Event Queue dos inputs assim que acaba de atacar.
		if (_inputBuffer.Count > 0 && !Attacking)
		{
			switch (_inputBuffer.Dequeue())
			{
				case InputType.Attack:
					// Rotaciona o player para a direção do FollowTransform.
					transform.rotation = Quaternion.Euler(0, FollowTransform.transform.rotation.eulerAngles.y, 0);
					// E reseta a rotação y do FollowTransform já que rotacionamos o player pra ficar igual a ele.
					FollowTransform.transform.localEulerAngles = new Vector3(FollowTransform.transform.localEulerAngles.x, 0, 0);

					Animator.SetTrigger("Attack");
					Attacking = true;
					OnSwingWeapon?.Invoke();
					break;

				case InputType.Defend:
					Animator.SetBool("ShieldUp", true);
					_defending = true;
					break;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Hurt") || !HealthComponent.IsAlive)
		{
			return;
		}

		// Cancela a Event Queue caso receba dano.
		_inputBuffer.Clear();
		_gotHit = true;
		Animator.ResetTrigger("Attack");

		// Cancela o ataque atual.
		HurtBox.SetActive(false);
		Attacking = false;
		Animator.SetInteger("AttackIndex", 0);

		if (_defending)
		{
			other.GetComponentInParent<EnemyAI>().Stagger();
			Animator.SetTrigger("Hit");
			OnDefendHit?.Invoke();
		}
		else
		{
			HealthComponent.DealDamage(other.GetComponent<AttackPower>().Power);

			if (!HealthComponent.IsAlive)
			{
				Animator.SetTrigger("Die");
				OnDeath?.Invoke();
			}
			else
			{
				Animator.SetTrigger("Hit");
				OnTakeHit?.Invoke();
			}
		}
	}

	private void AnimationEvents_OnAttackStarted(int attackIndex)
	{
		if (_gotHit)
		{
			return;
		}

		HurtBox.SetActive(true);
	}

	private void AnimationEvents_OnAttackEnded(int attackIndex)
	{
		HurtBox.SetActive(false);
		// Se mover o Attacking = false; para cá você acabou de implementar animation cancel.
		//Attacking = false;
		Animator.SetInteger("AttackIndex", ++attackIndex % 3);
		_lastAttackCommandTimer = Time.time + AttackBufferReset;

		if (attackIndex == 3)
		{
			_comboCooldownTimer = Time.time + ComboCooldown;

			// Limpar somente inputs de ataque da Event Queue
			if (_inputBuffer.Contains(InputType.Defend))
			{
				_inputBuffer.Clear();
				_inputBuffer.Enqueue(InputType.Defend);
			}
			else
			{
				_inputBuffer.Clear();
			}
		}
	}

	private void AnimationEvents_OnAttackAnimatioEnded(int attackIndex)
	{
		// Setando o Attacking aqui nós obrigamos o jogador a esperar toda a animação.
		Attacking = false;
	}

	private void AnimationEvents_OnHitAnimatioEnded()
	{
		_gotHit = false;
	}
}
