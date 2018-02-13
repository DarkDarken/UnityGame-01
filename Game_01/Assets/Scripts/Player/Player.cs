using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerState))]
public class Player : MonoBehaviour {

	[System.Serializable]
	public class MouseInput {
		public Vector2 Damping;
		public Vector2 Sensitivity;
		public bool mouseLock;
	}

	[SerializeField]
	SwatSoldier settings;


	[SerializeField] float jumpSpeed = 5f;
	[HideInInspector] public MouseInput MouseControl;
	[SerializeField] public AudioController footSteps;
	[SerializeField] float minimumMoveTreshold;

	public PlayerAim playerAim;

	Vector3 lastPosition;

	private CharacterController m_MoveController;
	public CharacterController MoveController {
		get {
			if (m_MoveController == null)
				m_MoveController = GetComponent<CharacterController> ();
			return m_MoveController;
		}
	}


	private PlayerShoot m_PlayerShoot;
	public PlayerShoot PlayerShoot {
		get {
			if (m_PlayerShoot == null) 
				m_PlayerShoot = GetComponentInChildren<PlayerShoot> ();
			return m_PlayerShoot;
		}
	}

	private PlayerState m_PlayerState;
	public PlayerState PlayerState {
		get {
			if (m_PlayerState == null) 
				m_PlayerState = GetComponent<PlayerState> ();
			return m_PlayerState;
		}
	}

	InputController playerInput;
	Vector2 mouseInput;

	void Awake () {
		playerInput = GameManager.Instance.InputController;
		GameManager.Instance.LocalPlayer = this;

		if (MouseControl.mouseLock) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

	}

	void Update () {

		Move ();

		LookAround ();
	}

	void Move () {

		float moveSpeed = settings.RunSpeed;

		if (playerInput.IsJumping)
			

		if (playerInput.IsWalking)
			moveSpeed = settings.WalkSpeed;

		if (playerInput.IsSprinting)
			moveSpeed = settings.SprintSpeed;

		if (playerInput.IsCrouching)
			moveSpeed = settings.CrouchSpeed;

		Vector3 direction = new Vector3 (playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
		MoveController.SimpleMove(transform.forward * direction.x + transform.right * direction.y);


		if (Vector3.Distance (transform.position, lastPosition) > minimumMoveTreshold)
			footSteps.Play ();
		
		lastPosition = transform.position;

	}

	void LookAround ()
	{
		mouseInput.x = Mathf.Lerp (mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
		mouseInput.y = Mathf.Lerp (mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);
		transform.Rotate (Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);
		playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
	}
		

}
