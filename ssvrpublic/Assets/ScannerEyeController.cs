using UnityEngine;
using System.Collections;

public class ScannerEyeController : MonoBehaviour {
	[SerializeField]
	private float blinkDelay = 1f;

	private Color startCol;

	void Awake()
	{
		startCol = GetComponent<Renderer> ().materials [1].GetColor ("_EmissionColor");
		EyeTurnOff ();
	}

	void EyeTurnOff()
	{
		Color _newCol = new Color (0, 0, 0);
		GetComponent<Renderer> ().materials [1].SetColor ("_EmissionColor", _newCol);
		Invoke ("EyeTurnOn", blinkDelay);
	}

	void EyeTurnOn()
	{
		GetComponent<Renderer> ().materials [1].SetColor ("_EmissionColor", startCol);
		Invoke ("EyeTurnOff", blinkDelay);
	}
}
