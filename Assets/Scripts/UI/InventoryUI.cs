using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	[Header("UI")]
	public TextMeshProUGUI InventoryText;
	public Image ItemSprite;
	public GameObject NextItemPanel;
	public Image NextItemSprite;

	private Inventory _playerInventory;

	private void Start()
	{
		_playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
		_playerInventory.OnInventoryUpdated += PlayerInventory_OnInventoryUpdated;
	}

	private void PlayerInventory_OnInventoryUpdated(Item item)
	{
		if (item == null)
		{
			InventoryText.text = "";
			ItemSprite.enabled = false;
		}
		else
		{
			InventoryText.text = $"[F] to use\n{item.Name}";
			ItemSprite.enabled = true;
			ItemSprite.sprite = item.Sprite;
		}

		if (_playerInventory.Count > 1)
		{
			NextItemPanel.SetActive(true);
			NextItemSprite.sprite = _playerInventory.NextItem.Sprite;
		}
		else
		{
			NextItemPanel.SetActive(false);
		}
	}
}
