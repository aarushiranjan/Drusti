using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffectRed : MonoBehaviour {

	public float timeToDisplay;

	// Use this for initialization
	void Start () {
		
	}

	float elapsedTime=0.0f;

	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime>timeToDisplay)
			this.gameObject.SetActive(false);
	}



	public void display()
	{
		this.gameObject.SetActive(true);
		elapsedTime =0.0f;
	}


}
