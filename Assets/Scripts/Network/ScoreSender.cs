using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;

public class ScoreSender : MonoBehaviour {

	public Authentication authentication;
	public  static readonly string scoreUrl = "https://eyenet.pythonanywhere.com/scores/";
	public static ScoreSender instance;
	public string Address = "127.0.0.1:8000";
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sendScore(int gameId, int level)
	{
		
	}

	public void sendScore(string gameId, int level, int score)
	{
		string loginURL = Address+ "/api/updateScore/";
	
		WWWForm form = new WWWForm();
        form.AddField( "sessionId", authentication.session );
        Dictionary<string, string> headers = form.headers;
        byte[] rawData = form.data;

		WWW www = new WWW(loginURL, rawData, headers);
	
	}
        // StartCoroutine(WaitForRequest(www));



	public void saveScore(string gameId, int nextlevel, int score)
	{
	
		// get session id
		string sessionCode = authentication.session;
		
		// get score array
		int noOfGames = PlayerPrefs.GetInt("totalGames",0);

		// get or create score, level, nextlevel arrays
		int gid = int.Parse(gameId);


		//
		int[] scoreArray = PlayerPrefsX.GetIntArray(sessionCode+"scores",0,noOfGames+1);
		int[] nextLevelsArray = PlayerPrefsX.GetIntArray(sessionCode+"nextLevels",0,noOfGames+1);
		
		nextLevelsArray[gid] = nextlevel;
		scoreArray[gid] = score;

		PlayerPrefsX.SetIntArray(sessionCode+"scores",scoreArray);
		PlayerPrefsX.SetIntArray(sessionCode+"nextLevels",nextLevelsArray);
	
//		PlayerPrefsX.SetIntArray("gameLevels",nextLevelsArray);
		Debug.Log("saved score and nextLevels offline");
	}

	//for sending score to the cloud
	

	public void uploadScore()
	{
		//testing
		//StartCoroutine(scoreSend(authentication.session,"1",45,34));
		syncScore(authentication.session);
		
	}


	//for online sessions only
	void syncScore(string sessionId)
	{
		Debug.Log("We are syncing the score for this session");
		
		int noOfGames = PlayerPrefs.GetInt("totalGames",0);
		int[] currentGamePlays = PlayerPrefsX.GetIntArray(sessionId+"currentGamePlays",0,noOfGames);
		int[] scoreArray = PlayerPrefsX.GetIntArray(sessionId+"scores",0,noOfGames+1);
		int[] nextLevelsArray = PlayerPrefsX.GetIntArray(sessionId+"nextLevels",0,noOfGames+1);
		

		for(int i=0;i<noOfGames+1;i++)
		{
			if(currentGamePlays[i]==1)	//if the game is played in this session
			{
				StartCoroutine(	scoreSend(sessionId,""+i,nextLevelsArray[i],scoreArray[i]));
				Debug.Log("score:"+scoreArray[i]);
				Debug.Log("level:"+nextLevelsArray[i]);
			}
		}
	}


	public int noOfScores=0;

	public void syncOfflineScore(int num,string actualId)
	{
		string sessionId = "offlineSession"+num;

		int noOfGames = PlayerPrefs.GetInt("totalGames",0);
		int[] currentGamePlays = PlayerPrefsX.GetIntArray(sessionId+"currentGamePlays",0,noOfGames+1);
		int[] scoreArray = PlayerPrefsX.GetIntArray(sessionId+"scores",0,noOfGames+1);
		int[] nextLevelsArray = PlayerPrefsX.GetIntArray(sessionId+"nextLevels",0,noOfGames+1);

		
		for(int i=0;i<noOfGames+1;i++)
		{
			if(currentGamePlays[i]==1)	//if the game is played in this session
			{
				noOfScores++;
			}
		}


		for(int i=0;i<noOfGames+1;i++)
		{
			if(currentGamePlays[i]==1)	//if the game is played in this session
			{
				StartCoroutine(	offlineScoreSend(actualId,""+i,nextLevelsArray[i],scoreArray[i]));
				Debug.Log("score:"+scoreArray[i]);
				Debug.Log("level:"+nextLevelsArray[i]);
			}
		}
	

	}







	//working fine
	IEnumerator scoreSend(string sessionId,string gameId,int nextlevel, int score)
	{
		string scoreUrl = authentication.Address+ "/api/updateScore/";
	
		WWWForm form = new WWWForm();
        
		form.AddField( "sessionId", sessionId );
		form.AddField( "gameId", gameId );
		form.AddField( "level", nextlevel);
		form.AddField( "score", score);
        
		Dictionary<string, string> headers = form.headers;
        //Dictionary<string, string> headers = new Dictionary<string, string>();
        //headers.Add("Content-Type", "application/json");
        byte[] rawData = form.data;

		WWW www = new WWW(scoreUrl, rawData, headers);
		WWW data =www;

		yield return data;
		if(data.error!=null)
		{
			Debug.Log (data.error);	
			
			if(data.error == "Cannot connect to destination host")
			{

			}

		}
		else
		{
			Debug.Log(data.text);
			ServerResponse res = JsonUtility.FromJson<ServerResponse>(data.text);
			if(res.status==0)
			{
				Debug.Log("Updated score");

			}	
			else
			{
				Debug.Log("Got an error");
			}
		}
	}


	//test it
	IEnumerator offlineScoreSend(string sessionId,string gameId,int nextlevel, int score)
	{
		string scoreUrl = authentication.Address+ "/api/updateScore/";
	
		WWWForm form = new WWWForm();
        
		form.AddField( "sessionId", sessionId );
		form.AddField( "gameId", gameId );
		form.AddField( "level", nextlevel);
		form.AddField( "score", score);
        
		Dictionary<string, string> headers = form.headers;
        byte[] rawData = form.data;

		WWW www = new WWW(scoreUrl, rawData, headers);
		WWW data =www;

		yield return data;
		if(data.error!=null)
		{
			Debug.Log (data.error);	
			
			if(data.error == "Cannot connect to destination host")
			{

			}

		}
		else
		{
			Debug.Log(data.text);
			ServerResponse res = JsonUtility.FromJson<ServerResponse>(data.text);
			if(res.status==0)
			{
				Debug.Log("Updated score");
				noOfScores--;
				if(noOfScores==0)
				{
					authentication.syncOfflineSessionsDataComplete();
				}
			}	
			else
			{
				Debug.Log("Got an error");
			}
		}
	}





}
