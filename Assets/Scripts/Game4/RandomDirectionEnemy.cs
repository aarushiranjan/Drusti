using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionEnemy : MonoBehaviour {
	public int value;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void rotateYo()
	{
	this.transform.RotateAround(new Vector3(0,1,0), value*(Mathf.PI/4));
	
	}

}
