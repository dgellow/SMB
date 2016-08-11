using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public EventSystem eventSystem;
	private GameObject selectedItem;

	void Start() {
		selectedItem = eventSystem.firstSelectedGameObject;
	}

	void Update() {
		if (eventSystem.currentSelectedGameObject != selectedItem) {
			if (eventSystem.currentSelectedGameObject == null) {
				eventSystem.SetSelectedGameObject (selectedItem);
			} else {
				selectedItem = eventSystem.currentSelectedGameObject;	
			}
		}
	}

	public void LoadScene (string sceneName) {
		SceneManager.LoadScene (sceneName);
	}

	public void Exit () {
		Application.Quit ();
	}

	public void SelectCharacter(GameObject characterObject, string nextScene) {
		LoadScene (nextScene);
	}
}