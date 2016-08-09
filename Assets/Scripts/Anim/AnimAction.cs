using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimAction : MonoBehaviour {
	private List<AnimFrame> frames;

	void Start() {
		frames = new List<AnimFrame>();
		foreach(Transform child in transform) {
			var frame = child.GetComponent<AnimFrame> ();
			if (frame != null) {
				frames.Add (frame);
			}
		}
	}

	public IEnumerator Play(CharacterController2D controller, Character character, AnimAction action) {
		character.anim.enabled = false;
		controller.canAttack = false;
		controller.canJump = false;
		controller.canMove = false;

		foreach(var frame in frames) {
			character.spriteRenderer.sprite = frame.sprite;
			foreach (var hitBox in frame.colliders) {
				hitBox.collider2D.enabled = true;
			}

			yield return new WaitForFixedUpdate ();
			foreach(var hitBox in frame.colliders) {
				hitBox.collider2D.enabled = false;
			}
		}

		character.anim.enabled = true;
		controller.canAttack = true;
		controller.canJump = true;
		controller.canMove = true;
	}
}
