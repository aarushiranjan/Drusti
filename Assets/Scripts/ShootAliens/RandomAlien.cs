using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomAlien : MonoBehaviour {

	public List<Sprite> sprites;
	public Image image;

	public int value;


	// Use this for initialization
	void Start () {	
		value =Random.Range(0,sprites.Count);
		image.sprite = sprites[value];		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
