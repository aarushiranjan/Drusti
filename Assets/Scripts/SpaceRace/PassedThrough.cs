using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedThrough : MonoBehaviour {

	bool disable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
 	
	 void OnTriggerEnter(Collider other) {
		 if(!disable)
		 {
			if(other.gameObject.GetComponent<CollisionWithCheckpoint>()!=null)
			{
				if(other.gameObject.GetComponent<CollisionWithCheckpoint>().isPlayer)
					disable=true;
				other.gameObject.GetComponent<CollisionWithCheckpoint>().yo();
			}
		 }

    }

}
