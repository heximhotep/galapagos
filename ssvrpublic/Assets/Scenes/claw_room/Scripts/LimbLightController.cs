using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class LimbLightController : MonoBehaviour, DoorCondition { 

	public GameObject headLight, lArmLight, rArmLight, lLegLight, rLegLight, torsoLight;
	public Material litMat;

	private int headCount, armCount, legCount, torsoCount;
	private GameObject[] armList, legList;

	void Awake()
	{
		headCount = 0; 
		armCount = 0;
		legCount = 0;
		torsoCount = 0;
		armList = new GameObject[] {lArmLight, rArmLight};
		legList = new GameObject[] { lLegLight, rLegLight };
	}

	public bool AcceptLimb(GameObject limb)
	{
		switch (limb.tag) {
		case("LoverArm"):
			if (armCount < 2) {
				TurnOn (armList [armCount]);
				armCount++;
				return true;
			} else
				return false;
			break;
		case("LoverHead"):
			if (headCount < 1) {
				TurnOn (headLight);
				headCount++;
				return true;
			} else
				return false;
			break;
		case("LoverLeg"):
			if (legCount < 2) {
				TurnOn (legList [legCount]);
				legCount++;
				return true;
			} else
				return false;
			break;
		case("LoverTorso"):
			if (torsoCount < 1) {
				TurnOn (torsoLight);
				torsoCount++;
				return true;
			} else
				return false;
			break;
		default:
			return false;
			break;
		}
	}

	public bool CanOpen()
	{
		return (torsoCount > 0 && headCount > 0 && legCount > 1 && armCount > 1);
	}

	private void TurnOn(GameObject light)
	{
		Renderer thisRend = light.GetComponent<Renderer> ();
		thisRend.material = litMat;
	}
}
