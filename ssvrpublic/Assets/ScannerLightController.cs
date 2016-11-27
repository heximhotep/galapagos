using UnityEngine;
using System.Collections;

public class ScannerLightController : MonoBehaviour {
	[SerializeField]
	private GameObject player;
	private Vector3 pointDir;
	void Awake()
	{
		pointDir = -transform.forward;
	}
	void Update () {
		transform.forward = -(player.transform.position - transform.position).normalized;
	}
}
