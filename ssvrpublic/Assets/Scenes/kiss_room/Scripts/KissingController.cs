using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class KissingController : MonoBehaviour, DoorCondition {

	[SerializeField]
	private GameObject player;

	[SerializeField]
	private Animator loverAnimator;

	[SerializeField]
	private WardrobeController wardrobeControl;

	[SerializeField]
	private LoverController loverControl;

	[SerializeField]
	private float kissTime;

	[SerializeField]
	private float kissDistance;

	private GameObject cam, lover;
	private Vector3 startPosition, targetPosition;
	private Vector3 startRotation, targetRotation;

	private float initialMoveSpeed = 0;
	private bool kissEnabled = false;
	private bool kissComplete = false;

	public enum KissState{
		INITIAL,
		CLOTHED,
		APPROACH,
		INTIMATE,
		KISSING,
		FINAL
	};

	public KissState curState = KissState.INITIAL;

	void Awake()
	{
		cam = player.transform.Find ("Camera Offset").Find ("MainCamera").gameObject;
		lover = transform.Find ("Lover Standing").gameObject;
		startPosition = lover.transform.position;
		targetPosition = startPosition;
		startRotation = lover.transform.rotation.eulerAngles;
		targetRotation = startRotation;
	}

	void FixedUpdate()
	{
		Vector3 _curPos = lover.transform.position;
		Vector3 _curRot = lover.transform.rotation.eulerAngles;

		lover.transform.position = _curPos + new Vector3 (
			Mathf.Clamp (targetPosition.x - _curPos.x, -0.01f, 0.01f),
			0,
			Mathf.Clamp (targetPosition.z - _curPos.z, -0.01f, 0.01f)
		);

		Vector3 _newRot = new Vector3 (
			                  0,
			                  Mathf.Clamp (targetRotation.y - _curRot.y, -0.5f, 0.5f),
			                  Mathf.Clamp (targetRotation.z - _curRot.z, -0.5f, 0.5f)
		                  );

		lover.transform.Rotate(_newRot);
	}

	void Update()
	{
		switch (curState) 
		{
		case(KissState.INITIAL):
			break;
		case(KissState.CLOTHED):
			break;
		case(KissState.APPROACH):
			break;
		case(KissState.INTIMATE):
			Invoke ("EndKiss", kissTime);
			break;
		case(KissState.FINAL):
			loverControl.Rigidify ();
			break;
		}
	}

	public void SetClothed()
	{
		curState = KissState.CLOTHED;
		wardrobeControl.Disable ();
	}

	public KissState GetState()
	{
		return curState;
	}

	public void TriggerApproach()
	{
		if (curState == KissState.CLOTHED) 
		{
			initialMoveSpeed = player.GetComponent<PlayerController> ().moveSpeed;
			player.GetComponent<PlayerController> ().moveSpeed = 0.5f;
			curState = KissState.APPROACH;
			EnableKiss ();
		}
	}

	public void TriggerKiss()
	{
		if (kissEnabled && curState == KissState.APPROACH) 
		{
			player.GetComponent<PlayerController> ().stopMovement ();
			curState = KissState.INTIMATE;
			StepToKiss ();
		}
	}

	public bool KissComplete()
	{
		return kissComplete;
	}

	public bool CanOpen()
	{
		return curState == KissState.FINAL;
	}

	void EnableKiss()
	{
		kissEnabled = true;
	}

	void StepToKiss()
	{
		targetPosition = Vector3.Lerp (transform.position, player.transform.position, kissDistance);
		loverAnimator.SetBool ("IsKissing", true);
	}

	void EndKiss()
	{
		kissComplete = true;
		player.GetComponent<PlayerController> ().startMovement ();
		player.GetComponent<PlayerController> ().moveSpeed = initialMoveSpeed;
		curState = KissState.FINAL;
	}
}
