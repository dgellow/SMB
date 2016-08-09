using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	public float maxSpeed = 10f;
	public float jumpVelocity = 100;
	public LayerMask groundLayer;
	public Character character;
	public string playerName;
	public int playerNumber;

	[HideInInspector]
	public Animator anim;

	public bool canJump = true;
	public bool canMove = true;
	public bool canAttack = true;
	public bool grounded = false;

	private Direction facingDirection = Direction.Right;
	private Rigidbody2D rigidbody2D;
	private float groundCheckRadius = 0.2f;
	private Transform groundCheck;

	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		anim = GetComponentInChildren<Animator> ();
		groundCheck = character.groundCheck;
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);
		canJump = grounded;
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		if (canMove) {
			var move = Input.GetAxis ("Horizontal");
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

			anim.SetFloat ("hSpeed", Mathf.Abs (move));

			if ((move < 0 && facingDirection == Direction.Right) ||
				(move > 0 && facingDirection == Direction.Left)) {
				Flip ();
			}
		}
	}
			
	void Flip() {
		facingDirection = facingDirection == Direction.Right ? Direction.Left : Direction.Right;
		var newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}
}