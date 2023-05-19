using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Content/New Potion")]
public class Potion : Item
{
	public int HealPower;

	public override bool Use(GameObject user)
	{
		user.GetComponent<Health>().Heal(HealPower);

		return true;
	}
}
