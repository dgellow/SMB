using UnityEngine;
using System.Collections;

public class StageCamera : MonoBehaviour {
	private Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerController mostLeft = GameController.gameState.players [0];
		PlayerController mostRight = GameController.gameState.players [0];

		foreach(var playerController in GameController.gameState.players) {
			if (playerController.transform.position.x < mostLeft.transform.position.x) {
				mostLeft = playerController;
			}
			if (playerController.transform.position.x > mostRight.transform.position.x) {
				mostRight = playerController;
			}
		}

		var middle = (mostLeft.transform.position + mostRight.transform.position) / 2;
		camera.transform.LookAt (middle);
	}
}
