using UnityEngine;
using System.Collections;

public class RevealController : MonoBehaviour {
	public GameObject[] lights;
	public AudioClip switchSound;
	// Use this for initialization
	void Start () {
		StartCoroutine(turnOnLights());
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator turnOnLights() {
		yield return new WaitForSeconds(1f);
		for(int i = 0; i < lights.Length; i++) {
			lights[i].GetComponent<Light>().enabled = true;
			lights[i].GetComponent<AudioSource>().PlayOneShot(switchSound);
			yield return new WaitForSeconds(1f);
		}
	}
}