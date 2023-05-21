using UnityEngine;
using UnityEngine.AI;

public abstract partial class EnemyMachineState
{
	public enum EnemyState
	{
		Idle, Patrol, Chase, Attack, Hurt, Die
	}

	protected enum Event
	{
		Enter, Update, Exit
	}

	public EnemyState Name;

	protected Event Stage;
	protected EnemyAI EnemyAI;
	protected EnemyMachineState NextState;

	private const float MaxViewingHeight = 3f;

	protected AIData Data => EnemyAI.Data;

	public EnemyMachineState(EnemyAI enemyAI)
	{
		EnemyAI = enemyAI;
		Stage = Event.Enter;
	}

	public virtual void Enter() { Stage = Event.Update; }
	public virtual void Update() { Stage = Event.Update; }
	public virtual void Exit() { Stage = Event.Enter; }

	public EnemyMachineState Handle()
	{
		switch (Stage)
		{
			case Event.Enter:
				Enter();
				break;

			case Event.Update:
				Update();
				break;

			case Event.Exit:
				Exit();

				// Corige um bug onde as vezes o inimigo não morria
				NextState.Enter();
				return NextState;
		}

		return this;
	}

	protected void ChangeState(EnemyMachineState nextState)
	{
		NextState = nextState;
		Stage = Event.Exit;
	}

	protected bool CanSensePlayer()
	{
		if (!Data.PlayerTransform.GetComponent<Health>().IsAlive)
		{
			return false;
		}

		Vector2 direction = Data.PlayerTransform.position.XZ() - Data.CharacterMotor.transform.position.XZ();
		if (direction.magnitude <= Data.VisibleDistance)
		{
			return true;
		}

		return false;
	}

	protected bool CanSeePlayer()
	{
		if (!Data.PlayerTransform.GetComponent<Health>().IsAlive)
		{
			return false;
		}

		Vector2 direction = Data.PlayerTransform.position.XZ() - Data.CharacterMotor.transform.position.XZ();
		float angle = Vector2.Angle(direction, Data.CharacterMotor.transform.forward.XZ());
		float yDifference = Mathf.Abs(Data.PlayerTransform.position.y - Data.CharacterMotor.transform.position.y);

		if (direction.magnitude <= Data.VisibleDistance && angle <= Data.ViewAngle && yDifference < MaxViewingHeight)
		{
			return !Data.CharacterMotor.Raycast(Data.PlayerTransform.position, out NavMeshHit _);
		}

		return CanAttackPlayer();
	}

	protected bool CanAttackPlayer()
	{
		if (!Data.PlayerTransform.GetComponent<Health>().IsAlive)
		{
			return false;
		}

		var distance = Data.PlayerTransform.position.FlatDistanceTo(Data.CharacterMotor.transform.position);
		float yDifference = Mathf.Abs(Data.PlayerTransform.position.y - Data.CharacterMotor.transform.position.y);

		if (distance <= Data.AttackDistance && yDifference < MaxViewingHeight)
		{
			return !Data.CharacterMotor.Raycast(Data.PlayerTransform.position, out NavMeshHit _);
		}

		return false;
	}
}
