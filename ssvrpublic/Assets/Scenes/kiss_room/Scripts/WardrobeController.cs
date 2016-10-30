using UnityEngine;
using System.Collections;

public class WardrobeController : MonoBehaviour {

	private Animator animator;

	private bool active;

	void Awake()
	{
		active = false;
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (active) {
			if (Input.GetButton ("Fire1")) {
				animator.SetBool ("Open", true);
			}
		} else {
			animator.SetBool ("Open", false);
		}
	}

	public void OnLook()
	{
		active = true;
	}

	public void OffLook()
	{
		active = false;
	}
}
