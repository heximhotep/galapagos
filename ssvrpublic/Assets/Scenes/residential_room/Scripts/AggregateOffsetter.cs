using UnityEngine;
using System.Collections;

public class AggregateOffsetter : MonoBehaviour {

	public float minOffset, maxOffset;

	void Start () 
	{
		AnimationDelayerRandomOffset[] feeders = GetComponentsInChildren<AnimationDelayerRandomOffset> ();
		foreach(var feeder in feeders)
		{
			feeder.OffsetAnim (Random.Range (minOffset, maxOffset));
		}
	}
}
