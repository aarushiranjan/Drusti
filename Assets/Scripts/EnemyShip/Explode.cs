using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

	public GameObject explosion;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		GameObject e = Instantiate(explosion);
		Destroy(e,3.0f);
        e.transform.position = this.transform.position;
		Destroy(other.gameObject);
		Destroy(this.gameObject);
		Game1Controller.instance.spawnEnemy();
		MissileLauncher.instance.elapsedTime = MissileLauncher.instance.timeDelay;
		Sounds.instance.playExplosion();
	}


}
