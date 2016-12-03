using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WardrobeController : MonoBehaviour {

	private Animator animator;

	private bool active, disabled;

	void Awake()
	{
		disabled = false;
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
		if(!disabled)
			active = true;
	}

	public void Deactivate()
	{
		active = false;
	}

	public void Disable()
	{
		disabled = true;
		active = false;
	}


}
