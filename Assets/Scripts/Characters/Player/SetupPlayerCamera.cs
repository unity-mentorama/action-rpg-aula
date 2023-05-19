using Cinemachine;
using UnityEngine;

public class SetupPlayerCamera : MonoBehaviour
{
	public CinemachineVirtualCamera CinemachineCamera;

	private void Start()
	{
		var cameraFollowTarget = GameObject.FindWithTag("Player").transform.Find("CameraFollowTarget");
		CinemachineCamera.Follow = cameraFollowTarget;
		CinemachineCamera.LookAt = cameraFollowTarget;
	}
}
