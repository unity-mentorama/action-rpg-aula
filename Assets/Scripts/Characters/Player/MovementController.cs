using UnityEngine;

public class MovementController : MonoBehaviour
{
	[Header("Values")]
	public float BaseSpeed = 12f;
	public float Gravity = -9.81f;
	public float SprintSpeed = 5f;
	public float RotationPower = 3f;

	[Header("References")]
	public CombatController CombatController;
	public CharacterController CharacterController;
	public GameObject FollowTransform;
	public Animator Animator;
	public AnimationEvents AnimationEvents;
	public Health HealthComponent;

	private Vector2 _look;
	private float _speedBoost = 1f;
	private Vector3 _velocity;

	private void Start()
	{
		// Trava e esconde o cursor durante o jogo
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		_look = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

		#region Follow Transform Rotation

		// Rotaciona o transform com base no input do mouse.
		FollowTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * RotationPower, Vector3.up);

		#endregion

		#region Vertical Rotation
		FollowTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * RotationPower, Vector3.right);

		var angles = FollowTransform.transform.localEulerAngles;
		angles.z = 0;

		var angle = FollowTransform.transform.localEulerAngles.x;

		// Trava a rotação pra cima e pra baixo, impedindo a camera de ficar de ponta cabeça.
		if (angle > 180 && angle < 340)
		{
			angles.x = 340;
		}
		else if (angle < 180 && angle > 40)
		{
			angles.x = 40;
		}

		FollowTransform.transform.localEulerAngles = angles;
		#endregion

		float x = 0;
		float z = 0;
		if (HealthComponent.IsAlive)
		{
			x = Input.GetAxis("Horizontal");
			z = Input.GetAxis("Vertical");
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			_speedBoost = SprintSpeed;
		}
		else
		{
			_speedBoost = 1f;
		}

		// Se houve algum input de movimentação.
		if (x != 0 || z != 0)
		{
			// Rotaciona o player para a direção do FollowTransform.
			transform.rotation = Quaternion.Euler(0, FollowTransform.transform.rotation.eulerAngles.y, 0);
			// E reseta a rotação y do FollowTransform já que rotacionamos o player pra ficar igual a ele.
			FollowTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

			// Se o player está atacando ele não se move
			if (!CombatController.Attacking)
			{
				Vector3 move = (transform.right * x) + (transform.forward * z);

				CharacterController.Move(move * (BaseSpeed + _speedBoost) * Time.deltaTime);
				Animator.SetBool("Moving", true);
				Animator.SetFloat("RightVelocity", x);
				Animator.SetFloat("ForwardVelocity", z);
			}
		}
		else if (x == 0 && z == 0)
		{
			Animator.SetBool("Moving", false);
		}

		// Aplica a gravidade quando o personagem não está no chão.
		if (CharacterController.isGrounded)
		{
			_velocity.y = 0;
		}
		else
		{
			_velocity.y += Gravity * Time.deltaTime;
		}

		CharacterController.Move(_velocity * Time.deltaTime);
	}
}
