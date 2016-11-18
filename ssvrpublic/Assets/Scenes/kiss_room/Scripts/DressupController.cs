using UnityEngine;
using System.Collections;

public class DressupController : MonoBehaviour {

	private enum dressGaze
	{
		TUX,
		DRESS,
		NONE
	}

	private bool initialTrigger, dropTrigger;
	private dressGaze curGaze, curVisible;
	private Animator rugmouthAnimator;
	private KissingController kissControl;
	private GameObject lover, loverDress, loverTux;

	void Awake()
	{
		initialTrigger = false;
		dropTrigger = false;
		curGaze = dressGaze.NONE;
		curVisible = dressGaze.NONE;
		rugmouthAnimator = GetComponent<Animator> ();
		kissControl = transform.Find ("transport_platform_base").gameObject.GetComponent<KissingController> ();
		lover = transform.Find ("transport_platform_base").Find ("Lover Standing").gameObject;
		loverDress = lover.transform.Find ("lover_standing_dress").gameObject;
		loverTux = lover.transform.Find ("lover_standing_tux").gameObject;
	}

	void Update () {
		if (initialTrigger && Input.GetButtonUp("Fire1")) {
			switch (curGaze) {
			case(dressGaze.DRESS):
				curVisible = dressGaze.DRESS;
				Invoke ("CloseMouth", 1.5f);
				break;
			case(dressGaze.TUX):
				curVisible = dressGaze.TUX;
				Invoke ("CloseMouth", 1.5f);
				break;
			default:
				break;
			}
		}
	}
		
	void CloseMouth()
	{
		rugmouthAnimator.SetBool ("Up", false);
		Invoke ("GulpedDown", 3.0f);
	}

	public void InitialActivate()
	{
		initialTrigger = true;
	}

	public void PtrEnterDress()
	{
		curGaze = dressGaze.DRESS;
	}

	public void PtrEnterTux()
	{
		curGaze = dressGaze.TUX;
	}

	public void PtrExitAll()
	{
		curGaze = dressGaze.NONE;
	}

	public void GulpedDown()
	{
		kissControl.SetClothed ();
		switch (curVisible) {
		case(dressGaze.DRESS):
			loverTux.SetActive (false);
			loverDress.SetActive (true);
			rugmouthAnimator.SetBool ("Up", true);
			break;
		case(dressGaze.TUX):
			loverDress.SetActive (false);
			loverTux.SetActive (true);
			rugmouthAnimator.SetBool ("Up", true);
			break;
		default:
			rugmouthAnimator.SetBool ("Up", true);
			break;
		}
	}
}
