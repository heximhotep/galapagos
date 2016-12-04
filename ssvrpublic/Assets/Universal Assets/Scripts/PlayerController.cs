using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {
	public Camera maincam;

	public float moveSpeed;

	private PlayerCanvasControl canvasControl;

	private Vector3 shakeOffset;
	private float shakeAmt;

	[SerializeField]
	private Color flashCol;

	[SerializeField]
	private float flashInterval = 0.5f;

	private bool canMove, canMoveHard, kissInterrupt;

	// Use this for initialization
	void Awake () 
	{
		canMove = true;
		canMoveHard = true;
		kissInterrupt = false;
		canvasControl = transform.Find ("Camera Offset").Find ("MainCamera").Find ("PlayerOverlayCanvas").Find("Panel").GetComponent<PlayerCanvasControl> ();
		Color _flashColClear = new Color (flashCol.r, flashCol.g, flashCol.b, 0);
		canvasControl.shiftColor (_flashColClear, 1f);
		canvasControl.GetComponent<Image> ().color = _flashColClear;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canMove && Input.GetButton("Fire1"))
		{
			Vector3 forward = maincam.transform.forward;
			transform.position = transform.position + new Vector3(forward.x, 0f, forward.z) * moveSpeed * Time.deltaTime;
		}

		if (Input.GetKeyUp (KeyCode.Space))
			Application.CaptureScreenshot ("kissroompreview.png");
	}

	void FlashScreenOn()
	{
		canvasControl.shiftColor(flashCol, flashInterval);
		Invoke ("FlashScreenOff", flashInterval);
	}

	void FlashScreenOff()
	{
		Color _fadedCol = new Color (flashCol.r, flashCol.g, flashCol.b, 0);
		canvasControl.shiftColor (_fadedCol, flashInterval);
		if (kissInterrupt)
			Invoke ("FlashScreenOn", flashInterval);
	}

	public void Deactivate()
	{
		maincam.tag = "Untagged";
	}

	public void Activate()
	{
		maincam.tag = "MainCamera";
	}

	public void stopMovement()
	{
		canMove = false;
	}

	public void stopMovementHARD()
	{
		canMoveHard = false;
		canMove = false;
	}

	public void startMovementHARD()
	{
		canMoveHard = true;
		canMove = true;
	}

	public void startMovement()
	{
		if(canMoveHard)
			canMove = true;
	}

	public void KissInterruptStart(float shakeAmt)
	{
		if (!kissInterrupt) 
		{
			kissInterrupt = true;
			FlashScreenOn ();
		}
	}

	public void KissInterruptEnd()
	{
		kissInterrupt = false;
	}
}
