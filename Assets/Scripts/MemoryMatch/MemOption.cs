using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemOption : MonoBehaviour {

	public int C;

	// Use this for initialization
	void Start () {
		C = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void buttonPressed()
	{
		Debug.Log (this.C + " " +MemMatchController.instance.correctOption);
		MemMatchController.instance.ButtonPressed (this.C);
	}

}
