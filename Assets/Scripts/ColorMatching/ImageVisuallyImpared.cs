using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageVisuallyImpared : MonoBehaviour {

	public Sprite spriteForVisuallyImpaired;

	// Use this for initialization
	void Start () {
	
		int mo = PlayerPrefs.GetInt("visuallyImpaired",0);
		if(mo==0)
		{
			Image img = GetComponent<Image>();
			img.sprite = spriteForVisuallyImpaired;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
