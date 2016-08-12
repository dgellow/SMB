using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;

[System.Serializable]
public class MatchState {
	public float remainingTime;
	/// <summary>
	/// Players kills. Kills of a specific player can be accessed with `matchState.kills[playerId]`.
	/// </summary>
	public int[] kills;
	/// <summary>
	/// Players stocks. Stock of a specific player can be accessed with `matchState.stocks[playerId]`.
	/// </summary>
	public int[] stocks;
	/// <summary>
	/// Players damages. Damages of a specific player can be accessed with `matchState.damages[playerId]`.
	/// </summary>
	public float[] damages;

	public MatchState(List<PlayerController> players) {
		kills = new int[GameController.gameState.players.Count];
		stocks = new int[GameController.gameState.players.Count];
		damages = new float[GameController.gameState.players.Count];

		foreach (var playerController in players) {
			var playerId = playerController.player.id;
			var matchRules = GameController.gameState.matchRules;

			kills [playerId] = 0;
			damages [playerId] = 0;

			if (matchRules.GetType () == typeof(StockRules)) {
				stocks [playerId] = (matchRules as StockRules).stockLimit;
			} else if (matchRules.GetType () == typeof(TimeRules)) {
				remainingTime = (matchRules as TimeRules).timeLimit;
			}
		}
	}
}
	
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
		for (var i = 0; i < ReInput.players.Players.Count; i++) {
			var playerControllerObject = Instantiate (playerPrefab);
			DontDestroyOnLoad (playerControllerObject);
			var playerController = playerControllerObject.GetComponent<PlayerController> ();
			playerController.playerId = i;
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
