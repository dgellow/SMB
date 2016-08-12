using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class KillWhenOutside : MonoBehaviour {
	
	void OnTriggerExit2D(Collider2D other) {
		var playerController = other.GetComponentInParent <PlayerController> ();
		if (playerController != null) {
			GameController.gameState.matchState.Kill (playerController);
		}
	}
}
