using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


	public static GameController instance;
	public GameObject cpPrefab;
	public float rate;
	public float maxX;
	

	float timeDiff;
	float elapsedTime=0.0f;

	public Text scoreBoard;
	public Text gameOverText;
	public Text accuracyText;
	int maxCounter;
	bool isGameOver;

	// Use this for initialization
	void Start () {
		timeDiff = 1.0f/rate;
		instance = this;
		maxCounter = PlayerPrefs.GetInt("game5no",10);
		elapsedTime=timeDiff;
	}
	
	int counter;
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime>timeDiff)
		{
			elapsedTime=0.0f;
			GameObject g = Instantiate(cpPrefab); 
			float x = Random.Range(-maxX,maxX);
			g.transform.position = this.transform.position+ new Vector3(x,0,0);
			Destroy(g,20);
			counter++;

		}
		if(counter==maxCounter+3 &&!isGameOver)
			gameOver();
	}


	int passCounter;
	public void passed()
	{
		passCounter++;
		scoreBoard.text = "Score: "+ passCounter;
		SkiSounds.instance.playGood();
	}
	public void pole()
	{
		passCounter--;
		SkiSounds.instance.playBad();
	}

	public void gameOver()
	{
		
		float accuracy = ((passCounter)*100.0f)/counter;
		gameOverText.gameObject.SetActive(true);
		accuracyText.gameObject.SetActive(true);
		int a = (int) accuracy;
		accuracyText.text = "Accuracy: "+ a;
		ScoreSender.instance.sendScore(7,a);
		isGameOver=true;
	}



}
