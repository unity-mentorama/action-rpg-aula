public abstract class BaseState : EnemyMachineState
{
	public BaseState(EnemyAI enemyAI)
		: base(enemyAI)
	{
		//
	}

	public override void Enter()
	{
		EnemyAI.OnHurt += EnemyAI_OnHurt;
		base.Enter();
	}

	private void EnemyAI_OnHurt()
	{
		if (Data.HealthComponent.IsAlive)
		{
			NextState = new HurtState(EnemyAI);
			Stage = Event.Exit;
		}
		else
		{
			NextState = new DieState(EnemyAI);
			Stage = Event.Exit;
		}
	}

	public override void Update()
	{

	}

	public override void Exit()
	{
		EnemyAI.OnHurt -= EnemyAI_OnHurt;
		base.Exit();
	}
}
