﻿using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	public Camera maincam;

	public float moveSpeed;

	private bool canMove, canMoveHard;

	// Use this for initialization
	void Start () 
	{
		canMove = true;
		canMoveHard = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canMove && Input.GetButton("Fire1"))
		{
			Vector3 forward = maincam.transform.forward;
			transform.position = transform.position + new Vector3(forward.x, 0f, forward.z) * moveSpeed * Time.deltaTime;
		}
	}

	public void Deactivate()
	{
		maincam.tag = "Untagged";
	}

	public void Activate()
	{
		maincam.tag = "MainCamera";
	}

	public void stopMovement()
	{
		canMove = false;
	}

	public void stopMovementHARD()
	{
		canMoveHard = false;
		canMove = false;
	}

	public void startMovementHARD()
	{
		canMoveHard = true;
		canMove = true;
	}

	public void startMovement()
	{
		if(canMoveHard)
			canMove = true;
	}
}
