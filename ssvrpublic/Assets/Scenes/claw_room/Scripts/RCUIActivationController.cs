using UnityEngine;
using System.Collections;

public class RCUIActivationController : MonoBehaviour {
	private ControlCanvasController canvasControl;

	void Awake()
	{
		Transform _RCUI = transform.parent.parent.Find ("RC UI");	
		canvasControl = _RCUI.transform.Find ("Control Canvas").GetComponent<ControlCanvasController> ();
		canvasControl.gameObject.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			canvasControl.gameObject.SetActive (true);
			canvasControl.Initiate ();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			canvasControl.Deactivate ();
		}
	}
}
