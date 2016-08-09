using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	public float maxSpeed = 10f;
	public float jumpVelocity = 100;
	public Transform groundCheck;
	public LayerMask groundLayer;

	public float lowPunchRecovery = 1;

	[SerializeField] private bool canJump = true;
	[SerializeField] private bool canLowPunch = true;
	[SerializeField] private bool canMove = true;

	private Directions facingDirection = Directions.Right;
	private Rigidbody2D rigidbody2D;
	private Animator anim;
	private bool grounded = false;
	private float groundCheckRadius = 0.2f;

	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);
		anim.SetBool ("grounded", grounded);
		canJump = grounded;

		var move = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		anim.SetFloat ("hSpeed", Mathf.Abs (move));
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		if ((move < 0 && facingDirection == Directions.Right) ||
			(move > 0 && facingDirection == Directions.Left)) {
			Flip ();
		}
	}

	void Update () {
		if (grounded) {
			// Low Punch
			if (canLowPunch && Input.GetButtonDown ("Low Punch")) {
				anim.SetTrigger ("lowPunch");
				canLowPunch = false;
				StartCoroutine(RecoverLowPunch (lowPunchRecovery));
			}

			// Jump
			var newVelocity = new Vector2 (rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			if (canJump && Input.GetButtonDown ("Jump")) {
				grounded = false;
				canJump = false;
				newVelocity.y = jumpVelocity;
				rigidbody2D.velocity = newVelocity;
			}
		}
	}

	void Flip() {
		facingDirection = facingDirection == Directions.Right ? Directions.Left : Directions.Right;
		var newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}
		
	IEnumerator RecoverLowPunch(float recoveryTime) {
		yield return new WaitForSeconds(recoveryTime);
		canLowPunch = true;
	}
}