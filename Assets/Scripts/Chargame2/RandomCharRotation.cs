using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharRotation : MonoBehaviour {
	public int value;
	// Use this for initialization
	void Start () {
		value = Random.Range(0,4);
		this.transform.RotateAround(new Vector3(0,0,1), value*(Mathf.PI/2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
