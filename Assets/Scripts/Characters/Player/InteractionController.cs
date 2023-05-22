using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
	public Inventory PlayerInventory;
	public Health HealthComponent;

	public Interactable CurrentInteractable { get; private set; }

	public event Action<bool> OnInteractionToggled;
	public event Action<string> OnInteractionFailed;

	private void Update()
	{
		if (!HealthComponent.IsAlive)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable != null)
		{
			if (CurrentInteractable.Interact(gameObject, out string failureReason))
			{
				CurrentInteractable = null;
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
			CurrentInteractable = other.GetComponent<Interactable>();
			if (CurrentInteractable.enabled)
			{
				OnInteractionToggled?.Invoke(true);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Interact"))
		{
			CurrentInteractable = null;
			OnInteractionToggled?.Invoke(false);
		}
	}
}
