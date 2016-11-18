using UnityEngine;

public class ClawRoomTransitionController : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		GameController.instance.LoadKissRoom ();
	}
}
