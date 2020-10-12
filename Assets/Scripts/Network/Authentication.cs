using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;




public class AuthRequest
{
	public string username;
	public string password;
}

[System.Serializable]
public class ServerResponse
{
    public int status;
    public string message;
}




public class Authentication : MonoBehaviour {

	public static Authentication instance;

	public InputField username;
	public InputField password;
	public Text errorText;
	
	public string uname;
	public string pass;
	public string Address;	//Live winv.apps.iitd.ac.in
	public string token;
	public string session;

	public bool offline;
	





	void Awake() {
		// PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(transform.gameObject);

    }

	void Start()
	{
		username.text = PlayerPrefs.GetString("username","");
		password.text = PlayerPrefs.GetString("password","");
		instance = this;
		offline = false;
	}


	
	public void login()
	{
		string loginURL = Address+ "/api/applogin/"; //api
	
		WWWForm form = new WWWForm();
        form.AddField( "username", username.text ); //parameter1 
		form.AddField( "password", password.text); //parameter2
		form.AddField("DeviceModel", SystemInfo.deviceModel.ToString());
		form.AddField("Name",SystemInfo.deviceName.ToString());
		form.AddField("Type",SystemInfo.deviceType.ToString());
		form.AddField("UniqueIdentifier",SystemInfo.deviceUniqueIdentifier.ToString()); //this 
		form.AddField("OperatingSystem",SystemInfo.operatingSystem);
		form.AddField("AppVersion",Application.version);//
        Dictionary<string, string> headers = form.headers; 
        byte[] rawData = form.data;

		WWW www = new WWW(loginURL, rawData, headers);
        Debug.Log("Sending auth request");
		StartCoroutine(WaitForRequest(www));
	}
		




	public void newSession()
	{
		Debug.Log("Starting new session");
		if(offline)
		{
			//add a new session in the sessions array.
			int noOfSessions = PlayerPrefs.GetInt("noOfSessions",0);
			
			string sessid = "offlineSession"+noOfSessions;
			PlayerPrefs.SetString("session",sessid);
			PlayerPrefs.SetString(sessid+"startTime",System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			PlayerPrefs.SetString(sessid+"endTime",System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			session = sessid;	
			noOfSessions++;
			PlayerPrefs.SetInt("noOfSessions",noOfSessions);
			Debug.Log("no of sessions: "+ noOfSessions);
			
			downloadSettings();
			



			return;
		}

		string sessionUrl = Address+ "/api/newSession/";
	
		WWWForm form = new WWWForm();
        form.AddField( "token", token );
		form.AddField("startTime",System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		form.AddField("endTime",System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        Dictionary<string, string> headers = form.headers;
        byte[] rawData = form.data;

		WWW www = new WWW(sessionUrl, rawData, headers);
        StartCoroutine(WaitForRequest2(www));
	}


	//this should be called after the sync data
	public void downloadSettings()
	{
		Debug.Log("Starting to download settings");
		if(offline)
		{
			//default or already existing settings
			//except the levels and gameplays
			Debug.Log("Loading offline settings");
			int noOfSessions = 	PlayerPrefs.GetInt("noOfSessions",0);
			if(noOfSessions>1)
			{
				Debug.Log("Loading settings from previous offline session");
				string sessid = "offlineSession"+(noOfSessions-2);
				int[] gameLevels = PlayerPrefsX.GetIntArray(sessid+"nextLevels");
				PlayerPrefsX.SetIntArray("gameLevels",gameLevels);
			}				
		
			//clear the currentgameplays  
			int noOfGames = PlayerPrefs.GetInt("totalGames",0);
			int[] currentGamePlays = new int[noOfGames+1];
			string sessionId = "offlineSession"+(noOfSessions);
			PlayerPrefsX.SetIntArray(sessionId+"currentGamePlays",currentGamePlays);
		
			

			return;
		}

		string settingsURL = Address+ "/api/currentSettings/";
	
		WWWForm form = new WWWForm();
        form.AddField( "token", token );
        Dictionary<string, string> headers = form.headers;
        byte[] rawData = form.data;

		WWW www = new WWW(settingsURL, rawData, headers);
        StartCoroutine(WaitForRequest4(www));
	}


	//for login
	IEnumerator WaitForRequest(WWW data)
	{
		yield return data;
		if(data.error!=null)
		{
			Debug.Log (data.error);	
			errorText.text=data.error;
		
			//security measure
			
			//by making this offline anyone without a valid auth can use the game.
			//avoid that

			//if offline
			if(data.error == "Cannot connect to destination host")
			{
				Debug.Log("offline mode activated");
				offline=true;
				newSession();
				//nxt scene
				SceneManager.LoadScene("MainScene");
			}

		}
		else
		{
			Debug.Log(data.text);
			Debug.Log("Auth successful");
			ServerResponse res = JsonUtility.FromJson<ServerResponse>(data.text);
			if(res.status==0)
			{
				//save the username and password in player prefs
				PlayerPrefs.SetString("username", username.text);
				uname = username.text;
				PlayerPrefs.SetString("password", password.text);
				pass = password.text;
				PlayerPrefs.SetString("playerToken",res.message);	
				token = res.message;

				//now start new session
				newSession();

			}	
			else
			{
				errorText.text= res.message;
			}
		}
	}

	//for new session
	IEnumerator WaitForRequest2(WWW data)
	{
		yield return data;
		if(data.error!=null)
		{
			Debug.Log (data.error);	
			errorText.text=data.error;

			//if offline
			if(data.error == "Cannot connect to destination host")
			{
				Debug.Log("offline mode activated");
				offline=true;
				newSession();
			}

		
		}
		else
		{
			Debug.Log(data.text);
			Debug.Log("New session id recieved");
			
			ServerResponse res = JsonUtility.FromJson<ServerResponse>(data.text);
			if(res.status==0)
			{
				//save the username and password in player prefs
				PlayerPrefs.SetString("username", username.text);
				PlayerPrefs.SetString("password", password.text);
				PlayerPrefs.SetString("session",res.message);	
				session = res.message;

				//Sync data
				syncData();
			}	
			else
			{
				errorText.text= res.message;
			}
		}
	}

	IEnumerator WaitForRequest4(WWW data)
	{
		yield return data;
		if(data.error!=null)
		{
			Debug.Log (data.error);	
			errorText.text=data.error;

			//if offline
			if(data.error == "Cannot connect to destination host")
			{
				Debug.Log("offline mode activated");
				offline=true;
				newSession();
			}
		}
		else
		{
			Debug.Log(data.text);
			ServerResponse res = JsonUtility.FromJson<ServerResponse>(data.text);
			if(res.status==0)
			{
				Debug.Log(res.message);
				string[] strArr = null;
				strArr = res.message.Split('@');
				
				string[] gameIds = strArr[0].Split('$');
				string[] levels = strArr[1].Split('$');
				string[] durations = strArr[2].Split('$');

                Debug.Log(gameIds.Length);



				int totalGames = int.Parse(strArr[3]);
				int maxGames = int.Parse(strArr[4]);
				
				float screenSize = float.Parse( strArr[5]);
				PlayerPrefs.SetFloat("screenSize",screenSize);
				int m = int.Parse(strArr[6]);
				if(m==1)
					PlayerPrefs.SetInt("mode",0);
				else
					PlayerPrefs.SetInt("mode",1);

				// Set which games should the child play
				int[] gamePlays = new int[totalGames+1];
				int[] gameLevels = new int[totalGames+1];
				int[] gameDurations = new int[totalGames+1];

				for(int i =0;i<gameIds.Length;i++)
				{
                    Debug.Log(gameIds[i]);
                    gamePlays[int.Parse(gameIds[i])] = 1;   
					int l = int.Parse(levels[i]);
                    gameLevels[int.Parse(gameIds[i])]=l;
                    Debug.Log(l);
                    int d = int.Parse(durations[i]);
					gameDurations[int.Parse(gameIds[i])] = d;
				}
                
                PlayerPrefs.SetInt("totalGames",totalGames);
				PlayerPrefs.SetInt("maxGames",maxGames);
				PlayerPrefsX.SetIntArray("gamePlays",gamePlays);
				PlayerPrefsX.SetIntArray("gameLevels",gameLevels);
				PlayerPrefsX.SetIntArray("gameDurations",gameDurations);
                


                // Save these arrays


                // for(int i = 0; i<gameIds.Length;i++)
                // {
                // 	//Setting which games to play
                // 	string g = "game"+gameIds[i];
                // 	PlayerPrefs.SetInt(g,1);
                // 	//Setting for levels of each game
                // 	string gg = "gameLevel"+gameIds[i];
                // 	int l = int.Parse(levels[i]);
                // 	int il = PlayerPrefs.GetInt(gg,0);
                // 	if(l>il)
                // 		PlayerPrefs.SetInt(gg,l);
                // 	//Setting for durations of each game
                // 	string ggg = "gameDuration"+gameIds[i];
                // 	int d = int.Parse(durations[i]);
                // 	PlayerPrefs.SetInt(ggg,d);
                // }


                //now start next scene
                SceneManager.LoadScene("MainScene");


			}	
			else
			{
				errorText.text= res.message;
			}
		}

	}
	

	int noOfSessionsToCreate=0;
	string[] sessionIds;
	public void syncData()
	{
		int noOfSessions = PlayerPrefs.GetInt("noOfSessions",0);
		noOfSessionsToCreate = noOfSessions;
		sessionIds = new string[noOfSessions];
		
		if(noOfSessions==0)
		{
			Debug.Log("No offline sessions");
			downloadSettings();		
			return;	
		}
		
		Debug.Log("Offline sessions: "+noOfSessions);
		
		for(int i=0;i<noOfSessions;i++)
		{
			StartCoroutine(createOfflineSession(i));
		}
		
		//After syncing data we download the settings
		//downloadSettings();
		
	}

	public void sessionsCreationComplete()
	{	
		Debug.Log("sessions creation complete");
		for(int i=0;i<sessionIds.Length;i++)
		{
			Debug.Log(sessionIds[i]);
		}
		syncOfflineSessionsData();
	}



	IEnumerator createOfflineSession(int i)
	{
		string sessionUrl = Address+ "/api/newSession/";
		string sessid = "offlineSession"+i;
			

		string st =	PlayerPrefs.GetString(sessid+"startTime",System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		string et =	PlayerPrefs.GetString(sessid+"endTime",System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

		WWWForm form = new WWWForm();
        form.AddField( "token", token );
		form.AddField("startTime",st);
		form.AddField("endTime",et);
			

        Dictionary<string, string> headers = form.headers;
        byte[] rawData = form.data;

		WWW www = new WWW(sessionUrl, rawData, headers);
    	WWW data = www;
		yield return data;
	
		if(data.error!=null)
		{
		
		}
		else
		{
			Debug.Log("New session id recieved");
			
			ServerResponse res = JsonUtility.FromJson<ServerResponse>(data.text);
			if(res.status==0)
			{
				sessionIds[i]=res.message;
				noOfSessionsToCreate--;
				if(noOfSessionsToCreate==0)
				{
					sessionsCreationComplete();
				}
			}	
			else
			{
				errorText.text= res.message;
			}
		}
	
	}

	public void syncOfflineSessionsData()
	{
		Debug.Log("syncing scores of the sessions");
		for(int i =0;i<sessionIds.Length;i++)
		{
			ScoreSender.instance.syncOfflineScore(i,sessionIds[i]);
		}
		if(ScoreSender.instance.noOfScores==0)
			syncOfflineSessionsDataComplete();
	}



	public void syncOfflineSessionsDataComplete()
	{
		Debug.Log("syncing scores of offline session complete");
		PlayerPrefs.SetInt("noOfSessions",0);

		//remove all player prefs which are not required
		
		
		PlayerPrefs.DeleteAll();
		
		//Important variables to save
		PlayerPrefs.SetString("username", uname);
		PlayerPrefs.SetString("password", pass);
		PlayerPrefs.SetString("playerToken",token);	
		PlayerPrefs.SetString("session",session);

		downloadSettings();
	}




}
	