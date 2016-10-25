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
	public readonly int[] suicides;
	/// <summary>
	/// Players stocks. Stock of a specific player can be accessed with `matchState.stocks[playerId]`.
	/// </summary>
	public readonly int[] stocks;
	/// <summary>
	/// Players damages. Damages of a specific player can be accessed with `matchState.damages[playerId]`.
	/// </summary>
	public readonly float[] damages;

	public readonly bool[] alive;

	/// <summary>
	/// Player id of the last player who hit the player at a specific id. -1 means that it has been initialized or reset, i.e: if `matchState.lastContact[playerId]` is -1 and the player die it will be counted as a suicide instead of a kill.
	/// </summary>
	public readonly int[] lastContacts;

	public MatchState(List<PlayerController> players) {
		kills = new int[players.Count];
		suicides = new int[players.Count];
		stocks = new int[players.Count];
		damages = new float[players.Count];
		alive = new bool[players.Count];
		lastContacts = new int[players.Count];
		for (var i = 0; i < lastContacts.Length; i++) {
			lastContacts [i] = -1;
		}

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

	public void Hit(PlayerController bully, PlayerController target, float amount) {
		if (bully != null) {
			lastContacts [target.player.id] = bully.player.id;
		} else {
			lastContacts [target.player.id] = -1;
		}
		damages [target.player.id] += amount;
	}

	public void Kill(PlayerController target) {
		var murdererId = lastContacts [target.player.id];
		if (murdererId != -1) {
			kills [murdererId] += 1;
		} else {
			suicides [target.player.id] += 1;
		}

		damages [target.player.id] = 0;
		lastContacts [target.player.id] = -1;

		if (GameController.gameState.matchRules.GetType () == typeof(StockRules)) {
			stocks [target.player.id] -= 1;
			if (stocks [target.player.id] < 0) {
				stocks [target.player.id] = 0;
			}
			alive[target.player.id] = false;
			Object.Destroy(target.character.gameObject);
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