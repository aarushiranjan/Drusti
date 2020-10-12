using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScale : MonoBehaviour {

	public float defaultSize=1;

	// Use this for initialization
	void Start () {



		float s = GetScale.scale/defaultSize;

		if (s != 0)
		{
			transform.localScale = new Vector3(s, s, s);
		}

		else {

			transform.localScale = new Vector3(1, 1, 1);

		}

		Debug.Log(s);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
