using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class GetScale : MonoBehaviour {

	

	public float screenSize;
	public static float scale;



	public float getScaleFromLevel(int level)
	{
		//
		int va = PlayerPrefs.GetInt("visualAcuity",3);
		int a = 800;

		float aa = 800.0f;
	
		if(level<10)
		{
			aa = 800-400*(level/10.0f);
		}	
		else if(level<20)
		{
			aa = 400-200*((level-10)/10.0f);
		}	
		else if(level<30)
		{
			aa = 200-100*((level-20)/10.0f);
		}	
		else if(level<40)
		{
			aa = 100-20*((level-30)/10.0f);
		}	
		else if(level<50)
		{
			aa = 80-20*((level-40)/10.0f);
		}	
		else if(level<60)
		{
			aa = 60-20*((level-50)/10.0f);
		}	
		else if(level<70)
		{
			aa = 40-10*((level-60)/10.0f);
		}	
		else if(level<80)
		{
			aa = 30-5*((level-70)/10.0f);
		}	
		else if(level<90)
		{
			aa = 25-5*((level-80)/10.0f);
		}	
		else 
		{
			aa = 20;
		}	
		

		float acuity = 200/aa;
		float screenSize = PlayerPrefs.GetFloat("screenSize");
		Debug.Log("ScreenSize in inchs :"+screenSize);
		screenSize = screenSize*2.54f;

		//we got the screen size
		float h = (float) Screen.height;
		float w = (float) Screen.width;
		float d = Mathf.Sqrt(h*h+w*w);
		float physicalHeight = (h*screenSize/d);
		
		//
		float yo = 87.5f/15.0f;
		float scl = yo/physicalHeight*70.0f;
		scl =scl/acuity;
		Debug.Log("Yeay Scale: " + scl);
		scale=scl;
		return scl;
	}





	public static GetScale instance;

	void Awake()
	{
		instance=this;
	}

	// Use this for initialization
	void Start () {	
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
