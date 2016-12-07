using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class VoiceOverController : MonoBehaviour {

	[SerializeField]
	private AudioClip[] clips;

	private AudioSource speaker;
	private Queue clipQueue;
	private bool clipPlaying;

	void Awake () {
		clipQueue = new Queue ();
		clipPlaying = false;
		speaker = GetComponent<AudioSource> ();
	}

	void PlayClip()
	{
		if (clipPlaying) {
			Debug.LogError ("Attempted to play clip while another clip was playing");
			return;
		}
		if (clipQueue.Count == 0) {
			Debug.LogError ("Attempted to play clip when clip queue is empty");
			return;
		}
		int clipIndex = (int)clipQueue.Dequeue ();

		if (clipIndex >= clips.Length) {
			Debug.LogError ("Attempted to access clip index out of bounds");
			return;
		}

		AudioClip thisClip = clips [clipIndex];
		float clipLen = thisClip.length;
		speaker.clip = clips [clipIndex];
		speaker.Play ();
		clipPlaying = true;
		Invoke ("ClipStopped", clipLen);
	}

	void ClipStopped()
	{
		clipPlaying = false;
		if (clipQueue.Count > 0) {
			Invoke ("PlayClip", 1f);
		}
	}

	public void RequestPlayClip(int clipIndex)
	{
		clipQueue.Enqueue (clipIndex);
		if (!clipPlaying && clipQueue.Count == 1) {
			Invoke("PlayClip", 1f);
		}
		switch (clipIndex) {
		case(12):
			clipQueue.Enqueue (13);
			clipQueue.Enqueue (14);
			break;
		default:
			break;
		}
	}

	public void ClearClipQueue()
	{
		clipQueue.Clear ();
	}
}