﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {
	public static GameController instance;

	void Awake()
	{
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void LoadClawRoom()
	{
		SceneManager.LoadScene ("claw_room");
	}

	public void LoadKissRoom()
	{
		SceneManager.LoadScene ("kiss_room");
	}
}
