using UnityEngine;
using System.Collections;

public class RCActivatorController : MonoBehaviour {

	public PlayerController player;
	public Camera playerCam;
	public RCPlayerController rc;

	private bool inRC, PtrOver;

	void Awake()
	{
		inRC = false;
		PtrOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (PtrOver && Input.GetButtonUp("Fire1")) {
			if (inRC) {
				rc.Deactivate ();
				player.Activate ();
			} else {
				player.Deactivate ();
				rc.Activate (playerCam.gameObject);
			}
		}
	}

	public void PtrEnter()
	{
		PtrOver = true;
	}

	public void PtrExit()
	{
		PtrOver = false;
	}
}
