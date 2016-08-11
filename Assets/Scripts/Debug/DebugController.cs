using UnityEngine;
using System.Collections;

public class DebugController : MonoBehaviour {

	public static DebugController debugState;

	public bool doNotDestroyOnLoad = false;
	public bool logInput = false;
	public bool showHitBoxes = false;

	// Use this for initialization
	void Awake () {
		if (doNotDestroyOnLoad) {
			if (debugState == null) {
				DontDestroyOnLoad (gameObject);
				debugState = this;
			} else if (debugState != this) {
				Destroy (debugState);
			}
		} else {
			debugState = this;
		}
	}
}
