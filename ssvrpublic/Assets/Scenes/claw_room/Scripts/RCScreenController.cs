using UnityEngine;
using System.Collections;

public class RCScreenController : MonoBehaviour {
	[SerializeField]
	private Material cameraMat, droppingMat;

	[SerializeField]
	private float textFlashInterval = 1f;

	private GameObject AlertText;
	private bool dropping;

	void Awake()
	{
		AlertText = transform.Find ("RC UI").Find ("Control Canvas").Find ("Dropping Alert Text").gameObject;
		AlertText.SetActive (false);
		dropping = false;
	}

	public void StartDrop()
	{
		GetComponent<Renderer> ().material = droppingMat;
		dropping = true;
		AlertTextOn ();
	}

	public void StopDrop()
	{
		GetComponent<Renderer> ().material = cameraMat;
		dropping = false;
	}

	void AlertTextOn()
	{
		AlertText.SetActive (true);
		Invoke ("AlertTextOff", textFlashInterval);
	}

	void AlertTextOff()
	{
		AlertText.SetActive (false);
		if (dropping)
			Invoke ("AlertTextOn", textFlashInterval);
	}
}
