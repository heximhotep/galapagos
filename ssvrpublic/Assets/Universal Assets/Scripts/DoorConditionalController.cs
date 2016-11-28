using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class DoorConditionalController : MonoBehaviour {

	[SerializeField]
	private GameObject Lock;

	private DoorCondition LockPiece;
	private Animator doorAnimator;

	void Awake()
	{
		doorAnimator = GetComponent<Animator> ();
		LockPiece = Lock.GetComponent<DoorCondition> ();
	}
	void OnTriggerEnter(Collider other)
	{
		if (LockPiece.CanOpen() && other.gameObject.CompareTag ("Player"))
			doorAnimator.SetBool ("Open", true);
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			doorAnimator.SetBool ("Open", false);
	}
}
