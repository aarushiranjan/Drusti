using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceRaceController : MonoBehaviour {

	public float gameTimeInSeconds;
	public float timeGapBetweenCheckpoints;
	public float maxXdisplacement;
	public float checkpointSpeed;
	public CheckpointMotion checkpointPrefab;
	public static SpaceRaceController instance;
	public ShootEffectRed shootEffect;

	public int Score;
	public Text scoreTextBox;
	public GameObject gameOverPanel;
	public Text gameOverScore;

	//
	public string gameId;
	public int level;
	public int totalCheckpoints;
	// Use this for initialization
	void Start () {
		
		int gid = int.Parse(gameId);
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		gameTimeInSeconds = gameDurations[gid]*60.0f;
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		level = gameLevels[gid];

		Time.timeScale = 1.0f;
		instance = this;
		checkpoint();
	}
	
	void checkpoint()
	{
		
		if(gameOver)
		{
			return;
		}
		totalCheckpoints++;
		CheckpointMotion g = Instantiate(checkpointPrefab) as CheckpointMotion;
		g.speed = -checkpointSpeed;
		float randomX = Random.Range(-maxXdisplacement,maxXdisplacement);
		g.gameObject.transform.position = this.transform.position + new Vector3(randomX,0,0);
		Destroy(g.gameObject, timeGapBetweenCheckpoints*5);	
	}


	float elapsedTime;
	float gameTime;
	public bool gameOver;

	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;
		if(gameTime>gameTimeInSeconds)
		{
			gameOver=true;
			gameTime=0.0f;
			GameOver();
			
		}
		else if(gameTime<gameTimeInSeconds-timeGapBetweenCheckpoints)
		{
			
			elapsedTime += Time.deltaTime;
			if(elapsedTime>timeGapBetweenCheckpoints)
			{
				checkpoint();
				elapsedTime=0.0f;
			}
		}
			
	}

	public void checkHit()
	{
		if(!gameOver)
		{PlaySounds.instance.playSound(0);
		Score++;
		scoreTextBox.text = "Score: "+Score;
		}
	}

	public void checkMiss()
	{
		if(!gameOver)
		{
			PlaySounds.instance.playSound(1);
			shootEffect.display();
		}
	}

	bool gameOverFlag;
	void GameOver()
	{
		gameOverPanel.SetActive(true);
		gameOverScore.text = "Score: " + Score;
		if(!gameOverFlag)
		{
			gameOverFlag=true;
			float a = (float)Score/(float)totalCheckpoints*100;
			if(a>90.0f)
			{
				level++;
			}
			ScoreSender.instance.saveScore(gameId,level,Score);

		}

	}

}
