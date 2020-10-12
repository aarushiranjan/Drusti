using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileGuidance : MonoBehaviour {


	public float speed;
	public float rotationSpeed;
	public float yoThres=200000;
	
	public GameObject target;
	public bool correct;
	public bool straight;
	

	// Use this for initialization
	void Start () {
		target =GameObject.FindWithTag("Enemy");
		if(target==null)
		{
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!straight)
		{
			if(correct)
			{
			//Rotation
			float yo = (1/(transform.position-target.transform.position).sqrMagnitude)*yoThres;

			Vector3 targetDir = target.transform.position - transform.position;
			float step = rotationSpeed * Time.deltaTime*yo;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 1.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
			//Movement
			transform.Translate(new Vector3(0,0,1)*speed*Time.deltaTime);
			}
			else
			{
			//Rotation
			float yo = (1/(transform.position-target.transform.position).sqrMagnitude)*yoThres;
			Vector3 targetDir = transform.position - target.transform.position;
			float step = rotationSpeed * Time.deltaTime*yo;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 1.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
			//Movement
			transform.Translate(new Vector3(0,0,1)*speed*Time.deltaTime);
				
			}
		}	
		else
		{
			transform.Translate(new Vector3(0,0,1)*speed*Time.deltaTime);
		}

	}
}
