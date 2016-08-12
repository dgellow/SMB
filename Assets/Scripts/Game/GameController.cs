using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using System.Linq;
	
public class GameController : MonoBehaviour {
	public static GameController gameState;

	public MatchRules matchRules;
	public MatchState matchState;
	public readonly int nbPlayers = 4;
	public List<GameObject> playersObjects;
	public List<PlayerController> players;
	public GameObject playerPrefab;

	void Awake() {
		if (gameState == null) {
			DontDestroyOnLoad (gameObject);
			gameState = this;

			// Default match rules
			matchRules = new StockRules ();
		} else if (gameState != this) {
			Destroy (gameState);
		}
	}

	public void StartCharacterSelection() {
		// Delete existing player controllers
		foreach (var playerControllerObject in FindObjectsOfType<PlayerController> ()) {
			Destroy (playerControllerObject.gameObject);
		}
		players.Clear ();

		// Instantiate and register player controllers
		var activePlayers = ReInput.players.GetPlayers ().Where (p => p.isPlaying).OrderBy (x => x.id);
		for (var i = 0; i < activePlayers.Count (); i++) {
			var playerControllerObject = Instantiate (playerPrefab);
			DontDestroyOnLoad (playerControllerObject);
			var playerController = playerControllerObject.GetComponent<PlayerController> ();
			playerController.player = ReInput.players.GetPlayer (i);
			players.Add (playerController);
		}
	}

	public void StartMatch() {
		foreach (var playerController in players) {
			playerController.InstantiateCharacter ();
		}
		matchState = new MatchState (players);
		StartCoroutine (PlayMatchIntro());
	}

	IEnumerator PlayMatchIntro() {
		yield return new WaitForSeconds (1);
	}
}
