using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Content/New Sword")]
public class Sword : Item
{
	public int AttackPower;
	public GameObject SwordPrefab;

	public override bool Use(GameObject user)
	{
		var disequipedSword = user.GetComponent<Equipments>().Equip(this);
		user.GetComponent<Inventory>().AddItem(disequipedSword);

		return true;
	}
}
