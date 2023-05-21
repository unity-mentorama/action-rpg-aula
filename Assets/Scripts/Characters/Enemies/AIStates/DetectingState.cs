public abstract class DetectingState : BaseState
{
	public DetectingState(EnemyAI enemyAI)
		: base(enemyAI)
	{
		//
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		if (CanSeePlayer())
		{
			ChangeState(new ChaseState(EnemyAI));
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
