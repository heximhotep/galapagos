using UnityEngine;
using System.Collections;

public class SoundTriggerController : MonoBehaviour {
	private AudioSource clip;

	void Awake()
	{
		clip = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			clip.Play ();
			GetComponent<Collider> ().enabled = false;
		}
	}
}
