using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {

	public AnimationCurve curve;
	private Text text;

	void Start () {
		text = GetComponent<Text> ();
	}
	
	void Update () {
		var value = curve.Evaluate (Time.time);
		var newColor = new Color (text.color.r, text.color.g, text.color.b, value);
		text.color = newColor;
	}
}
