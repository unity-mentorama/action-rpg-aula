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
			NextState = new ChaseState(EnemyAI);
			Stage = Event.Exit;
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
