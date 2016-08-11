using UnityEngine;
using System.Collections;
using System;

public class DebugInputs : MonoBehaviour {
	
	void Update () {
		if (DebugController.debugState.logInput) {
			if (Input.anyKeyDown) {
				foreach(KeyCode keyCode in Enum.GetValues(typeof(KeyCode))) {
					if (Input.GetKeyDown (keyCode)) {
						Debug.Log ("Input: " + keyCode);
					}
				}
			}
		}
	}
}
