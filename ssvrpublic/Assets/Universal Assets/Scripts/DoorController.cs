using UnityEngine;
using AssemblyCSharp;

public class DoorController : MonoBehaviour {

	private Animator doorAnimator;


	void Awake()
	{
		doorAnimator = GetComponent<Animator> ();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
			doorAnimator.SetBool ("Open", true);
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			doorAnimator.SetBool ("Open", false);
	}
}