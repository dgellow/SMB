using UnityEngine;
using System.Collections;

[System.Serializable]
public class HitBox : MonoBehaviour {
	public HitBoxType type;	
	public float damage;

	[HideInInspector]
	public BoxCollider2D collider2D;

	void Start () {
		collider2D = GetComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		var otherHitBox = other.GetComponent<HitBox> ();
		var controller = GetComponentInParent<PlayerController> ();
		var otherController = other.GetComponentInParent<PlayerController> ();
		if (otherHitBox && (type == HitBoxType.Damage && otherHitBox.type == HitBoxType.Attack)) {
			Debug.Log (controller.gameObject.name + " hit by " + otherController.gameObject.name + " for " + otherHitBox.damage + " damages");

			var force = controller.transform.position - otherController.transform.position;
			force.Normalize ();
			force.y = 1;
			force.Scale (new Vector2(otherHitBox.damage * 2000, otherHitBox.damage * 1000));
			var rigidbody2D = GetComponentInParent<Rigidbody2D> ();
			rigidbody2D.AddForce (force, ForceMode2D.Force);

			GameController.gameState.matchState.Hit (otherController, controller, otherHitBox.damage);
		}
	}
}
