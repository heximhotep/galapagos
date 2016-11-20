using UnityEngine;
using System.Collections;

public class WardrobeOpenTriggerController : MonoBehaviour {

	private WardrobeController grandMaster;

	void Awake()
	{
		grandMaster = GetComponentInParent<WardrobeController> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
			grandMaster.Activate ();
	}
}
