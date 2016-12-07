using UnityEngine;
using AssemblyCSharp;

public class DoorController : MonoBehaviour {

	private Animator doorAnimator;
	private AudioSource audio;
	public AudioClip doorSlide_1;
	public AudioClip doorSlide_2;
	public AudioClip doorClose;
	public AudioClip doorLock;


	void Awake()
	{
		doorAnimator = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			doorAnimator.SetBool ("Open", true);
			if (!audio.isPlaying) {
				if (Random.Range(0, 2) % 2 == 0)
					audio.clip = doorSlide_1;
				else
					audio.clip = doorSlide_2;
				audio.Play();
			}
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) {
			if (!audio.isPlaying) {
				doorAnimator.SetBool ("Open", false);
				audio.clip = doorClose;
				audio.Play();

				print (doorClose.length);
				//yield WaitForSeconds(doorClose.length);

				audio.clip = doorLock;
				audio.Play();
			}
		}

	}

}