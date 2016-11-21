using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;

public class ControlCanvasController : MonoBehaviour {
	private float opacity, targetOpacity;
	private bool initialized = false;
	private bool deactivating = false;

	[SerializeField]
	private Image[] UIElems;

	[SerializeField]
	private float fadeRate = 0.5f;

	public void Initiate()
	{
		opacity = 0f;
		targetOpacity = 100f;
		initialized = true;
		foreach (Image elem in UIElems) {
			Color newCol = elem.color;
			newCol.a = opacity;
			elem.color = newCol;
		}
	}

	public void Deactivate()
	{
		targetOpacity = 0f;
		deactivating = true;
	}

	void Update()
	{
		if (initialized && Mathf.Abs(targetOpacity - opacity) > float.Epsilon) {
			opacity = Mathf.Clamp (targetOpacity, opacity - fadeRate, opacity + fadeRate);
			foreach (Image elem in UIElems) {                   
				Color newCol = elem.color;
				newCol.a = opacity;
				elem.color = newCol;
			}
		} else {
			if (deactivating) 
			{
				gameObject.SetActive (false);
			}
		}
	}
}
