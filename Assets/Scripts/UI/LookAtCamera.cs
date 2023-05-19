using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private void Update()
	{
		transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
	}
}
