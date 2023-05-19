using DG.Tweening;
using UnityEngine;

public class Gate : Door
{
	public Transform GateLeafOne;
	public Transform GateLeafTwo;

	public override bool Open()
	{
		if (Locked)
		{
			return false;
		}

		//GateLeafOne.Rotate(0, OpenAngle, 0);
		//GateLeafTwo.Rotate(0, -OpenAngle, 0);
		GateLeafOne.DORotate(GateLeafOne.rotation.eulerAngles + new Vector3(0, OpenAngle, 0), 3f).SetEase(Ease.OutBounce);
		GateLeafTwo.DORotate(GateLeafTwo.rotation.eulerAngles + new Vector3(0, -OpenAngle, 0), 3f).SetEase(Ease.OutBounce);
		OpenSound.Play(AudioSource);

		return true;
	}
}
