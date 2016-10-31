using UnityEngine;
using System.Collections;


public class ClawControl : MonoBehaviour {

	enum controllerState{
		OFF_ALL,
		ON_TOP,
		ON_LEFT,
		ON_RIGHT,
		ON_BOTTOM
	}

	enum dropBtnState{
		LIVE, OFFLINE
	}

	enum limbDeliveryState{
		SEARCHING,
		LIFTING,
		ROTATING_UP,
		PUSHING,
		RETRACTING,
		ROTATING_DOWN
	}

	public GameObject claw;
	public GameObject nozzle;
	public GameObject horizontalBeam;
	public GameObject verticalBeam;
	public LimbLightController limbLock;

	private LineRenderer rope;
	private float beamRotationY, beamRotationX, targetRotationX, targetRotationY,
		clawExtension, targetExtension,
		clawHeight, currentDrop, targetDrop, restHeight, bottomHeight,
		clawXStart, clawZStart;
	private float rotationStateX, rotationStateY;
	private controllerState cState;
	private dropBtnState dropState;
	private limbDeliveryState deliveryState;
	private bool liftingLimb;

	private Transform grabbedLimb;
	private Ray checkGrab;

	private Vector3 limbOffset;

	public float clawRange, armLength;

	public void Awake()
	{
		cState = controllerState.OFF_ALL;
		dropState = dropBtnState.OFFLINE;
		deliveryState = limbDeliveryState.SEARCHING;
		liftingLimb = false;
		clawXStart = claw.transform.localPosition.x;
		clawZStart = claw.transform.localPosition.z;
		rope = GetComponent<LineRenderer> ();
		beamRotationY = 0f;
		beamRotationX = 180f;
		targetRotationX = 0f;
		targetRotationY = 0f;
		clawExtension = 0f;
		targetExtension = 0f;
		rotationStateX = 0f;
		rotationStateY = 0f;
		clawHeight = claw.transform.localPosition.y;
		restHeight = clawHeight;
		bottomHeight = clawHeight + armLength;
		currentDrop = 0.0f;
		targetDrop = 0.0f;

	}

	public void OnTop()
	{
		cState = controllerState.ON_TOP;
	}

	public void OnLeft()
	{
		cState = controllerState.ON_LEFT;
	}

	public void OnRight()
	{
		cState = controllerState.ON_RIGHT;
	}

	public void OnBottom()
	{
		cState = controllerState.ON_BOTTOM;
	}

	public void OffDrop()
	{
		dropState = dropBtnState.OFFLINE;
	}

	public void OffAll()
	{
		cState = controllerState.OFF_ALL;
	}

	public void OnDrop()
	{
		dropState = dropBtnState.LIVE;
	}

	public void FixedUpdate()
	{
		horizontalBeam.transform.eulerAngles = new Vector3 (-beamRotationX, beamRotationY, 0);
		verticalBeam.transform.localPosition = new Vector3 (clawRange * clawExtension, 0, 0.1f);
		claw.transform.localPosition = new Vector3(clawXStart, restHeight + (bottomHeight - restHeight) * currentDrop, clawZStart);
		rope.SetPosition (0, nozzle.transform.position);
		rope.SetPosition (1, claw.transform.position);
		if (grabbedLimb != null && liftingLimb) {
			grabbedLimb.transform.position = claw.transform.position + limbOffset;
		}
	}

	public void Update()
	{
		switch (deliveryState) {
		case(limbDeliveryState.SEARCHING):
			switch (cState) {
			case(controllerState.ON_TOP):
				targetExtension = Mathf.Clamp (targetExtension + 0.01f, -0.5f, 0.5f);
				break;
			case(controllerState.ON_LEFT):
				rotationStateY -= 0.02f;
				break;
			case(controllerState.ON_RIGHT):
				rotationStateY += 0.02f;
				break;
			case(controllerState.ON_BOTTOM):
				targetExtension = Mathf.Clamp (targetExtension - 0.01f, -0.5f, 0.5f);
				break;
			}

			if (dropState == dropBtnState.LIVE) {
				if (targetDrop == 0) {
					RaycastHit rayInfo = new RaycastHit ();
					Physics.Raycast (new Ray (claw.transform.position + new Vector3 (0, -2, 0), Vector3.down), out rayInfo);
					if (rayInfo.transform != null) {
						Debug.Log ("raycast found object " + rayInfo.transform.gameObject.name);
						if (rayInfo.collider.gameObject.layer == LayerMask.NameToLayer ("BodyPart")) {
							grabbedLimb = rayInfo.transform;
						}
					} else
						Debug.Log ("raycast missed all objects");
					targetDrop = 1;
				} else {
					if (grabbedLimb != null) {
						targetExtension = 0;
						liftingLimb = true;
					}
					targetDrop = 0;
					deliveryState = limbDeliveryState.LIFTING;
				}
				dropState = dropBtnState.OFFLINE;
			}
			break;
		case(limbDeliveryState.LIFTING):
			if (Mathf.Abs (currentDrop - targetDrop) < float.Epsilon) {
				if (targetDrop == 1) {
					if (grabbedLimb != null) {
						limbOffset = grabbedLimb.position - claw.transform.position;
						grabbedLimb.GetComponentInParent<Rigidbody> ().isKinematic = true;
					}
				} else if (targetDrop == 0) {
					if (grabbedLimb != null) {
						if (limbLock.AcceptLimb (grabbedLimb.gameObject)) {
							deliveryState = limbDeliveryState.ROTATING_UP;
							rotationStateX = 3f;
						} else {
							grabbedLimb.GetComponentInParent<Rigidbody> ().isKinematic = false;
							grabbedLimb = null;
							deliveryState = limbDeliveryState.SEARCHING;
						}
					} else {
						deliveryState = limbDeliveryState.SEARCHING;
					}
				}
			}
			break;
		case(limbDeliveryState.ROTATING_UP):
			if (Mathf.Abs (beamRotationX - targetRotationX) < float.Epsilon) {
				deliveryState = limbDeliveryState.PUSHING;
				targetDrop = 0.5f;
			}
			break;
		case(limbDeliveryState.PUSHING):
			if (Mathf.Abs (currentDrop - targetDrop) < float.Epsilon) {
				Destroy (grabbedLimb.gameObject);
				liftingLimb = false;
				grabbedLimb = null;
				deliveryState = limbDeliveryState.RETRACTING;
				targetDrop = 0.0f;
			}
			break;
		case(limbDeliveryState.RETRACTING):
			if (Mathf.Abs (currentDrop - targetDrop) < float.Epsilon) {
				deliveryState = limbDeliveryState.ROTATING_DOWN;
				rotationStateX = 0f;
			}
			break;
		case(limbDeliveryState.ROTATING_DOWN):
			if (Mathf.Abs (beamRotationX - targetRotationX) < float.Epsilon) {
				deliveryState = limbDeliveryState.SEARCHING;
			}
			break;
		}



		targetRotationX = rotationStateX * 60 % float.MaxValue - 180f;
		targetRotationY = rotationStateY * 60 % float.MaxValue;
		beamRotationY = (beamRotationY + Mathf.Clamp (targetRotationY - beamRotationY, -5, 5)) % float.MaxValue;
		clawExtension = clawExtension + Mathf.Clamp (targetExtension - clawExtension, -0.01f, 0.01f);
		currentDrop = (currentDrop + Mathf.Clamp (targetDrop - currentDrop, -0.01f, 0.01f));

		if (deliveryState == limbDeliveryState.ROTATING_UP || deliveryState == limbDeliveryState.ROTATING_DOWN)
			beamRotationX = (beamRotationX + Mathf.Clamp (targetRotationX - beamRotationX, -2, 2));

	}
}