using UnityEngine;
using System.Collections;

public class ApproachTriggerController : MonoBehaviour {

	[SerializeField]
	private KissingController kCon;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
			kCon.TriggerApproach ();
	}
}
