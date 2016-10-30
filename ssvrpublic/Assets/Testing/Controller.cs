using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float lookSpeed;
	public float speed;
	public Transform cameraTransform;

	private float lrMove, udMove;
	private float yaw, pitch;
	private Rigidbody rb;
	private Vector3 movementVector;
	private Transform transform;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		lrMove = Input.GetAxisRaw("Horizontal");
		udMove = Input.GetAxisRaw("Vertical");
		yaw += lookSpeed * Input.GetAxis("Mouse X");
		pitch += lookSpeed * Input.GetAxis("Mouse Y");

		pitch = Mathf.Clamp(pitch, -90f, 90f);

		while (yaw < 0f) {
            yaw += 360f;
        }
        while (yaw >= 360f) {
            yaw -= 360f;
        }

        if(lrMove == 0f && udMove == 0f){
        	rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        else{
        	rb.constraints = RigidbodyConstraints.None;
        }
        movementVector = new Vector3(lrMove, 0f, udMove);
        transform.Translate(movementVector.normalized * speed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0f, yaw, 0f);
		cameraTransform.eulerAngles = new Vector3(-pitch, yaw, 0f);
	}
}
