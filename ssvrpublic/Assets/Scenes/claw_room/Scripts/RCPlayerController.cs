using UnityEngine;
using System.Collections;

public class RCPlayerController : MonoBehaviour {

	public GameObject playerCamParent;
	public GameObject rcUI;
	public ClawControl clawC;
	public GameObject playerOffset;
	public float refractionTime;


	private enum OnButton{
		LEFT,
		RIGHT,
		UP,
		DOWN,
		DROP,
		EXIT,
		NONE
	}
		
	private GameObject playerCam;
	private OnButton gazedBtn;

	//Private Methods

	void Awake()
	{
		gazedBtn = OnButton.NONE;
	}

	void Update()
	{ 
		if (Input.GetButton ("Fire1")) {
			switch (gazedBtn) {
			case(OnButton.LEFT):
				clawC.OnLeft ();
				break;
			case(OnButton.UP):
				clawC.OnTop ();
				break;
			case(OnButton.RIGHT):
				clawC.OnRight ();
				break;
			case(OnButton.DOWN):
				clawC.OnBottom ();
				break;
			case(OnButton.EXIT):
				Invoke ("Deactivate", 0.5f);
				break;
			}
		} else {
			clawC.OffAll ();
		}
	}

	public void EnterLeft()
	{
		gazedBtn = OnButton.LEFT;
	}

	public void EnterRight()
	{
		gazedBtn = OnButton.RIGHT;
	}

	public void EnterUp()
	{
		gazedBtn = OnButton.UP;
	}

	public void EnterDown()
	{
		gazedBtn = OnButton.DOWN;
	}

	public void EnterExit()
	{
		gazedBtn = OnButton.EXIT;
	}

	public void ExitAll()
	{
		gazedBtn = OnButton.NONE;
	}
}
