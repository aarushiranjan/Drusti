using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController4 : MonoBehaviour {

	public float maxX;
	public float minY,maxY;
	public Enemy enemyPrefab;
	public static GameController4 instance;
	public int missilesLaunched;
	public Text scoreBoard;
	public Text gameOverText;
	public Text accuracyText;
	public Image arrow;

	int counter;
	public string gameId;
	public float duration;
	int level;
	float scale;
	//
	public int missileDirection;
	
	public int[] enemyDirections;
	public int maxCounter;
	// Use this for initialization
	void Start () {
		
		instance = this;
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		duration = gameDurations[gid]*60.0f;
		level = gameLevels[gid];		
		scale = GetScale.instance.getScaleFromLevel(level);


		counter=0;
		maxCounter=10;	
		spawnEnemies();
		missileDirection=0;
		arrow.transform.RotateAround(new Vector3(0,0,1), missileDirection*(Mathf.PI/4));
		missileDirection = enemyDirections[counter];
		
		arrow.transform.RotateAround(new Vector3(0,0,-1), missileDirection*(Mathf.PI/4));

	}
	public float elapsedTime;
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime>duration)
			{
				gameComplete();
			}
	}

	public void spawnEnemy()
	{

		

		counter++;
		scoreBoard.text = "Score: "+counter;

		if(counter%10==0)
		{
			spawnEnemies();
		}

		
		arrow.transform.RotateAround(new Vector3(0,0,1), missileDirection*(Mathf.PI/4));
		
		missileDirection = enemyDirections[counter%10];
		
		arrow.transform.RotateAround(new Vector3(0,0,-1), missileDirection*(Mathf.PI/4));

		
	}

	public void spawnEnemies()
	{
		Debug.Log(Screen.width);
		Debug.Log(Screen.height);
		Debug.Log(minY);
		Debug.Log(maxX);
		Debug.Log(maxY);
		//Debug.Log("1pi2ou3;1o2i3;l1k23;l1k2kl;3k123");

		enemyDirections = new int[maxCounter];
		for(int i=0;i<maxCounter;i++)
		{	

			Enemy e = Instantiate(enemyPrefab) as Enemy;
			e.gameObject.transform.position = new Vector3(Random.Range(-maxX,maxX),0,Random.Range(minY,maxY));
			int r = Random.Range(0,8);
			enemyDirections[i]=r;
			e.gameObject.GetComponent<RandomDirectionEnemy>().value=r;
			e.gameObject.GetComponent<RandomDirectionEnemy>().rotateYo();
		}
	}

	bool gameOverFlag;
	public void gameComplete()
	{	if(!gameOverFlag)
		{
			gameOverFlag=true;
			float accuracy = ((counter)*100.0f)/missilesLaunched;
			Debug.Log(accuracy);
			gameOverText.gameObject.SetActive(true);
			accuracyText.gameObject.SetActive(true);
			int a = (int) accuracy;
			accuracyText.text = "Accuracy: "+ a;
			
			float min = duration/5; //he should be able to hit 1  in 5 seconds, then only he will go to next level.
			if(a>60)		//this game is a bit difficult
			{
				if(counter>min)
					level =level+1;
			}
			Time.timeScale=0.0f;

			ScoreSender.instance.saveScore(gameId,level,counter);




		}
	}

}
