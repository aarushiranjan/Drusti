using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPass : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter()
	{
		Debug.Log("Passed");

		GameController.instance.passed();
	}
}
