using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemMatchController : MonoBehaviour {

	public GameObject showingPanel;
	public Image showImage;

	public GameObject askingPanel;
	public List<Button> askButtons;

	public List<Sprite> options;

	public float showingTime;
	bool showing;

	public static MemMatchController instance;

	public GameObject gameOverPanel;
	public Text gameOverScore;

	public float duration;
	public int level;
	public int totalRounds;
	public string gameId;
	// Use this for initialization
	void Start () {
		
		instance = this;
		show ();
		BackgroundScore.instance.playRandom ();
		Time.timeScale = 1.0f;
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		duration = gameDurations[gid]*60.0f;
		level = gameLevels[gid];

		showingTime = 2 - 1.5f*(float)level/100.0f;
	}

	bool gameOverFlag;
	void gameOver()
	{
		if(!gameOverFlag)
		{
			gameOverFlag=true;
			Time.timeScale = 0.0f;
			gameOverPanel.SetActive (true);
			gameOverScore.text = "Score :" + score;
			float a = (float)score/(float)totalRounds*100.0f;
			Debug.Log("percent "+ a);
			if(a>95.0) //more than 95% accuracy then level increases.
			{
				Debug.Log("accuracy>95");
				float m = 5-3*(float)level/100.0f;
				Debug.Log("duration of each round "+ m);
				Debug.Log(duration/m);
				Debug.Log(score);
				if(score>duration/m) // max 3 seconds
					{
						level++;
					}
			}
			ScoreSender.instance.saveScore(gameId,level,score);
		}

	}


	float elapsedGameTime;
	public int correctOption;
	float elapsedShowingTime;
	// Update is called once per frame
	void Update () {
		elapsedGameTime += Time.deltaTime;
		if (elapsedGameTime > duration) {
			//
			gameOver();
		}
		if (showing) {
			elapsedShowingTime += Time.deltaTime;
			if (elapsedShowingTime > showingTime) {
				ask ();
			}
		} 
		else {
		
		}
	}

	void show()
	{
		showingPanel.SetActive (true);
		askingPanel.SetActive (false);
		showing = true;
	
		correctOption = Random.Range (0, options.Count);
		showImage.sprite = options [correctOption];
	}



	void ask()
	{
		elapsedShowingTime = 0.0f;
		showing = false;
		showingPanel.SetActive (false);
		askingPanel.SetActive (true);
		totalRounds++;

		//
		int ch = Random.Range(0,askButtons.Count);

		for (int i = 0; i < askButtons.Count; i++) {
			if (i == ch) {
				askButtons [i].image.sprite = options [correctOption];
				askButtons [i].GetComponent<MemOption> ().C = correctOption;

			}
			else {
				int cc = Random.Range (0, options.Count);
				while (cc == correctOption) {
					cc = Random.Range (0, options.Count);
				}
				askButtons [i].image.sprite = options [cc];
				askButtons [i].GetComponent<MemOption> ().C = cc;

			}
		}
	}

	int score;
	public Text scoreText;

	public void ButtonPressed(int c)
	{
		Debug.Log (correctOption);
		Debug.Log (c);
		if (c == correctOption) {
			Debug.Log ("correct");
			score++;
			scoreText.text = "Score: " + score;
			PlaySounds.instance.playSound (0);	
			show ();
		}
		else {
			Debug.Log ("Wrong");
			PlaySounds.instance.playSound (1);	
			show ();
		}
	}


}
