using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
	public Inventory PlayerInventory;
	public Health HealthComponent;

	private Interactable _currentInteractable;

	public Interactable CurrentInteractable => _currentInteractable;

	public event Action<bool> OnInteractionToggled;
	public event Action<string> OnInteractionFailed;

	private void Update()
	{
		if (!HealthComponent.IsAlive)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null)
		{
			if (_currentInteractable.Interact(gameObject, out string failureReason))
			{
				_currentInteractable = null;
				OnInteractionToggled?.Invoke(false);
			}
			else
			{
				OnInteractionFailed?.Invoke(failureReason);
			}
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			PlayerInventory.UseItem(gameObject);
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			PlayerInventory.SelectNextItem();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Interact"))
		{
			_currentInteractable = other.GetComponent<Interactable>();
			if (_currentInteractable.enabled)
			{
				OnInteractionToggled?.Invoke(true);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Interact"))
		{
			_currentInteractable = null;
			OnInteractionToggled?.Invoke(false);
		}
	}
}
