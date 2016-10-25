using UnityEngine;
using System.Collections;
using Rewired;
using System.Linq;
using UnityEngine.SceneManagement;

public enum CharacterSelectionState {
	Empty,
	Selecting,
	Selected,
	Ready
}

public class CharacterSelectionController : MonoBehaviour {
	public int widthCharactersPool = 4;
	public string previousScene;
	public float inputLatency = 0.30f;

	[SerializeField]
	private Vector2[] positions;
	private CharacterSelector[] selectors;
	private PlayerSelection[] selections;
	[SerializeField]
	private float[] latencies;

	void Start () {
		positions = new Vector2[GameController.gameState.nbPlayers];
		latencies = new float[GameController.gameState.nbPlayers];
		selectors = FindObjectsOfType<CharacterSelector> ();
		selections = FindObjectsOfType<PlayerSelection> ().OrderBy (x => x.playerId).ToArray ();
	}

	void Update () {
		foreach (var playerController in GameController.gameState.players) {
			var selection = selections [playerController.player.id];

			switch (selection.state) {
			case CharacterSelectionState.Empty:
				if (playerController.player.GetButtonDown ("A")) {
					selection.state = CharacterSelectionState.Selecting;
					playerController.player.isPlaying = true;
				} else if (playerController.player.GetButtonTimedPressDown ("B", 4)) {
					SceneManager.LoadScene (previousScene);
				} else {
					playerController.player.isPlaying = false;
				}
				break;
			case CharacterSelectionState.Selecting:
				var position = positions [playerController.player.id];
				var maxX = widthCharactersPool - 1;
				var maxY = (selectors.Length / widthCharactersPool) - 1;
				var latency = playerController.player.GetAxisTimeActive ("Control Stick Horizontal");
				if (latency > inputLatency && latencies [playerController.player.id] == 0) {
					latencies [playerController.player.id] = latency;
				}

				if (latencies [playerController.player.id] != 0) {
					latencies [playerController.player.id] = 0;

					// Handle horizontal moves
					var hAxis = playerController.player.GetAxis ("Control Stick Horizontal");
					if (hAxis < 0) {
						position.x -= 1;
						position.x = position.x < 0 ? 0 : position.x;
					} else if (hAxis > 0) {
						position.x += 1;
						position.x = position.x > maxX ? maxX : position.x;
					}
				

					// Handle vertical moves
					if (playerController.player.GetAxisTimeActive ("Control Stick Vertical") > inputLatency) {
						var vAxis = playerController.player.GetAxis ("Control Stick Vertical");
						if (vAxis < 0) {
							position.y -= 1;
							position.y = position.y < 0 ? 0 : position.y;
						} else if (vAxis > 0) {
							position.y += 1;
							position.y = position.y > maxY ? maxY : position.y;
						}
					}
				}

				positions [playerController.player.id] = position;
		
				// Handle ok/back actions
				if (playerController.player.GetButtonDown ("A")) {
					var selector = selectors [(maxX * (int) position.y) + (int) position.x];
					playerController.characterPrefab = selector.characterPrefab;
					selection.state = CharacterSelectionState.Selected;
				} else if (playerController.player.GetButtonDown ("B")) {
					selection.state = CharacterSelectionState.Empty;
				}
				break;
			case CharacterSelectionState.Selected:
				if (playerController.player.GetButtonDown ("Start")) {
					selection.state = CharacterSelectionState.Ready;
				} else if (playerController.player.GetButtonDown ("B")) {
					playerController.characterPrefab = null;
					selection.state = CharacterSelectionState.Selecting;
				}
				break;
			case CharacterSelectionState.Ready:
				if (playerController.player.GetButtonDown ("B")) {
					selection.state = CharacterSelectionState.Selected;
				}
				break;
			default:
				throw new System.ArgumentOutOfRangeException ();
			}	
		}
	}
}
