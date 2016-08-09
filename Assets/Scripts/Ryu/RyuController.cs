using UnityEngine;
using System.Collections;

public class RyuController : MonoBehaviour {

	private CharacterController2D controller;
	private Character character;
	private Rigidbody2D rigidbody2D;

	public AnimAction lowPunch;

	void Start () {
		controller = GetComponentInParent <CharacterController2D> ();
		rigidbody2D = GetComponentInParent <Rigidbody2D> ();
		character = GetComponent<Character> ();
	}

	void Update () {
		if (controller.grounded) {
			// Low Punch
			if (controller.canAttack && Input.GetButtonDown ("Special")) {
				controller.canAttack = false;
				PlayAction (character.actions.special);
			}

			// Jump
			var newVelocity = new Vector2 (rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			if (controller.canJump && Input.GetButtonDown ("Jump")) {
				controller.grounded = false;
				controller.canJump = false;
				newVelocity.y = controller.jumpVelocity;
				rigidbody2D.velocity = newVelocity;
			}
		}
	}

	void PlayAction(AnimAction action) {
		StartCoroutine (action.Play (controller, character, character.actions.special));
	}
}
