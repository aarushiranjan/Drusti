using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPageController : MonoBehaviour {

	public GameObject patchTimePanel;
	public GameObject validTimePanel;
	public GameObject startGame;
	public InputField hrs;
	public InputField mins;
	public GameObject EndGamePanel;

	public float patchTimePanelTimeIn;
	public float startGameTimeIn;

	public float elapsedTime;
	// Use this for initialization
	string sessionId;
	void Start () {
		Time.timeScale =1.0f;
		BackgroundScore.instance.playScore (0);
		
		//get session id
		sessionId = Authentication.instance.session;
		patchFlag = (PlayerPrefs.GetInt(sessionId+"patchFlag",0)==0);
		startFlag = !patchFlag;

		//get current session games
		int noOfGames = PlayerPrefs.GetInt("totalGames",0);
		int[] currentGamePlays = PlayerPrefsX.GetIntArray(sessionId+"currentGamePlays",0,noOfGames+1);
		
		//get the games we need to load from settings.
		int[] gamePlays = PlayerPrefsX.GetIntArray("gamePlays");

		//number of games played in this session
		int noOfGamesInThisSession = 0;
		bool gamesLeft = false;
		for(int i=0;i<currentGamePlays.Length;i++)
		{
			if(currentGamePlays[i]==1)
			{
				noOfGamesInThisSession++;
			}
			if( gamePlays[i]==1)
				if(currentGamePlays[i]==0)
				{
					gamesLeft=true;
				}
		}
		
		//end therapy when no other game is left to play or number of games in this session reaches max
		int maxGames = 	PlayerPrefs.GetInt("maxGames",0);
		
		if(noOfGamesInThisSession==maxGames)
			{
				Debug.Log("maxGames :"+maxGames);
				Debug.Log("noOfGAMES:" + noOfGamesInThisSession);
				endTherapy();
			}
		if(! gamesLeft)
			{
				Debug.Log("no games left");
				endTherapy();
			}



	}

	public bool patchFlag= true;
	public bool startFlag =false;
	
	// Update is called once per frame
	void Update () {
		if(patchFlag)
		{
			elapsedTime	+= Time.deltaTime;
			if(elapsedTime > patchTimePanelTimeIn)
			{
				patchTimePanel.SetActive(true);
				patchFlag=false;
				PlayerPrefs.SetInt(sessionId+"patchFlag",1);
				elapsedTime=0.0f;
			}
		}	
		if(startFlag)
		{
			elapsedTime += Time.deltaTime;
			if(elapsedTime>startGameTimeIn)
			{
				startGame.SetActive(true);
				startFlag=false;
			}
		}
	}

	public void ValidateTime()
	{
		//
		int h,m;
		bool hh= int.TryParse(hrs.text,out h);
		bool mm= int.TryParse(mins.text,out m);
		if(m<0	|| m>59 || h<0 ||(!hh)||(!mm))
		{
			showEnterValidTime();
			validTime=false;
			return;
		}
		validTime=true;
	}

	public void showEnterValidTime()
	{
		validTimePanel.SetActive(true);
	}

	public void hideEnterValidTime()
	{
		validTimePanel.SetActive(false);
	}

	bool validTime;

	public void okPressed()
	{
		ValidateTime();
		if(validTime)
		{
			//send or save time
			int h,m;
			bool hh= int.TryParse(hrs.text,out h);
			bool mm= int.TryParse(mins.text,out m);
			PatchTimeSender.instance.sendPatchTime(h*60+m);
			//hide
			patchTimePanel.SetActive(false);
			//
			startFlag=true;

		}
	}

	public void endTherapy()
	{
		ScoreSender.instance.uploadScore();
		EndGamePanel.SetActive(true);
		Debug.Log("we end the session here");
		Time.timeScale=0.0f;
	}

	// Automatically loads the correct scene randomly
	public void startTherapy()
	{
		int noOfGames = PlayerPrefs.GetInt("totalGames",0);
		int[] currentGamePlays = PlayerPrefsX.GetIntArray(sessionId+"currentGamePlays",0,noOfGames+1);
		int noOfGamesInThisSession = 0;
		for(int i=0;i<currentGamePlays.Length;i++)
		{
			if(currentGamePlays[i]==1)
			{
				noOfGamesInThisSession++;
			}
		}
		


		//get the games we need to load from settings.
		int[] gamePlays = PlayerPrefsX.GetIntArray("gamePlays");
		
		//pick at random
		int yo =0;
		while(true)
		{
			yo = Random.Range(0, gamePlays.Length);
			if(gamePlays[yo]==1)
				if(currentGamePlays[yo]==0)
					break;
		}

		//based on yo, load the scene
		Debug.Log("we are loading game with id "+ yo);
		currentGamePlays[yo]=1;
		PlayerPrefsX.SetIntArray(sessionId+"currentGamePlays",currentGamePlays);
		
		if(yo==1)
			SceneManager.LoadScene("ColorMatching");	
		if(yo==2)
			SceneManager.LoadScene("CharGame1");	
		if(yo==3)
			SceneManager.LoadScene("Game2");			
		if(yo==4)
			SceneManager.LoadScene("Game1");	
		if(yo==5)
			SceneManager.LoadScene("CharGame2");	
		if(yo==6)
			SceneManager.LoadScene("Game3");
		if(yo==7)
			SceneManager.LoadScene("Game4");	
		if(yo==8)
			SceneManager.LoadScene("FlashingLights");	
		if(yo==9)
			SceneManager.LoadScene("ShootingAliens");	
		if(yo==10)
			SceneManager.LoadScene("MemoryMatch");	
		if(yo==11)
			SceneManager.LoadScene("SnipperShoot");	
		if(yo==12)
			SceneManager.LoadScene("SpaceRace");	
		
	}



}
