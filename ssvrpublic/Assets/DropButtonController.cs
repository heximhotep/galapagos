using UnityEngine;
using System.Collections;

public class DropButtonController : MonoBehaviour {
	public ClawControl claw;

	private bool activated;
	void Awake()
	{
		activated = false;
	}

	void Update()
	{
		if(activated && Input.GetButtonUp("Fire1"))
			claw.OnDrop();
	}

	public void PtrEnter()
	{
		activated = true;
	}

	public void PtrExit()
	{
		activated = false;
	}
}
