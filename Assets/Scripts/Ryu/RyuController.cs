using UnityEngine;
using System.Collections;
using Rewired;

public class RyuController : MonoBehaviour, ICharacterController {
	public Actions actions;

	private PlayerController playerController;
	private Character character;
	private Rigidbody2D rigidbody2D;

	void Start () {
		playerController = GetComponentInParent <PlayerController> ();
		rigidbody2D = GetComponentInParent<Rigidbody2D> ();
		character = GetComponent<Character> ();
	}

	void PlayAction (AnimAction action) {
		StartCoroutine (action.Play (playerController, character, action));
	}

	#region ICharacterController implementation

	public void Jump () {
		var newVelocity = new Vector2 (rigidbody2D.velocity.x, rigidbody2D.velocity.y);
		playerController.grounded = false;
		playerController.canJump = false;
		newVelocity.y = playerController.jumpVelocity;
		rigidbody2D.velocity = newVelocity;
	}

	public void Special () {
		throw new System.NotImplementedException ();
	}

	public void SpecialLeft () {
		throw new System.NotImplementedException ();
	}

	public void SpecialRight () {
		throw new System.NotImplementedException ();
	}

	public void SpecialDown () {
		throw new System.NotImplementedException ();
	}

	public void SpecialUp () {
		throw new System.NotImplementedException ();
	}

	public void Normal () {
		playerController.canAttack = false;
		PlayAction (actions.normal);
	}

	public void SmashLeft () {
		throw new System.NotImplementedException ();
	}

	public void SmashRight () {
		throw new System.NotImplementedException ();
	}

	public void SmashDown () {
		throw new System.NotImplementedException ();
	}

	public void SmashUp () {
		throw new System.NotImplementedException ();
	}

	#endregion
}
