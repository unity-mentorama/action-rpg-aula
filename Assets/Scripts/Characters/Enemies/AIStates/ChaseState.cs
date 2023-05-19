using UnityEngine;

public class ChaseState : BaseState
{
	public ChaseState(EnemyAI enemyAI)
		: base(enemyAI)
	{
		Name = EnemyState.Chase;
	}

	public override void Enter()
	{
		Data.CharacterMotor.speed = Data.ChaseSpeed;
		Data.CharacterMotor.SetDestination(Data.PlayerTransform.position);

		Data.Animator.SetBool("Moving", true);

		base.Enter();
	}

	public override void Update()
	{
		Data.CharacterMotor.SetDestination(Data.PlayerTransform.position);

		if (CanAttackPlayer())
		{
			Data.CharacterMotor.speed = 0;
			Data.CharacterMotor.SetDestination(Data.CharacterMotor.transform.position);
			Data.CharacterMotor.speed = Data.ChaseSpeed;

			if (Time.time >= Data.NextAttackCooldown)
			{
				// Configura o cooldown do próximo ataque no Data.AttackCooldown
				// Dessa forma o cooldown é persistido entre qualquer estados.
				Data.NextAttackCooldown = Time.time + Data.AttackCooldown;

				NextState = new AttackState(EnemyAI);
				Stage = Event.Exit;
			}
		}
		// Em combate o NPC fica mais "inteligente" e usa o 'Sense' ao invés do 'See'
		else if (!CanSensePlayer())
		{
			NextState = new PatrolState(EnemyAI);
			Stage = Event.Exit;
		}
	}

	public override void Exit()
	{
		Data.Animator.SetBool("Moving", false);

		base.Exit();
	}
}
