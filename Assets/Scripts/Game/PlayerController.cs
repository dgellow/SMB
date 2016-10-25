using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public float walkSpeed = 10f;
	public float runSpeed = 20f;
	public float runThreshold = 0.5f;
	public float jumpVelocity = 100;
	public LayerMask groundLayer;
	public GameObject characterPrefab;
	public Character character;

	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public Player player;

	public bool canJump = false;
	public bool canMove = false;
	public bool canAttack = false;
	public bool grounded = false;

	private ICharacterController characterController;
	private Direction facingDirection = Direction.Right;
	private Rigidbody2D rigidbody2D;
	private float groundCheckRadius = 0.2f;
	private Transform groundCheck;

	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		// Do nothing if character has not been instantiate
		if (character == null) {
			return;
		}

		// If character has been instantiate ...
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);
		canJump = grounded;
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		if (canJump) {
			if (player.GetButtonDown ("X") || player.GetButtonDown ("Y")) {
				characterController.Jump ();
			}
//			var verticalAxis = player.GetAxis ("Control Stick Vertical");
//			if (verticalAxis == 1) {
//				playerController.grounded = false;
//				playerController.canJump = false;
//				newVelocity.y = playerController.jumpVelocity;
//				rigidbody2D.velocity = newVelocity;
//			}
		}

		if (canMove) {
			var move = player.GetAxis ("Control Stick Horizontal");
			var moveDirection = move > 0 ? 1 : -1;

			if (Mathf.Abs (move) > runThreshold) {
				rigidbody2D.velocity = new Vector2 (moveDirection * runSpeed, rigidbody2D.velocity.y);
			} else if (Mathf.Abs (move) > 0) {
				rigidbody2D.velocity = new Vector2 (moveDirection * walkSpeed, rigidbody2D.velocity.y);
			}

			anim.SetFloat ("hSpeed", Mathf.Abs (move));

			if ((move < 0 && facingDirection == Direction.Right) ||
			    (move > 0 && facingDirection == Direction.Left)) {
				Flip ();
			}
		}

		if (canAttack) {
			var horizontal = player.GetAxis ("Control Stick Horizontal");
			var vertical = player.GetAxis ("Control Stick Vertical");
			var cHorizontal = player.GetAxis ("C Stick Horizontal");
			var cVertical = player.GetAxis ("C Stick Vertical");

			// A Button
			if (player.GetButtonDown ("A")) {
				if (horizontal == -1) {
					characterController.SmashLeft ();	
				} else if (horizontal == 1) {
					characterController.SmashRight ();	
				} else if (vertical == -1) {
					characterController.SmashDown ();	
				} else if (vertical == 1) {
					characterController.SmashUp ();	
				} else {
					characterController.Normal ();
				}
			}
		// C Stick
		else if (cHorizontal != 0 || cVertical != 0) {
				if (cHorizontal == -1) {
					characterController.SmashLeft ();	
				} else if (cHorizontal == 1) {
					characterController.SmashRight ();	
				} else if (cVertical == -1) {
					characterController.SmashDown ();	
				} else if (cVertical == 1) {
					characterController.SmashUp ();	
				}
			} 
		// B Button
		else if (player.GetButtonDown ("B")) {
				if (horizontal == -1) {
					characterController.SpecialLeft ();	
				} else if (horizontal == 1) {
					characterController.SpecialRight ();	
				} else if (vertical == -1) {
					characterController.SpecialDown ();	
				} else if (vertical == 1) {
					characterController.SpecialUp ();	
				} else {
					characterController.Special ();
				}
			}
		}
	}

	public void InstantiateCharacter() {
		var obj = Instantiate (characterPrefab, transform) as GameObject;
		obj.transform.localPosition = new Vector3 (0, 0, 0);
		character = obj.GetComponent<Character> ();
		groundCheck = character.groundCheck;
		anim = character.GetComponent <Animator> ();
		characterController = character.GetComponent<ICharacterController> ();
		rigidbody2D.velocity = new Vector2 ();
	}

	void Flip () {
		facingDirection = facingDirection == Direction.Right ? Direction.Left : Direction.Right;
		var newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}
}