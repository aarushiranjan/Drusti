using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour {

	public static MissileLauncher instance;
	public float timeDelay;
	public MissileGuidance missile;
	public enum Direction { up,down,left,right};
	public Direction enemyDirection;

	// Use this for initialization
	void Start () {
		elapsedTime = timeDelay;
		instance = this;
	}
	

	public float elapsedTime;
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;

		if(elapsedTime>timeDelay)
		{
		 if (Input.GetKeyDown("up"))
			launchMissile(Direction.up);
		 if (Input.GetKeyDown("down"))
			launchMissile(Direction.down);
		 if (Input.GetKeyDown("left"))
			launchMissile(Direction.left);
		 if (Input.GetKeyDown("right"))
			launchMissile(Direction.right);

		//for the onscreen arrowkeys


			if (ArrowKeys.instance.up)
				launchMissile(Direction.up);
			if (ArrowKeys.instance.down) 
				launchMissile(Direction.down);
			if  (ArrowKeys.instance.left)
				launchMissile(Direction.left);
			if  (ArrowKeys.instance.right)
				launchMissile(Direction.right);
		}
		
	}


	void launchMissile(Direction keyPressed)
	{
		elapsedTime = 0.0f;
		GameObject e =GameObject.FindWithTag("Enemy");
		if(e==null)
			return;
		enemyDirection = e.GetComponent<Enemy>().direction;
		MissileGuidance m = Instantiate(missile) as MissileGuidance;
		m.transform.position = this.transform.position;
		m.correct =(keyPressed ==enemyDirection);
		
		Destroy(m.gameObject,timeDelay);
		Game1Controller.instance.missilesLaunched++;
	}



}
