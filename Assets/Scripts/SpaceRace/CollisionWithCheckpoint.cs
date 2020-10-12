using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithCheckpoint : MonoBehaviour {

	public bool isPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	public void yo()
	{
		if(isPlayer)
			{
				SpaceRaceController.instance.checkHit();
			}
		else
		{
			SpaceRaceController.instance.checkMiss();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
