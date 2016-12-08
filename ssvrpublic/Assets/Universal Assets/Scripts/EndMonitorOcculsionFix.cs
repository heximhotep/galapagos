using UnityEngine;
using System.Collections;

public class EndMonitorOcculsionFix : MonoBehaviour {
	public GameObject canvas;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player"){
			canvas.GetComponent<Canvas>().enabled = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player"){
			canvas.GetComponent<Canvas>().enabled = false;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
