using UnityEngine;
using System.Collections;

public class ResRoomTransitionController : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		GameController.instance.LoadSolarRoom ();
	}
}
