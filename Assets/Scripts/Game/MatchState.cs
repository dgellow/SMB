using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MatchState {
	public readonly float remainingTime;
	/// <summary>
	/// Players kills. Kills of a specific player can be accessed with `matchState.kills[playerId]`.
	/// </summary>
	public readonly int[] kills;
	/// <summary>
	/// Players stocks. Stock of a specific player can be accessed with `matchState.stocks[playerId]`.
	/// </summary>
	public readonly int[] stocks;
	/// <summary>
	/// Players damages. Damages of a specific player can be accessed with `matchState.damages[playerId]`.
	/// </summary>
	public readonly float[] damages;

	public readonly bool[] alive;

	public MatchState(List<PlayerController> players) {
		kills = new int[GameController.gameState.players.Count];
		stocks = new int[GameController.gameState.players.Count];
		damages = new float[GameController.gameState.players.Count];
		alive = new bool[GameController.gameState.players.Count];

		foreach (var playerController in players) {
			var playerId = playerController.player.id;
			var matchRules = GameController.gameState.matchRules;

			kills [playerId] = 0;
			damages [playerId] = 0;
			alive [playerId] = true;

			if (matchRules.GetType () == typeof(StockRules)) {
				stocks [playerId] = (matchRules as StockRules).stockLimit;
			} else if (matchRules.GetType () == typeof(TimeRules)) {
				remainingTime = (matchRules as TimeRules).timeLimit;
			}
		}
	}

	public void Kill(PlayerController playerController) {
		if (GameController.gameState.matchRules.GetType () == typeof(StockRules)) {
			stocks [playerController.player.id] -= 1;
			if (stocks [playerController.player.id] < 0) {
				stocks [playerController.player.id] = 0;
			}
			alive[playerController.player.id] = false;
			Object.Destroy(playerController.character.gameObject);
		}
	}

	public void Revive(PlayerController playerController) {
		alive [playerController.player.id] = true;
	}

	public bool CanRespawn(int playerId) {
		if (!alive [playerId]) {
			if (GameController.gameState.matchRules.GetType () == typeof(StockRules) && stocks[playerId] > 0) {
				return true;
			} else {
				return true;
			}
		}
			
		return false;
	}
}