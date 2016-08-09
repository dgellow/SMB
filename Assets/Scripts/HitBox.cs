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
		var controller = GetComponentInParent<CharacterController2D> ();
		var otherController = GetComponentInParent<CharacterController2D> ();
		if (type == HitBoxType.Damage && otherHitBox.type == HitBoxType.Attack) {
			Debug.Log (controller.gameObject.name + " hit by " + otherController.gameObject.name + " for " + otherHitBox.damage + " damages");
			var force = new Vector3 (otherHitBox.damage, 0, 0);
			var rigidbody2D = GetComponentInParent<Rigidbody2D> ();
			rigidbody2D.AddForce (force, ForceMode2D.Force);
		}
	}
}
