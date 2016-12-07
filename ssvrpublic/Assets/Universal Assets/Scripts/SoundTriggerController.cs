using UnityEngine;
using System.Collections;

public class SoundTriggerController : MonoBehaviour {
	[SerializeField]
	private int clipIndex = 0;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			GameController.instance.voice.RequestPlayClip (clipIndex);
			GetComponent<Collider> ().enabled = false;
		}
	}
}
