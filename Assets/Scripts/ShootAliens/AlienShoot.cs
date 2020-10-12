using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShoot : MonoBehaviour {

	public GameObject splatPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clicked()
	{
		if(!PlaceAliens.instance.GameOver)

		{
			Destroy(this.gameObject);
			PlaceAliens.instance.shotAlien();
			//splat
			GameObject g = Instantiate(splatPrefab);
			g.transform.parent = this.transform.parent;
			g.transform.position = this.transform.position;
			//Destroy(g,splatDisplayTime);
			Destroy(g,3.0f);
			//PlaceAliens.instance.splats.Add(g);
		}
	}

}
