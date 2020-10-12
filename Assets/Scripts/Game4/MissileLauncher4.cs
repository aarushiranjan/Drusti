using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher4 : MonoBehaviour {

	public static MissileLauncher4 instance;
	public float timeDelay;
	public MissileGuidance missile;
	public enum Direction { up,down,left,right};
	public Direction enemyDirection;

	// Use this for initialization
	void Start () {
		elapsedTime = timeDelay;
		instance = this;
		screenHeight = Screen.height;
	}
	
	int screenHeight;
	public float elapsedTime;
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;

		// if(elapsedTime>timeDelay)
		// {
		//  if (Input.GetKeyDown("up"))
		// 	launchMissile(Direction.up);
		//  if (Input.GetKeyDown("down"))
		// 	launchMissile(Direction.down);
		//  if (Input.GetKeyDown("left"))
		// 	launchMissile(Direction.left);
		//  if (Input.GetKeyDown("right"))
		// 	launchMissile(Direction.right);
		// }
		 
		if(Input.GetKeyDown("space"))
			launchMissile();
		
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector2 mousePosition = Input.mousePosition;
			if(mousePosition.y > screenHeight/3)
				launchMissile ();
		}
	}


	void launchMissile()
	{
		elapsedTime = 0.0f;
		GameObject e =GameObject.FindWithTag("Enemy");
		if(e==null)
			return;
		//enemyDirection = e.GetComponent<Enemy>().direction;
		MissileGuidance m = Instantiate(missile) as MissileGuidance;
		m.transform.position = this.transform.position;
		//m.correct =(keyPressed ==enemyDirection);
		m.straight=true;
		Destroy(m.gameObject,timeDelay);
		GameController4.instance.missilesLaunched++;
	}



}
