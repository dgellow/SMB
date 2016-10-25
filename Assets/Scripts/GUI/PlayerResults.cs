using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerResults : MonoBehaviour {
	public Text nameText;
	public Text killsText;
	public Text stocksText;
	public int playerId;

	void Start() {
		var rules = GameController.gameState.matchRules as IMatchRules;
		if (rules.GetWinnerId () == playerId) {
			transform.localScale = new Vector3 (1.2f, 1.2f, 1f);
		}
		if (playerId < GameController.gameState.players.Count) {
			var playerController = GameController.gameState.players [playerId];
			nameText.text = playerController.player.name;
			killsText.text = GameController.gameState.matchState.kills [playerId].ToString ();
			stocksText.text = GameController.gameState.matchState.stocks [playerId].ToString ();
		} else {
			nameText.text = "";
			killsText.text = "-";
			stocksText.text = "-";
		}
	}
}
