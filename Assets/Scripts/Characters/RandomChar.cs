using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChar : MonoBehaviour {

	public AssignChar aschar;
	// Use this for initialization
	void Awake () {
		aschar.value = Random.Range(0,aschar.sprites.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
