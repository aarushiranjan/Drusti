using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseControl : MonoBehaviour {

	public Camera cam;

	public int screenHeight;
	// Use this for initialization
	void Start () {
		screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mousePosition = Input.mousePosition;
		if (mousePosition.y < screenHeight / 3) 
		{
			Vector3 yo = cam.ScreenToWorldPoint (new Vector3 (mousePosition.x, mousePosition.y, 0));
		
			
			yo.y = this.transform.position.y;
			yo.z = this.transform.position.z;
			this.gameObject.transform.position = yo;
		}
	}
}
