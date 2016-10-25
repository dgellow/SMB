using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Rewired;

public class PlayerSelection : MonoBehaviour {
	public int playerId;
	public Text nameText;
	public Text stateText;
	public SpriteRenderer token;
	public Vector3 tokenOffset;
	public CharacterSelectionState state;

	private PlayerController playerController;
	private GameObject characterObject;

	void Start () {
		playerController = GameController.gameState.players [playerId];
		nameText.text = playerController.player.name;
		token.enabled = false;
	}
		
	void OnGUI() {
		switch (state) {
		case CharacterSelectionState.Empty:
			stateText.text = "Press A to join";
			token.enabled = false;
			break;
		case CharacterSelectionState.Selecting:
			token.enabled = true;
			token.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
			if (characterObject != null) {
				Destroy (characterObject);
			}
			stateText.text = "Press A to select";
			break;
		case CharacterSelectionState.Selected:
			if (characterObject == null) {
				characterObject = Instantiate (playerController.characterPrefab, transform) as GameObject;
				characterObject.transform.localPosition = new Vector3(150, 10, transform.position.z - 10);
				characterObject.transform.localScale = new Vector3 (20, 20, 1);
			}
			stateText.text = "Press Start when ready!";
			break;
		case CharacterSelectionState.Ready:
			stateText.text = "READY";
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
	}
}
