using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {
	public int playerId;
	public Text nameText;
	public Text damagesText;
	public Text stockText;

	void OnGUI() {
		nameText.text = GameController.gameState.players [playerId].playerName;
		var damages = GameController.gameState.matchState.damages [playerId];
		damagesText.text = damages + " %";

		if (GameController.gameState.matchRules.GetType () == typeof(StockRules)) {
			var text = "";
			for(var i = 0; i < GameController.gameState.matchState.stocks [playerId]; i++) {
				text += "💖";
			}
			stockText.text = text;
		}
	}
}