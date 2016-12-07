using UnityEngine;
using System.Collections;

public class AnimationDelayerRandomOffset : MonoBehaviour {

	private Animator anim;
	private AudioSource audio;
	public AudioClip sucksuck;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		audio.clip = sucksuck;
		audio.maxDistance = 3;
		audio.loop = true;
	}

	// Update is called once per frame
	void StartAnim () {
		anim.SetBool ("active", true);
		audio.Play();
	}

	public void OffsetAnim(float offset)
	{
		Invoke ("StartAnim", offset);
	}
}