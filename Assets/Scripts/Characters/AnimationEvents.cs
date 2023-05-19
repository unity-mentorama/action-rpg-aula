using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
	public event Action<int> OnAttackStarted;
	public event Action<int> OnAttackEnded;
	public event Action<int> OnAttackAnimatioEnded;
	public event Action OnHitAnimatioEnded;
	public event Action OnStep;

	public void AttackStarted(int attackIndex)
	{
		OnAttackStarted?.Invoke(attackIndex);
	}

	public void AttackEnded(int attackIndex)
	{
		OnAttackEnded?.Invoke(attackIndex);
	}

	public void AttackAnimationEnded(int attackIndex)
	{
		OnAttackAnimatioEnded?.Invoke(attackIndex);
	}

	public void HitAnimationEnded()
	{
		OnHitAnimatioEnded?.Invoke();
	}

	public void Step()
	{
		OnStep?.Invoke();
	}
}
