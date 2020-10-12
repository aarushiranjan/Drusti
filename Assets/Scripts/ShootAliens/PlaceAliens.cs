using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceAliens : MonoBehaviour {


	public GameObject alienPrefab;
	public float timeInterval;
	public float skipProbability;
	public static PlaceAliens instance;

	public Text scoreText;
	public Text gameOverScoreText;
	public GameObject gameOverPanel;

	public List<GameObject> splats;	

	public string gameId;
	public int level;
	public float duration;
	public int noOfAliens;
	// Use this for initialization

	void Start () {
		
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		duration = gameDurations[gid]*60.0f;
		level = gameLevels[gid];	


		Debug.Log("Screen Width : " + Screen.width);
		 startPlacingAliens();
		 instance = this;
	 	BackgroundScore.instance.playRandom ();

		 timeInterval = 1.0f-0.7f*(level/100.0f);

	}

	int m,n;
	int i,j;
	float elapsedGameTime;

	void startPlacingAliens()
	{
		
		n = 800/50;
		float a = ((float)Screen.height / (float)(Screen.width));
		m = (int)((a*800)*9/500);
		j = m/2-1;
		i = -n/2+2;
		place = true;

		// //clear the splats
		// for (int k = 0; k < splats.Count; k++)
		// 	Destroy(splats [i]);
	}

	bool place=true;

	bool placeAlien()
	{
		bool placed = false;
		//noOfAliens++;

		if(j>-m/2)
		{
			if(i<n/2)
			{
				float r = Random.Range(0.0f,1.0f);
				if(r>skipProbability)
				{
					GameObject g = Instantiate(alienPrefab);
					g.transform.parent = this.transform;
					g.transform.localPosition = new Vector3(i*50-25,j*50,0);
					i++;
					coun++;
					placed = true;
                    noOfAliens++;

                }
				else
				{
					i++;
				}
			}
			else
			{
				i = -n/2+2;
				j--;
			}
		}
		else
		{
			place=false;
		}
		return placed;
	}


	

	int coun =0;
	int score;
	// Update is called once per frame
	
	float elapsedTime;
	
	void Update () {
		elapsedGameTime+= Time.deltaTime;
		if(elapsedGameTime>duration)
		{
			gameOver();
		}

		if(place)
		{
			elapsedTime +=Time.deltaTime;
			if(elapsedTime>timeInterval)
			{
				if (placeAlien ()) {
					elapsedTime = 0.0f;
				}
			}
		}
		else
		{
			//placing is done
			if(coun==0)
			{
				//place again
				startPlacingAliens();
			}
		}
	}

	public void shotAlien()
	{
	
	
		coun--;
		//play sounds
		PlaySounds.instance.playSound(0);
		PlaySounds.instance.playSound(1);

		score++;


		//score update
		scoreText.text = "Score: "+score;
	}

	public bool GameOver;

	void gameOver()
	{
		if(!GameOver)
		{
			place=false;
			GameOver =true;
			//show panel
			gameOverPanel.gameObject.SetActive(true);
			gameOverScoreText.text = "Score: "+ score;

			//level up if 
			float a = (float)score/(float)noOfAliens*100.0f;
            Debug.Log("Score : " + (float)score);
            Debug.Log("Total Number of Aliens : " + (float)noOfAliens);
            Debug.Log("Percentage of Aliens killed : " + a);
			if(a>80.0f)
			{
				level++;
			}

			Time.timeScale=0.0f;
		
			ScoreSender.instance.saveScore(gameId,level,score);
		}
	}


}
