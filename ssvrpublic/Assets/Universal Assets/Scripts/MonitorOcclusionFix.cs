using UnityEngine;
using System.Collections;

public class MonitorOcclusionFix : MonoBehaviour {
	public GameObject monitor;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player"){
			monitor.SetActive(true);
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player"){
			monitor.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
