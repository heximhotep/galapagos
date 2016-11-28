using UnityEngine;
using System.Collections;

public class ApproachTriggerController : MonoBehaviour {

	[SerializeField]
	private KissingController kCon;

	private bool disable;

	void Awake()
	{
		disable = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!disable && other.gameObject.CompareTag ("Player") && kCon.GetState() == KissingController.KissState.CLOTHED) 
		{
			disable = true;
			kCon.TriggerApproach ();
		}
		
	}
}
