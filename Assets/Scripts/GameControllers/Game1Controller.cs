﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game1Controller : MonoBehaviour {

	public float maxX;
	public float minY,maxY;
	public Enemy enemyPrefab;
	public static Game1Controller instance;
	public int missilesLaunched;
	public GameObject BackButton;
	public GameObject ExitButton;
	public Text scoreBoard;
	public Text gameOverText;
	public Text accuracyText;
	public string gameId;
	int counter;
	int maxCounter;
	public bool game2;
	public float duration;
	float scale;
	int level;
	// Use this for initialization
	void Start () {
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		duration = gameDurations[gid]*60.0f;
		level = gameLevels[gid];		
		scale = GetScale.instance.getScaleFromLevel(level);
		
	
		
		counter=-1;
	
		spawnEnemy();
		instance = this;

		BackgroundScore.instance.playRandom ();

	}
	public float timeElapsed;
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if(timeElapsed>duration)
			{gameComplete();}
	}

	public void spawnEnemy()
	{
		


		Enemy e = Instantiate(enemyPrefab) as Enemy;
		e.gameObject.transform.position = new Vector3(Random.Range(-maxX,maxX),0,Random.Range(minY,maxY));
		
		counter++;
		scoreBoard.text = "Score: "+counter;
		
			
	}
	bool gameOverFlag;

	public void gameComplete()
	{
		if(!gameOverFlag)
		{
			gameOverFlag=true;
			float accuracy = ((counter)*100.0f)/missilesLaunched;
			//  level increase if hits mores than dura
			float min = duration/5; //he should be able to hit 1  in 5 seconds, then only he will go to next level.

			Debug.Log(accuracy);
			//BackButton.SetActive(false);
			//ExitButton.SetActive(true);
			gameOverText.gameObject.SetActive(true);
			accuracyText.gameObject.SetActive(true);
			int a = (int) accuracy;
			accuracyText.text = "Accuracy: "+ a;

			
			
			if(a>75)
			{
				if(counter>min)
					level =level+1;
			}
			Time.timeScale=0.0f;
		
			ScoreSender.instance.saveScore(gameId,level,counter);
		}
	}

}
