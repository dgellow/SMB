using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
	public string nextScene;

	void Update () {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene (nextScene);	
		}
	}
}
