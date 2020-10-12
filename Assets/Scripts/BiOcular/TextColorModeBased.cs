using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorModeBased : MonoBehaviour {

	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		int mo = PlayerPrefs.GetInt("mode",0);
		if(mo==2)
		{
		text.color = Color.black;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
