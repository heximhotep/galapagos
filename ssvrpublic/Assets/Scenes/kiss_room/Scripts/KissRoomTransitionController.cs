using UnityEngine;

public class KissRoomTransitionController : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		GameController.instance.LoadEndRoom ();
	}

}
