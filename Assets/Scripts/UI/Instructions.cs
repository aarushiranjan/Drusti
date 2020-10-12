using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale=0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void startPressed()
	{
		Time.timeScale=1.0f;
		this.gameObject.SetActive(false);
		
	}

}
