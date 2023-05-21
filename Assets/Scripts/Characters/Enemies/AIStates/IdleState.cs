using UnityEngine;

public class IdleState : DetectingState
{
	private float _idleTimer;

	public IdleState(EnemyAI enemyAI)
		: base(enemyAI)
	{
		Name = EnemyState.Idle;
	}

	public override void Enter()
	{
		_idleTimer = Data.IdleDuration;
		base.Enter();
	}

	public override void Update()
	{
		_idleTimer -= Time.deltaTime;

		if (_idleTimer <= 0)
		{
			if (Data.PreviousStates.Count > 0)
			{
				ChangeState(Data.PreviousStates.Pop());
			}
			else
			{
				ChangeState(new PatrolState(EnemyAI));
			}
		}
		else
		{
			base.Update();
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
