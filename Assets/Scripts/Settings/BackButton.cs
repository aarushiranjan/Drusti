﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void buttonPressed()
	{
		//
		SceneManager.LoadScene("MainScene");
		Time.timeScale=1.0f;
		// BackgroundScore.instance.playScore (0);
		// Time.timeScale = 1.0f;
	}

}
