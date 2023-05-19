using UnityEngine;

public class Equipments : MonoBehaviour
{
	public Transform WeaponPosition;
	public Sword EquippedSword;
	public AttackPower HurtBox;

	private GameObject _swordInstance;

	private void Start()
	{
		_swordInstance = Instantiate(EquippedSword.SwordPrefab, WeaponPosition);
	}

	public Item Equip(Sword sword)
	{
		Destroy(_swordInstance);
		var disequippedSword = EquippedSword;
		EquippedSword = sword;
		HurtBox.Power = sword.AttackPower;

		_swordInstance = Instantiate(EquippedSword.SwordPrefab, WeaponPosition);

		return disequippedSword;
	}
}
