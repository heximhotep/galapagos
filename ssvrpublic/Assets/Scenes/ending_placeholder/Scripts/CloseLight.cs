using UnityEngine;
using System.Collections;
using ui = UnityEngine.UI;

public class CloseLight : MonoBehaviour {
    public GameObject[] lights;
    public GameObject roomLight;
    public GameObject fixImage, fixImageAgain;
    public GameObject playerCanvas;
    public AudioClip switchSound;
    private bool firedOff;
	// Use this for initialization
	void Start () {
		fixImage.GetComponent<ui.Image> ().color = new Color (0f, 0f, 0f, 0f);
		fixImageAgain.GetComponent<ui.Image> ().color = new Color (0f, 0f, 0f, 0f);
        firedOff = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter() {
        if (firedOff) return;
        firedOff = true;
        //roomLight.GetComponent<Light>().enabled = false;
        fixImage.GetComponent<ui.Image>().color = new Color(0f, 0f, 0f, 1f);

        StartCoroutine(turnOff());
    }

    IEnumerator turnOff() {
        for(int i = 0; i < lights.Length; i++)
		{
            lights[i].GetComponent<Light>().enabled = false;
            lights[i].GetComponent<AudioSource>().PlayOneShot(switchSound);
            print("play");
            if (i != lights.Length - 1) yield return new WaitForSeconds(1f);
        }
		fixImageAgain.GetComponent<ui.Image>().color = new Color(0f, 0f, 0f, 1f);
		playerCanvas.SetActive (true);
    }
}
