using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[Header("Character")]
	public Health HealthComponent;
	public bool IsPlayerHealthUI;

	[Header("UI")]
	public Image HealthFillBar;
	public TextMeshProUGUI HealthTextLabel;

	private void Start()
	{
		if (IsPlayerHealthUI)
		{
			HealthComponent = GameObject.FindWithTag("Player").GetComponent<Health>();
		}

		HealthTextLabel.text = $"{HealthComponent.MaxHealth} / {HealthComponent.MaxHealth}";
		HealthComponent.OnHealthChanged += PlayerHealthComponent_OnHealthChanged;
		HealthFillBar.fillAmount = 1f;
	}

	private void PlayerHealthComponent_OnHealthChanged(int currentHealth)
	{
		HealthTextLabel.text = $"{currentHealth} / {HealthComponent.MaxHealth}";
		HealthFillBar.fillAmount = currentHealth / (float)HealthComponent.MaxHealth;
	}
}
