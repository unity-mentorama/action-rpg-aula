public class PatrolState : DetectingState
{
	private int _currentIndex = 0;

	public PatrolState(EnemyAI enemyAI)
		: base(enemyAI)
	{
		Name = EnemyState.Patrol;
		_currentIndex = 0;
	}

	public override void Enter()
	{
		Data.CharacterMotor.speed = Data.WalkSpeed;
		Data.CharacterMotor.SetDestination(Data.PatrolPoints[_currentIndex].position);
		Data.Animator.SetBool("Moving", true);
		base.Enter();
	}

	public override void Update()
	{
		if (Data.CharacterMotor.transform.position.FlatDistanceTo(Data.PatrolPoints[_currentIndex].position) <= 0.1f)
		{
			NextState = new IdleState(EnemyAI);
			Stage = Event.Exit;
		}
		else
		{
			base.Update();
		}
	}

	public override void Exit()
	{
		_currentIndex++;
		if (_currentIndex >= Data.PatrolPoints.Length)
		{
			_currentIndex = 0;
		}

		Data.PreviousStates.Push(this);

		Data.Animator.SetBool("Moving", false);

		base.Exit();
	}
}
