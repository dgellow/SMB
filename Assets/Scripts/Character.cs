using UnityEngine;
using System.Collections;

[System.Serializable]
public class Action {
	public AnimAction anim;
	public bool smash;
}

[System.Serializable]
public class Actions {
	public AnimAction special;
	public AnimAction specialDirection;
	public AnimAction normal;
}

public class Character : MonoBehaviour {
	public Actions actions;
	public Transform groundCheck;
	public SpriteRenderer spriteRenderer;
	public Animator anim;

	void Start() {
		anim = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
}