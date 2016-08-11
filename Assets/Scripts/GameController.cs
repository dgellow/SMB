using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MatchSettings {
	public int nbLives;
	public string levelScene;
}

public class MatchState {
	public LevelStage levelStage;
	public readonly int[] lives;
	public readonly float[] damages;
	private MatchSettings matchSettings;

	public MatchState(PlayerController[] controllers, MatchSettings settings, LevelStage stage) {
		levelStage = stage;
		matchSettings = settings;
		foreach(var controller in controllers) {
			lives [controller.playerId] = settings.nbLives;
			damages [controller.playerId] = 0;
		}
	}

	public void Kill(PlayerController controller)  {
		lives[controller.playerId] -= 1;
		if (lives[controller.playerId] < 0) {
			lives[controller.playerId] = 0;
		}
	}

	public void Damages(float amount, PlayerController from, PlayerController to) {
		if (amount > 0) {
			damages[to.playerId] += amount;
			if (damages[to.playerId] > 200) {
				Kill (to);
			}
		}
	}
}

public class GameController : MonoBehaviour {

	public static GameController gameState;
	public int nbLives;

	[SerializeField]
	private MatchSettings matchSettings;
	[SerializeField]
	private MatchState matchState;

	void Awake() {
		if (gameState == null) {
			DontDestroyOnLoad (gameObject);
			gameState = this;
		} else if (gameState != this) {
			Destroy (gameState);
		}
	}

	public void StartMatch() {
		SceneManager.LoadScene (matchSettings.levelScene);
		var controllers = FindObjectsOfType<PlayerController> ();
		var levelStage = FindObjectOfType<LevelStage> ();
		levelStage.InitialSpawn ();
		matchState = new MatchState (controllers, matchSettings, levelStage);
	}

	public void Kill(PlayerController controller) {
		Debug.Log ("Kill player #" + controller.playerId + ", " + controller.playerName);

	}	
}
