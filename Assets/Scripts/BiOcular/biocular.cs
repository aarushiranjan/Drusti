using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biocular : MonoBehaviour {

	public AssignChar ac;

	void Awake()
	{
		SpriteRenderer m_SpriteRenderer;
	
		int mo = PlayerPrefs.GetInt("mode",1);

		//Normal
		if(mo==1)
		{
						//Fetch the SpriteRenderer from the GameObject
			m_SpriteRenderer = GetComponent<SpriteRenderer>();
			//Set the GameObject's Color quickly to a set Color (blue)
			m_SpriteRenderer.color = Color.green;

		}		
		//biocular, :Blue
		if(mo==0)
		{
			//Fetch the SpriteRenderer from the GameObject
			m_SpriteRenderer = GetComponent<SpriteRenderer>();
			//Set the GameObject's Color quickly to a set Color (blue)
			m_SpriteRenderer.color = Color.blue;

		}
		//Color impairment
		if(mo==2)
		{
			
						//Fetch the SpriteRenderer from the GameObject
			m_SpriteRenderer = GetComponent<SpriteRenderer>();
			//Set the GameObject's Color quickly to a set Color (blue)
			m_SpriteRenderer.color = Color.black;

		}


	}

	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
