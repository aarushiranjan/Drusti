using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode4 : MonoBehaviour {

	public GameObject explosion;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {

		if(this.gameObject.GetComponent<RandomDirectionEnemy>().value==GameController4.instance.missileDirection)

		{
		GameObject e = Instantiate(explosion);
		Destroy(e,3.0f);
        e.transform.position = this.transform.position;
		Destroy(other.gameObject);
		Destroy(this.gameObject);
		MissileLauncher4.instance.elapsedTime = MissileLauncher4.instance.timeDelay;
		Sounds.instance.playExplosion();
		
		GameController4.instance.spawnEnemy();
		}
	}


}
