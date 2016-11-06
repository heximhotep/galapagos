using UnityEngine;
using System.Collections;

public class AnimationDelayerRandomOffset : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void StartAnim () {
		anim.SetBool ("active", true);
	}

	public void OffsetAnim(float offset)
	{
		Invoke ("StartAnim", offset);
	}
}