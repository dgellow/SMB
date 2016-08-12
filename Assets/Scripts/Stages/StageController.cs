using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageController : MonoBehaviour {

	public Transform[] spawnPoints;
	public Transform[] startingBlocks;
	public float introReadyDuration = 5;
	public float introStartDuration = 2;

	private Canvas canvas;
	private Text introText;
	private PlayerInfo[] playersInfo;

	void Start() {
		canvas = GameObject.Find ("StageUI").GetComponent<Canvas> ();
		var camera = FindObjectOfType<Camera> ();
		canvas.worldCamera = camera;
		introText = GameObject.Find ("IntroText").GetComponent<Text> ();

		playersInfo = FindObjectsOfType<PlayerInfo> ();
		for(var i = 0; i < GameController.gameState.players.Count; i++) {
			var playerController = GameController.gameState.players [i];
			var playerInfo = playersInfo [i];
			//playerInfo.playerId = playerController.player.id;
		}

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

	public void Spawn(PlayerController controller) {
		var spawnPoint = spawnPoints.GetRandomValue ();
		controller.transform.position = spawnPoint.position;
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
}
