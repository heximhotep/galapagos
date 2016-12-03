using UnityEngine;

public class LoverController : MonoBehaviour {
	[SerializeField]
	private GameObject body, animatedHead, rigidHead;
	private Rigidbody[] pieces;
	void Awake()
	{
		pieces = new Rigidbody[body.transform.childCount + 1];
		for (int i = 0; i < pieces.Length - 1; i++) {
			pieces [i] = body.transform.GetChild (i).gameObject.GetComponent<Rigidbody> ();
		}
		pieces [pieces.Length - 1] = rigidHead.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Rigidify()
	{
		rigidHead.SetActive (true);
		animatedHead.SetActive (false);
		foreach (var item in pieces) {
			item.isKinematic = false;
		}
	}
}
