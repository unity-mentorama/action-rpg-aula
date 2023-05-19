using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int MaxHealth;

	private int _currentHealth;

	public event Action OnHealthDepleted;
	public event Action<int> OnHealthChanged;

	public bool IsAlive => _currentHealth > 0;

	private void Start()
	{
		_currentHealth = MaxHealth;
	}

	public void DealDamage(int damage)
	{
		_currentHealth -= damage;

		if (_currentHealth <= 0)
		{
			_currentHealth = 0;
			OnHealthDepleted?.Invoke();
		}

		OnHealthChanged?.Invoke(_currentHealth);
	}

	public void Heal(int healAmount)
	{
		_currentHealth += healAmount;

		if (_currentHealth > MaxHealth)
		{
			_currentHealth = MaxHealth;
		}

		OnHealthChanged?.Invoke(_currentHealth);
	}
}
