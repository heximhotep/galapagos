using UnityEngine;
using System.Collections;

public class RugmouthInitialTriggerController : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
			GetComponentInParent<DressupController> ().InitialActivate ();
	}
}
