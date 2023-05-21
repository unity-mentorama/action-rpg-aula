public class HurtState : BaseState
{
	public HurtState(EnemyAI enemyAI)
		: base(enemyAI)
	{
		Name = EnemyState.Attack;
	}

	public override void Enter()
	{
		Data.Animator.SetTrigger("Hit");

		Data.AnimationEvents.OnHitAnimatioEnded += AnimationEvents_OnHitAnimatioEnded;

		Data.CharacterMotor.speed = 0;
		Data.CharacterMotor.transform.LookAt(Data.PlayerTransform);

		base.Enter();
	}

	private void AnimationEvents_OnHitAnimatioEnded()
	{
		ChangeState(new ChaseState(EnemyAI));
	}

	public override void Update()
	{
		//
	}

	public override void Exit()
	{
		Data.Animator.ResetTrigger("Hit");
		Data.AnimationEvents.OnHitAnimatioEnded -= AnimationEvents_OnHitAnimatioEnded;
		base.Exit();
	}
}
