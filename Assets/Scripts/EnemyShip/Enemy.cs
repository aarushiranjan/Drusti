using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public MissileLauncher.Direction direction;
	// Use this for initialization
	void Start () {
		int d = Random.Range(0,4);
		
		direction = MissileLauncher.Direction.up;

		if(d==1)
		{
			transform.Rotate(Vector3.up * 90.0F, Space.World);
			direction = MissileLauncher.Direction.right;
		}	
	
		if(d==2)
		{
			transform.Rotate(Vector3.up * -90.0F, Space.World);
			direction = MissileLauncher.Direction.left;
		}	
	
		if(d==3)
		{
			transform.Rotate(Vector3.up * 180.0F, Space.World);
			direction = MissileLauncher.Direction.down;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
