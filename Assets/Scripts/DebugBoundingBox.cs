using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DebugLayer {
	public LayerMask layermask;
	public Color color;
}

public class DebugBoundingBox : MonoBehaviour {

	public List<DebugLayer> layersSettings;

	private BoxCollider2D[] allBoxCollider2D;
	private CircleCollider2D circleCollider2D;

	private Material mat;

	// Use this for initialization
	void Start () {	
		allBoxCollider2D = Resources.FindObjectsOfTypeAll<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void OnPostRender () {
		if (!mat) {
			// Unity has a built-in shader that is useful for drawing
			// simple colored things. In this case, we just want to use
			// a blend mode that inverts destination colors.			
			var shader = Shader.Find ("Hidden/Internal-Colored");
			mat = new Material (shader);
			mat.hideFlags = HideFlags.HideAndDontSave;
			// Set blend mode to invert destination colors.
//			mat.SetInt ("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusDstColor);
//			mat.SetInt ("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
			// Turn off backface culling, depth writes, depth test.
			mat.SetInt ("_Cull", (int) UnityEngine.Rendering.CullMode.Off);
			mat.SetInt ("_ZWrite", 0);
			mat.SetInt ("_ZTest", (int) UnityEngine.Rendering.CompareFunction.Always);
		}

		//GL.PushMatrix ();
		//GL.LoadOrtho ();

		// activate the first shader pass (in this case we know it is the only pass)
		mat.SetPass (0);

		foreach (var collider in allBoxCollider2D) {
			if (collider.enabled) {
				var scale = collider.size * 0.5f;
				var points = new Vector3[5];

				points [0] = collider.transform.TransformPoint (new Vector3 (-scale.x + collider.offset.x, scale.y + collider.offset.y, 0));
				points [1] = collider.transform.TransformPoint (new Vector3 (scale.x + collider.offset.x, scale.y + collider.offset.y, 0));
				points [2] = collider.transform.TransformPoint (new Vector3 (scale.x + collider.offset.x, -scale.y + collider.offset.y, 0));
				points [3] = collider.transform.TransformPoint (new Vector3 (-scale.x + collider.offset.x, -scale.y + collider.offset.y, 0));
				points [4] = points [0];
	
				var setting = layersSettings.Find (i => i.layermask.value == (i.layermask.value | (1 << collider.gameObject.layer)));
				if (setting != null) {
					GL.Begin (GL.LINES);
					GL.Color (setting.color);
					foreach (var p in points) {
						GL.Vertex3 (p.x, p.y, p.z);
					}
					GL.End ();

					var quadColor = new Color (setting.color.r, setting.color.g, setting.color.b, 0.7f);
					GL.Begin (GL.QUADS);
					GL.Color (quadColor);
					foreach (var p in points) {
						GL.Vertex3 (p.x, p.y, p.z);
					}
					GL.End ();
				}
			}
		}
	
		//GL.PopMatrix ();
	}
}
