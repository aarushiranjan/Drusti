using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienTarget : MonoBehaviour {

	public GameObject gameOverPanel;
	public Text gameOverText;
	public Image alienImage;
	public List<Sprite> sprites;
	public Sprite blankSprite;
	public float alienShowTime;
	public float minGap;
	public float maxGap;
	public float totalGameTime;
	public ShootEffectRed ser;

	public int score;
	public Text scoreText;
	public string gameId;
	public int level;
	// Use this for initialization
	void Start () {
		nextRandomTime = Random.Range(minGap,maxGap);
		scoreText.text = "Score: 0";
		BackgroundScore.instance.playRandom ();

		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		totalGameTime = gameDurations[gid]*60.0f;
		level = gameLevels[gid];		
		alienShowTime = 2 -1.5f*(float)level/100.0f;



	}
	
	float elapsedTime;
	float nextRandomTime;
	float elapsedGameTime;
	int totalAliens;
	// Update is called once per frame
	void Update () {
		elapsedGameTime += Time.deltaTime;
		if (elapsedGameTime > totalGameTime) {
			//gameover
			gameOver();
		}
		shootCapture();
		if(!show)
		{
			elapsedTime += Time.deltaTime;
			if(elapsedTime>nextRandomTime)
			{
				elapsedTime = 0;
				showAlien();				
			}
		}
		else
		{
			elapsedTime += Time.deltaTime;
			if(elapsedTime>alienShowTime)
			{
				hideAlien();
			}
		}

	}

	bool show=false;

	void showAlien()
	{
		show=true;
		elapsedTime = 0.0f;
		int value = Random.Range(0,sprites.Count);
		alienImage.sprite = sprites[value];
		totalAliens++;
	}

	void hideAlien()
	{
		show=false;
		elapsedTime=0.0f;
		alienImage.sprite = blankSprite;
	}

	void shootAlien()
	{
		//
		if(show)
		{
			hideAlien();
			//hit effects
			PlaySounds.instance.playSound(1);
			score++;
			scoreText.text = "Score: " + score;
			
		}

		// shooting effects
		ser.display();
		PlaySounds.instance.playSound(0);
	}


	void shootCapture()
	{
		//mouse or keyboard
		 if (Input.GetMouseButtonDown(0))
            shootAlien();
		else   if (Input.GetKeyDown("space"))
			shootAlien();
	}

	bool gameOverFlag;


	void gameOver()
	{
		if(!gameOverFlag)
		{
			gameOverFlag=true;
			
			Time.timeScale = 0.0f;
			gameOverPanel.SetActive (true);
			gameOverText.text = "Score: " + score;
			
			//accuracy >90%
			float a = (float)score/(float)totalAliens*100;
			if(a>90.0f)
			{
				level++;
			}


		
			ScoreSender.instance.saveScore(gameId,level,score);

		}
	}

}
