using UnityEngine;
public static class VectorExtensions
{
	public static Vector2 XZ(this Vector3 vv)
	{
		return new Vector2(vv.x, vv.z);
	}

	public static float FlatDistanceTo(this Vector3 from, Vector3 unto)
	{
		Vector2 a = from.XZ();
		Vector2 b = unto.XZ();
		return Vector2.Distance(a, b);
	}
}