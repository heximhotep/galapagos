using UnityEngine;
using System.Collections;

public class AggregateOffsetter : MonoBehaviour {

	public float minOffset, maxOffset;
	//private AudioSource audio;
	//public AudioClip sucksuck;

	void Awake () {
		//audio = GetComponent <AudioSource> ();
		//audio.clip = sucksuck;
		//audio.loop = true;
		//audio.maxDistance = 3;
	}

	void Start () 
	{
		AnimationDelayerRandomOffset[] feeders = GetComponentsInChildren<AnimationDelayerRandomOffset> ();
		foreach(var feeder in feeders)
		{
			feeder.OffsetAnim (Random.Range (minOffset, maxOffset));
		}
		//audio.Play();
	}
}
