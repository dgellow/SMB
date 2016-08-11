using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class StageSelector : MonoBehaviour, ISelectHandler {

	public string scene;
	public string name;
	public Sprite preview;

	private Image imagePreview;
	private Text namePreview;

	void Start() {
		var imagePreviewObject = GameObject.Find ("PreviewImage");
		if (imagePreviewObject != null) {
			imagePreview = imagePreviewObject.GetComponent<Image> ();
		}

		var namePreviewObject = GameObject.Find ("PreviewName");
		if (namePreviewObject != null) {
			namePreview = namePreviewObject.GetComponent<Text> ();
		}

		var button = GetComponent<Button> ();
		if (button) {
			button.onClick.AddListener (delegate {
				SelectStage ();
			});
		}
	}

	void SelectStage() {
		SceneManager.LoadScene (scene);
	}

	#region ISelectHandler implementation

	public void OnSelect (BaseEventData eventData) {
		if (imagePreview) {
			imagePreview.sprite = preview;
		}

		if (namePreview) {
			namePreview.text = name;
		}
	}

	#endregion
}
