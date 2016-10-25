using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour {

	public Transform[] spawnPoints;
	public Transform[] startingBlocks;
	public float introReadyDuration = 5;
	public float introStartDuration = 2;
	public float endingDuration = 3;

	private Canvas canvas;
	private Text introText;

	void Start() {
		canvas = FindObjectOfType<Canvas> ();
		var camera = FindObjectOfType<Camera> ();
		canvas.worldCamera = camera;
		introText = GameObject.Find ("IntroText").GetComponent<Text> ();

		var controllers = FindObjectsOfType<PlayerController> ();
		controllers.Randomize ();
		for (int i = 0; i < controllers.Length; i++) {
			var controller = controllers [i];
			var startingBlock = startingBlocks [i];
			if (startingBlock != null) {
				controller.transform.position = startingBlock.position;
			}
		}

		GameController.gameState.StartMatch ();
		StartCoroutine (PlayIntro ());
	}

	void FixedUpdate() {
		var rules = GameController.gameState.matchRules as IMatchRules;
		if (rules.CheckVictory ()) {
			foreach (var player in GameController.gameState.players) {
				player.enabled = false;
			}
			StartCoroutine (PlayEnding());
		} else {
			Respawn ();
		}
	}

	void Spawn(PlayerController playerController) {
		var spawnPoint = spawnPoints.GetRandomValue ();
		playerController.transform.position = spawnPoint.position;
		playerController.InstantiateCharacter ();
		playerController.enabled = true;
		GameController.gameState.matchState.Revive (playerController);
	}

	IEnumerator PlayIntro() {
		foreach (var player in GameController.gameState.players) {
			player.enabled = false;
		}
		introText.enabled = true;
		introText.text = "Ready ...";
		yield return new WaitForSeconds (introReadyDuration);
		introText.text = "Fight!";
		yield return new WaitForSeconds (introStartDuration);
		introText.enabled = false;
		foreach (var player in GameController.gameState.players) {
			player.enabled = true;
		}
	}

	IEnumerator PlayEnding() {
		introText.enabled = true;
		introText.text = "FINISH!";
		introText.transform.localScale = new Vector3 (2f, 2f, 1f);
		yield return new WaitForSeconds (endingDuration);
		SceneManager.LoadScene ("Match Results");
	}

	void Respawn() {
		foreach (var playerController in GameController.gameState.players) {
			if (GameController.gameState.matchState.CanRespawn (playerController.player.id)) {
				Spawn (playerController);
			}
		}
	}
}
