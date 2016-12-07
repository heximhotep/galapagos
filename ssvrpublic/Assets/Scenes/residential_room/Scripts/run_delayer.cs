using UnityEngine;
using System.Collections;

public class run_delayer : MonoBehaviour {

	public float delay;

	private Animator anim;
	private AudioSource audio;
	public AudioClip tredmell_1;
	public AudioClip tredmell_2;
	public AudioClip tredmell_3;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		Invoke ("StartRun", delay);
		audio = GetComponent<AudioSource> ();
		Invoke ("PlaySound", delay);
	}
	
	void PlaySound() {
		if (!audio.isPlaying) {
			int random = Random.Range(0, 3);
			if (random == 0)
				audio.clip = tredmell_1;
			else if (random == 1)
				audio.clip = tredmell_2;
			else if (random == 2)
				audio.clip = tredmell_3;
			audio.Play();
		}
	}

	// Update is called once per frame
	void StartRun () {
		anim.SetBool ("active", true);
	}

	void Update () {
		Invoke ("PlaySound", delay);
	}
}
