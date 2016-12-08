using UnityEngine;
using System.Collections;

public class BadButton : MonoBehaviour {
	public GameObject goodButton;

	private bool readyToClick;
	private GameObject captchaController;
	private GameObject player;
	private PlayerController playerScript;
	private EthicsCaptcha captchaScript;

	// Use this for initialization
	void Start () {
		captchaController = GameObject.Find("CaptchaController");
		captchaScript = captchaController.GetComponent<EthicsCaptcha>();
		player = GameObject.Find("player");
		playerScript = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (readyToClick && Input.GetButtonDown("Fire1")) {
			captchaScript.humanVal -= 1;
			string terminalName = gameObject.name;
			int terminalVal = int.Parse(terminalName.Substring(terminalName.Length - 1));
			captchaScript.SayThanks(terminalVal);
			gameObject.SetActive(false);
			goodButton.SetActive(false);
			playerScript.startMovement();
		}
		
	}

	public void Deactivate (){
		readyToClick = false;
	}

	public void Activate (){
		readyToClick = true;
	}

}
