using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

	public float MaxX;
	public float MinY;
	public float MaxY;


	public float minT;
	public float maxT;
	public float maxAngle;
	public float speed;

	Vector3 direction;
	float angle;
	// Use this for initialization
	void Start () {
		//normal speed is 20cm/s 30degrees/sec pursuits
		//2cm/s for visual acuity of 200
		//70 = physical height
		// x = 2cm/s
		//x = 140cm/physicalhe
		int sc = PlayerPrefs.GetInt("screenSize",0);
		float acuity = PlayerPrefs.GetInt("currentAcuity",200);
		float screenSize=0.0f;
		acuity = 200/acuity;

		if(sc==0)
			screenSize=13.3f;
		if(sc==1)
			screenSize=14f;
		if(sc==2)
			screenSize=15.6f;
		screenSize = screenSize*2.54f;
		
		//we got the screen size
		float h = (float) Screen.height;
		float w = (float) Screen.width;
		float d = Mathf.Sqrt(h*h+w*w);
		float physicalHeight = (h*screenSize/d);

		speed =(280/physicalHeight)*acuity;
		maxT = 70/speed;
		minT = maxT/2;

	}
	
	float elapsedTime;
	float nextRandomMotionTime;

	void changeDir()
	{
		angle = angle + Random.Range(-maxAngle,maxAngle);
		float s = Mathf.Sin(angle);
		float c = Mathf.Cos(angle);
		direction = new Vector3(s,0,c);
		nextRandomMotionTime = Random.Range(minT,maxT);
		elapsedTime = 0.0f;
	}

	void FixedUpdate()
	{
		elapsedTime = elapsedTime+Time.fixedDeltaTime;
		if(elapsedTime>nextRandomMotionTime)
			changeDir();
		Bounds();
		this.transform.position += direction*speed*Time.fixedDeltaTime;
	}

	void Bounds()
	{
		if(transform.position.x>MaxX||transform.position.x<-MaxX||transform.position.z>MaxY||transform.position.z<MinY)
			{
				angle = angle + 3.14f;
				float s = Mathf.Sin(angle);
				float c = Mathf.Cos(angle);
				direction = new Vector3(s,0,c);
				elapsedTime = 0.0f;
			}
	}

	// Update is called once per frame
	void Update () {
		
	}



}
