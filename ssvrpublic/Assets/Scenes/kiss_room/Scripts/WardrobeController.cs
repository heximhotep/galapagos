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
				animator.SetBool ("Active", true);
			}
		} else {
			animator.SetBool ("Active", false);
		}
	}

	public void Activate()
	{
		active = true;
	}

	public void Deactivate()
	{
		active = false;
	}
}
