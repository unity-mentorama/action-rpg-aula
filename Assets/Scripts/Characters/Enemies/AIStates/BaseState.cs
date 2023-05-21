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
			ChangeState(new HurtState(EnemyAI));
		}
		else
		{
			ChangeState(new DieState(EnemyAI));
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
