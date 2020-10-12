using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

	bool showingDialog;
	public GameObject exitGameDialog;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void closeButtonPressed()
	{
		if(!showingDialog)
		{
			//showDialog
			exitGameDialog.SetActive(true);
			Time.timeScale=0.0f;
		}
	}

	public void yesPressed()
	{
		  Application.Quit();
	}

	public void noPressed()
	{
		exitGameDialog.SetActive(false);
		showingDialog=false;
		Time.timeScale=1.0f;
	}

}
