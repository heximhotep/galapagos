using UnityEngine;
using System.Collections;

public class KissTriggerController : MonoBehaviour {
	[SerializeField]
	private KissingController kCon;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
			kCon.TriggerKiss ();
	}
}
