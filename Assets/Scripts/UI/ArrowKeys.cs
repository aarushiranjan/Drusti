using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeys : MonoBehaviour {

	public bool left,right,up,down;

	public static ArrowKeys instance;
	public int maxCounter;
	// Use this for initialization
	void Start () {
		instance = this;
	}

	int counter;
	// Update is called once per frame
	void Update () {
		if (left || right || down || up) {
			counter++;
		}
		if (counter > maxCounter) {
			counter = 0;
			left = false;
			right = false;
			up = false;
			down = false;
		}
	}

	public void leftArrow()
	{
		left = true;
	}

	public void rightArrow()
	{
		right = true;
	}public void upArrow()
	{
		up = true;
	}
	public void downArrow()
	{
		down = true;
	}

	public void reset()
	{
		up = false;
		down = false;
		left = false;
		right = false;
	}


}
