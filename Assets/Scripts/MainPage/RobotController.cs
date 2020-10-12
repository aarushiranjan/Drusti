using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour {

	public List<float> timeline;
	public List<string> triggers;

	public Animator animator;


	// Use this for initialization
	void Start () {
		
	}
	
	float elapsedTime;
	int counter;
	bool flag = true;
	// Update is called once per frame
	void Update () {
		if(flag)
		{
			elapsedTime += Time.deltaTime;
			if(elapsedTime>timeline[counter])
			{
				elapsedTime=0.0f;
				//Trigger the corresponding trigger
				animator.SetTrigger(triggers[counter]);
				counter++;				
				if(counter==triggers.Count)
				{
					flag=false;
				}
			}
		}
	}


}
