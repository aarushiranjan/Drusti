using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIxedSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//get screen size in pixels
		Debug.Log(Screen.currentResolution);
		Debug.Log(Screen.height);
		Debug.Log(Screen.width);
		//28cm is the screen width
		//x = 3.35 cm for 20/20
		//20/800 

		//Stupid dpi, Screen's physical size cannot be known 

		//based on dpi, calculate the scales of enemyships

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
