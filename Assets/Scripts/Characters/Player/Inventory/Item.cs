using UnityEngine;

public abstract class Item : ScriptableObject
{
	public string Name;
	public Sprite Sprite;
	public ItemType Type;

	public abstract bool Use(GameObject user);
}
