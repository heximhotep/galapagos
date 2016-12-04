using UnityEngine;
using System.Collections;

public class ScannerController : MonoBehaviour {
	[SerializeField]
	private GameObject player;

	private GameObject head, neck;

	private bool followPlayer;

	void Awake()
	{
		followPlayer = true;
		head = transform.Find ("scanner_head").gameObject;
		neck = transform.Find ("scanner_neck").gameObject;
	}

	void FixedUpdate()
	{
		if (followPlayer) 
		{
			Vector3 _offset = (player.transform.position - head.transform.position).normalized;
			_offset = _offset - new Vector3 (0, _offset.y, 0);
			Vector3 _curFace = head.transform.up - new Vector3 (0, head.transform.up.y, 0);
			float theta = Vector3.Angle (_curFace, _offset);
			if (theta > 1f) 
			{
				float thetaSign = Mathf.Sign ((_curFace - _offset).x);
				theta = Mathf.Clamp (theta * thetaSign, -0.5f, 0.5f);
				head.transform.Rotate (Vector3.forward, theta);
			}
		}
	}
}
