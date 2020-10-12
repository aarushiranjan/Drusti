using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPole : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter()
	{
		Debug.Log("Pole");
		GameController.instance.pole();		
	}


}
