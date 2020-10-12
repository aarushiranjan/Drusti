using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour {

	public Camera cam;
	public float depth;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!SpaceRaceController.instance.gameOver)
		{
			Vector2 mousePosition = Input.mousePosition;
			Vector3 yo = 	cam.ScreenToWorldPoint( new Vector3 (mousePosition.x, mousePosition.y, depth));
			
			yo.y = this.transform.position.y;
			yo.z = this.transform.position.z;
			this.gameObject.transform.position = yo; 
		}
		
		
		}
}
