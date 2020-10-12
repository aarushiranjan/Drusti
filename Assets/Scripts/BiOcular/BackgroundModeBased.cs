using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundModeBased : MonoBehaviour {


 Camera cam;
	// Use this for initialization
	void Start () {
		int mo = PlayerPrefs.GetInt("mode",0);
		if(mo==2)
		{
		cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
		cam.backgroundColor = Color.white;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
