using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	private List<Item> _items;
	private int _currentIndex;

	public event Action<Item> OnInventoryUpdated;
	public event Action<ItemType> OnItemUsed;
	public event Action OnNextItemSelected;

	public int Count => _items.Count;
	public Item NextItem => _items[GetNextIndex()];

	private void Start()
	{
		_items = new List<Item>();
		_currentIndex = 0;
	}

	public void UseItem(GameObject user)
	{
		if (_items.Count == 0)
		{
			return;
		}

		// O Use da sword adiciona um item no inventário e isso faz o
		// _currentIndex atualizar, precisamos salvar o _currentIndex
		// antes do uso nesse caso para remover a espada correta do
		// inventário depois.
		var originalCurrentIndex = _currentIndex;
		if (_items[_currentIndex].Use(user))
		{
			OnItemUsed?.Invoke(_items[_currentIndex].Type);
			_items.RemoveAt(originalCurrentIndex);

			if (_currentIndex == _items.Count)
			{
				_currentIndex--;
			}

			if (_items.Count > 0)
			{
				OnInventoryUpdated.Invoke(_items[_currentIndex]);
			}
			else
			{
				OnInventoryUpdated?.Invoke(null);
			}
		}
	}

	public void AddItem(Item item)
	{
		_items.Add(item);
		_currentIndex = _items.Count - 1;

		OnInventoryUpdated?.Invoke(item);
	}

	public void SelectNextItem()
	{
		if (_items.Count <= 1)
		{
			return;
		}

		_currentIndex = GetNextIndex();

		OnInventoryUpdated?.Invoke(_items[_currentIndex]);
		OnNextItemSelected?.Invoke();
	}

	private int GetNextIndex()
	{
		var nextIndex = _currentIndex + 1;

		if (nextIndex >= _items.Count)
		{
			nextIndex = 0;
		}

		return nextIndex;
	}
}
