using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Placement2 : MonoBehaviour {

	//we know the height of screen is 70
	public GameObject character;
	public Text gameOverText;
	
	public RandomCharRotation[] rcr;
	public GameObject charExplosion;

	public float gameTime;
	
	public GameObject gameOverPanel;
	public Text gameOverScore;
	public int Score;

	bool gameOverFlag;
	public string gameId;
	float scale;
	
	int level;
	// Use this for initialization
	void Start () {
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		level = gameLevels[gid];		
		scale = GetScale.instance.getScaleFromLevel(level);


		BackgroundScore.instance.playRandom ();
		gameTime = gameDurations[gid]*60;
		place();
	}
	

	public void place()
	{
		counter=0;
		float s = scale;
		int nr = (int)(110/s)-1;
		
		if(nr<=0)
			nr=1;

		int numberOfRows = nr;
		int numberOfColumns = (int)(numberOfRows*1.6f);
		
			if(numberOfColumns==1)
				numberOfColumns=2;
			if(numberOfColumns%2==1)
				numberOfColumns++;
		
		//
		int n = numberOfRows/2 - numberOfRows/6;
		float m = numberOfColumns/2 -0.5f ;
		rcr = new RandomCharRotation[(numberOfColumns)*numberOfRows];

		for(int i=0;i<numberOfRows;i++)
		{
			for(int j=0;j<numberOfColumns;j++)
			{	GameObject g = Instantiate(character);
				g.transform.position = new Vector3(j*s-m*s,(i)*s-n*s,0);
				rcr[(numberOfRows-i-1)*(numberOfColumns)+j] = g.GetComponent<RandomCharRotation>();
				// if(target==g.GetComponent<AssignChar>().value)
				// 	counter++;
			}
		}
		
	}

float elapsedGameTime;
	// Update is called once per frame
	void Update () {
elapsedGameTime += Time.deltaTime;
			 if(elapsedGameTime>gameTime)
			{
                if(!gameOverFlag)
				    gameOver();
			}

	}
	int counter=0;

	public void pressed(int i)
	{
		if(!gameOverFlag)
		{
		if(i==rcr[counter].value)
			{
				//Destroy explosion
				GameObject g = Instantiate(charExplosion);
				g.transform.position = rcr[counter].gameObject.transform.position;
				Destroy(rcr[counter].gameObject);
				Sounds.instance.playExplosion();
				Destroy(g,5);
				Score ++;
				counter++;
			}
		
		//wame over
		if(counter==rcr.Length)
		{
			waveOver();
		}

		}
	}


	void waveOver()
	{
		//
		Debug.Log("GameOver");
		//gameOverText.gameObject.SetActive(true);
		//ScoreSender.instance.sendScore(6,100);
		place();
	}


void gameOver()
	{
		gameOverFlag=true;
		gameOverPanel.SetActive(true);
		gameOverScore.text = "No of hits: " + Score;
		Time.timeScale = 0.0f;

		if(Score>gameTime/3)		//should take max 3 seconds to find it
		{
			level=level+1;
		}
		ScoreSender.instance.saveScore(gameId,level,Score);



	}

}
