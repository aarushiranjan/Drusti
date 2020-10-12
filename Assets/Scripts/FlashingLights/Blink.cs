using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

	public Image img;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	float t;
	void Update () {
		t += Time.deltaTime;
		t = t % 1;
		Color c = img.color;
		c.a = t;
		img.color = c;
	}
}
