using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour {

	Image img;
	public List<Color> colors;
	public int N;
	// Use this for initialization
	void Awake () {
		img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeIndex(int i)
	{
		img.color = colors[i];
		N=i;
	}
	
	public void onClick()
	{
		//send the current N
		ColorMatchController.instance.InputChoice(N);
	}

}
