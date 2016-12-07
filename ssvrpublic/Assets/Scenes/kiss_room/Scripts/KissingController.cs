﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class KissingController : MonoBehaviour, DoorCondition {

	private AudioSource speaker;

	[SerializeField]
	private AudioClip initialKissDialogue, kissInterruptDialogue, kissEndDialogue;

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
	private float kissAngle;

	[SerializeField]
	private float kissDistance;

	[SerializeField]
	private ClothFadeControl dressFade, tuxFade;

	private GameObject cam, lover, loverHead;
	private Vector3 startPosition, targetPosition;
	private Vector3 startRotation, targetRotation;

	private float initialMoveSpeed = 0;
	private float curKissTime;
	private bool IntimateEnabled = false;
	private bool kissEnabled = false;
	private bool kissComplete = false;
	private bool fadeEnabled = false;
	private bool kissInterrupts = false;

	private AudioSource audio;
	public AudioClip kissing;
	public AudioClip fade;
	public AudioClip get_clothed;

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
		speaker = GetComponent<AudioSource> ();
		cam = player.transform.Find ("Camera Offset").Find ("MainCamera").gameObject;
		lover = transform.Find ("Lover Standing").gameObject;
		loverHead = lover.transform.Find ("lover_standing_base").Find ("generic_head").Find ("lover_head_2").gameObject;
		startPosition = lover.transform.position;
		targetPosition = startPosition;
		startRotation = lover.transform.rotation.eulerAngles;
		targetRotation = startRotation;
		curKissTime = kissTime;

		audio = GetComponent<AudioSource> ();
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
//			Invoke ("EndKiss", kissTime);
			if (kissEnabled && Input.GetButton ("Fire1"))
				StartKiss ();
			break;
		case(KissState.KISSING):
			if (!audio.isPlaying) {
				if (audio.clip != kissing)
					audio.clip = kissing;
				audio.Play();
			}
			if (KissCheck ()) {
				player.GetComponent<PlayerController> ().KissInterruptEnd ();
				curKissTime -= Time.deltaTime;
			} else {
				curKissTime = kissTime;
				if (!GameController.instance.voice.IsPlaying () && !kissInterrupts) {
					int clipIndex = 22 + (int)Random.Range (0, 2 - float.Epsilon);
					GameController.instance.voice.RequestPlayClip (clipIndex);
					kissInterrupts = true;
					Invoke ("NewInterrupts", 2f);
				}
			player.GetComponent<PlayerController> ().KissInterruptStart (0.2f);
			}
			if (curKissTime <= 0) {
				EndKiss ();
			}
			break;
		case(KissState.FINAL):
			if (!fadeEnabled && tuxFade.gameObject.activeInHierarchy) {
				tuxFade.StartFade ();
				fadeEnabled = true;
				audio.Stop();
				audio.clip = fade;
				audio.Play();
			}
			if (!fadeEnabled && dressFade.gameObject.activeInHierarchy && !fadeEnabled) {
				dressFade.StartFade ();
				fadeEnabled = true;
				audio.Stop();
				audio.clip = fade;
				audio.Play();
			}
			loverControl.Rigidify ();
			break;
		}
	}

	bool KissCheck()
	{
		Vector3 _camEye = cam.transform.forward;
		Vector3 _idealEye = (loverHead.transform.position - cam.transform.position).normalized;
		return (Vector3.Angle (_camEye, _idealEye) <= kissAngle);
	}

	void NewInterrupts()
	{
		kissInterrupts = false;
	}

	public void SetClothed()
	{
		audio.Stop();
		audio.clip = get_clothed;
		audio.Play();
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
			GameController.instance.voice.RequestPlayClip (21);
			IntimateEnabled = true;
		}
	}

	public void TriggerIntimate()
	{
		if (IntimateEnabled && curState == KissState.APPROACH) 
		{
			player.GetComponent<PlayerController> ().stopMovementHARD ();
			curState = KissState.INTIMATE;
			StepToKiss ();
		}
	}

	public void OnLoverFaceEnter()
	{
		if (curState == KissState.INTIMATE)
			kissEnabled = true;
	}

	public void OnLoverFaceExit()
	{
		if (curState == KissState.INTIMATE)
			kissEnabled = false;
	}

	public bool KissComplete()
	{
		audio.Stop();
		return kissComplete;
	}

	public bool CanOpen()
	{
		return curState == KissState.FINAL;
	}


	void StepToKiss()
	{
		targetPosition = Vector3.Lerp (transform.position, player.transform.position, kissDistance);
	}

	void StartKiss()
	{
		curState = KissState.KISSING;
		loverAnimator.SetBool ("IsKissing", true);
	}

	void EndKiss()
	{
		kissComplete = true;
		player.GetComponent<PlayerController> ().startMovementHARD ();
		player.GetComponent<PlayerController> ().moveSpeed = initialMoveSpeed;
		curState = KissState.FINAL;
	}
}
