using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignChar : MonoBehaviour {
	public List<Sprite> sprites;
	public SpriteRenderer sr;

	public int value;


	// Use this for initialization
	void Start () {
			sr.sprite = sprites[value];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
