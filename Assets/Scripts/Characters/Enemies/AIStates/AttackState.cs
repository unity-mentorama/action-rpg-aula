using UnityEngine;

public class AttackState : BaseState
{
	private float _delayTimer;
	private bool _attacked;

	public AttackState(EnemyAI enemyAI)
		: base(enemyAI)
	{
		Name = EnemyState.Attack;
	}

	public override void Enter()
	{
		Data.CharacterMotor.transform.LookAt(Data.PlayerTransform);
		_delayTimer = Data.AttackDelay;
		_attacked = false;

		Data.AnimationEvents.OnAttackStarted += AnimationEvents_OnAttackStarted;
		Data.AnimationEvents.OnAttackEnded += AnimationEvents_OnAttackEnded;
		Data.AnimationEvents.OnAttackAnimatioEnded += AnimationEvents_OnAttackAnimatioEnded;

		EnemyAI.TriggerAttackEvent();

		base.Enter();
	}

	private void AnimationEvents_OnAttackStarted(int obj)
	{
		Data.HurtBox.SetActive(true);
	}

	private void AnimationEvents_OnAttackEnded(int obj)
	{
		Data.HurtBox.SetActive(false);
	}

	private void AnimationEvents_OnAttackAnimatioEnded(int obj)
	{
		NextState = new ChaseState(EnemyAI);
		Stage = Event.Exit;
	}

	public override void Update()
	{
		_delayTimer -= Time.deltaTime;

		if (!_attacked && _delayTimer < 0)
		{
			Data.Animator.SetTrigger("Attack1");
			_attacked = true;
		}
	}

	public override void Exit()
	{
		Data.Animator.ResetTrigger("Attack1");

		Data.HurtBox.SetActive(false);
		Data.AnimationEvents.OnAttackStarted -= AnimationEvents_OnAttackStarted;
		Data.AnimationEvents.OnAttackEnded -= AnimationEvents_OnAttackEnded;
		Data.AnimationEvents.OnAttackAnimatioEnded -= AnimationEvents_OnAttackAnimatioEnded;
		base.Exit();
	}
}
