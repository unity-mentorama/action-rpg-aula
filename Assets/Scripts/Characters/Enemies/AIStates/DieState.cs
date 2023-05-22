using UnityEngine;

public class DieState : EnemyMachineState
{
	private float _dieTimer;

	public DieState(EnemyAI enemyAI)
		: base(enemyAI)
	{
		Name = EnemyState.Die;
	}

	public override void Enter()
	{
		Data.Animator.SetTrigger("Die");

		_dieTimer = 3f;
		Data.CharacterMotor.speed = 0;
		Data.CharacterMotor.gameObject.GetComponent<Collider>().enabled = false;

		base.Enter();
	}

	public override void Update()
	{
		_dieTimer -= Time.deltaTime;

		if (_dieTimer < 0)
		{
			GameObject.Destroy(Data.CharacterMotor.gameObject);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
