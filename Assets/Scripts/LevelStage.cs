using UnityEngine;
using System.Collections;

public class LevelStage : MonoBehaviour {

	public Transform[] spawnPoints;
	public Transform[] startingBlocks;

	public void InitialSpawn() {
		var controllers = FindObjectsOfType<PlayerController> ();
		controllers.Randomize ();
		for (int i = 0; i < controllers.Length; i++) {
			var controller = controllers [i];
			var startingBlock = startingBlocks [i];
			if (startingBlock != null) {
				controller.transform.position = startingBlock.position;
			}
		}
	}

	public void Spawn(PlayerController controller) {
		var spawnPoint = spawnPoints.GetRandomValue ();
		controller.transform.position = spawnPoint.position;
	}
}
