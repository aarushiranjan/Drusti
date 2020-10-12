using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour {

		// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void buttonPressed(int s)
	{
		if(s==0)
			SceneManager.LoadScene("CharGame1");
		if(s==1)
			SceneManager.LoadScene("CharGame2");
		if(s==2)
			SceneManager.LoadScene("SnipperShoot");
		if(s==3)
			SceneManager.LoadScene("ColorMatching");
		if(s==4)
			SceneManager.LoadScene("Game1");
		if(s==5)
			SceneManager.LoadScene("Game4");
		if(s==6)
			SceneManager.LoadScene("Game2");
		if(s==7)
			SceneManager.LoadScene("Game3");
		if(s==8)
			SceneManager.LoadScene("MemoryMatch");
		if(s==9)
			SceneManager.LoadScene("ShootingAliens");
		if(s==10)
			SceneManager.LoadScene("SpaceRace");
		if(s==11)
			SceneManager.LoadScene("FlashingLights");
		
	}

}
