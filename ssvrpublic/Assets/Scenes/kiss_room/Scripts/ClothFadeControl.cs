using UnityEngine;
using System.Collections;

public class ClothFadeControl : MonoBehaviour {

	private bool fading;
	private float fadeAmt;

	// Use this for initialization
	void Awake () {
		fading = false;
		fadeAmt = 1.0f;
	}
	
	void Update()
	{
		if (fading) {
			if (fadeAmt > float.Epsilon) {
				Material thisMat = GetComponent<Renderer> ().material;
				fadeAmt = Mathf.Max (fadeAmt - 0.005f, 0);
				Color newCol = new Color (thisMat.color.r, thisMat.color.g, thisMat.color.b, fadeAmt);
				GetComponent<Renderer> ().material.color = newCol;
			} else {
				Destroy (gameObject);
			}
		}
	}

	public void StartFade()
	{
		fading = true;
	}
}
