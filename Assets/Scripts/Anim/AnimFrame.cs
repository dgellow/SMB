using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimFrame : MonoBehaviour {
	public bool invulnerable;

	[HideInInspector]
	public List<HitBox> colliders;
	[HideInInspector]
	public Sprite sprite;

	public void Start() {
		var spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.enabled = false;
		sprite = spriteRenderer.sprite;
		foreach (HitBox hitBox in GetComponentsInChildren<HitBox> ()) {
			colliders.Add (hitBox);
		}
	}
}
