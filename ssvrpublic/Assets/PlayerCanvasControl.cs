using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasControl : MonoBehaviour {

	private Image overlay;

	private float overlayAlpha, overlayDelta;
	private float curR, curG, curB, curA, targR, targB, targG, targA;

	void Awake() {
		overlay = GetComponent<Image> ();
		Color _startCol = overlay.color;
		curR = _startCol.r;
		targR = curR;
		curG = _startCol.g;
		targG = curG;
		curB = _startCol.b;
		targB = curB;
		curA = _startCol.a;
		targA = curA;
		overlayAlpha = 0f;
		overlayDelta = 0.05f;
	}

	void Update() {
		curR = curR + Mathf.Clamp (targR - curR, -overlayDelta, overlayDelta);
		curG = curG + Mathf.Clamp (targG - curG, -overlayDelta, overlayDelta);
		curB = curB + Mathf.Clamp (targB - curB, -overlayDelta, overlayDelta);
		curA = curA + Mathf.Clamp (targA - curA, -overlayDelta, overlayDelta);

		overlay.color = new Color (curR, curG, curB, curA);

	}

	public void shiftColor(Color _newCol, float _rate)
	{
		overlayDelta = _rate;
		targR = _newCol.r;
		targG = _newCol.g;
		targB = _newCol.b;
		targA = _newCol.a;
	}

	public void switchSprite(Sprite _newSprite)
	{
		overlay.sprite = _newSprite;
	}
}
