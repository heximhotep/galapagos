using UnityEngine;
using System.Collections;

public class SolarRoomTransitionController : MonoBehaviour {
	void OnTriggerEnter(Collider other)
	{
		GameController.instance.LoadClawRoom ();
	}
}
