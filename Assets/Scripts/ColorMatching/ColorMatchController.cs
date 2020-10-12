using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMatchController : MonoBehaviour {

	public Image centralImage;
	public List<Color> colors;
	public float maxColorDuration;

	public static ColorMatchController instance;
	public List<ColorButton> buttons;

	public Text scoreText;
	public int score;

	public float gameTime;
	public GameObject gameOverPanel;
	public Text gameOverScore;
	
	public LevelController1 lc;
	// Use this for initialization
	void Start () {
		
		assignColors();
		ColorMatchController.instance = this;
		//
		BackgroundScore.instance.playRandom ();
	}
	float elapsedGameTime;

	// Update is called once per frame
	void Update () {
		elapsedGameTime += Time.deltaTime;
			if(elapsedGameTime>gameTime)
			{
				gameOver();
			}


		elapsedTime += Time.deltaTime;
		if(elapsedTime>maxColorDuration)
		{
			changeColor();
		}
	}

	float elapsedTime;

	int choice;

	void changeColor()
	{
		elapsedTime = 0.0f;
	

		int newchoice = Random.Range(0,6);
		while(newchoice==choice)
		{
			newchoice = Random.Range(0,6);
		}
		choice = newchoice;
		elapsedGameTime += Time.deltaTime;
			if(elapsedGameTime>gameTime)
			{
				gameOver();
			}
		centralImage.color = colors[choice];

		assignColors();
	}

	public void InputChoice(int i)
	{
		if(!gameOverflag)
		{
			if(i==choice)
				{
					changeColor();
					score++;
					scoreText.text = "Score: "+score;
					PlaySounds.instance.playSound(0);
		
				}
			else
			{
				PlaySounds.instance.playSound(1);
			}
		
		}
	}

	public void assignColors()
	{
		for(int i=0;i<6;i++)
		{
			int r = Random.Range(0,6);
			//exchange i and r
			int p = buttons[i].N;
			int q = buttons[r].N;
			buttons[i].changeIndex(q);
			buttons[r].changeIndex(p);

		}
	}

	bool gameOverflag;
	void gameOver()
	{
		if(!gameOverflag)
			lc.saveSettings();
		gameOverflag=true;
		gameOverPanel.SetActive(true);
		gameOverScore.text = "Score: " + score;
		Time.timeScale = 0.0f;
	}


}
