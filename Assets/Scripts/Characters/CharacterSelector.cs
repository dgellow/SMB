using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelector : MonoBehaviour, ISelectHandler {

	public string name;
	public GameObject characterPrefab;
	public Vector3 characterPosition;
	public Vector3 characterScale;

	private Text nameLabel; 
	private GameObject characterInstance;

	void Start() {
		nameLabel = GetComponentInChildren<Text> ();
		if (nameLabel != null) {
			nameLabel.text = name;
		}

		characterInstance = Instantiate (characterPrefab, transform) as GameObject;
		if (characterInstance != null) {
			characterInstance.transform.localPosition = characterPosition;
			characterInstance.transform.localScale = characterScale;
		}
	}
		
	#region ISelectHandler implementation
	public void OnSelect (BaseEventData eventData) {
		foreach (var playerController in GameController.gameState.players) {
			playerController.characterPrefab = (eventData.selectedObject.GetComponent<CharacterSelector> ()).characterPrefab;
		}
	}
	#endregion
}
