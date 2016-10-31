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

	//Public Methods
	public void Activate(GameObject maincam)
	{
		rcUI.SetActive (true);
		maincam.transform.position = playerOffset.transform.position;
		maincam.transform.parent = playerOffset.transform;
		playerCam = maincam;
	}

	public void Deactivate()
	{		
		if (playerCam != null) {
			rcUI.SetActive (false);
			playerCam.transform.position = playerCamParent.transform.position;
			playerCam.transform.parent = playerCamParent.transform;
			playerCam = null;
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
