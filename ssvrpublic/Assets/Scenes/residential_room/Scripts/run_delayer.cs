using UnityEngine;
using System.Collections;

public class run_delayer : MonoBehaviour {

	public float delay;

	private Animator anim;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		Invoke ("StartRun", delay);
	}
	
	// Update is called once per frame
	void StartRun () {
		anim.SetBool ("active", true);
	}
}
