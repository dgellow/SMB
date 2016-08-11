using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class KillWhenOutside : MonoBehaviour {
	
	void OnTriggerExit2D(Collider2D other) {
		var characterController = other.GetComponent<PlayerController> ();
		if (characterController != null) {
			GameController.gameState.Kill (characterController);			
		}
	}
}
