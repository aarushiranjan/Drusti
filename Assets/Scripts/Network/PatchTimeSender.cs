using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchTimeSender : MonoBehaviour {

	public static PatchTimeSender instance;


	// Use this for initialization
	void Start () {
		instance	= this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	
	
	public void sendPatchTime(int t)
	{

		if(Authentication.instance.offline)
		{
			//add patchTime
			int noOfSessions = PlayerPrefs.GetInt("noOfSessions",0);
			string s = "session"+noOfSessions+"patchTime";
			PlayerPrefs.SetInt(s,t);
			Debug.Log("saved patchtime: "+ s +" : "+t);
			return;
		}




		string loginURL =  Authentication.instance.Address+ "/api/patchTime/";
	
		WWWForm form = new WWWForm();
        form.AddField( "sessionId", Authentication.instance.session);
		form.AddField( "patchTime", t.ToString());
        Dictionary<string, string> headers = form.headers;
        byte[] rawData = form.data;

        Debug.Log(loginURL);

		WWW www = new WWW(loginURL, rawData, headers);
        StartCoroutine(WaitForRequest(www,t));
	}
		
	
	IEnumerator WaitForRequest(WWW data,int pt)
	{
		yield return data;
		if(data.error!=null)
		{
			Debug.Log (data.error);	
			//if offline
			if(data.error == "Cannot connect to destination host")
			{
				Debug.Log("offline mode activated");
				Authentication.instance.offline=true;
				Authentication.instance.newSession();
				sendPatchTime(pt);
			}

		}
		else
		{
			Debug.Log(data.text);
			ServerResponse res = JsonUtility.FromJson<ServerResponse>(data.text);
			if(res.status==0)
			{
				//now start the session

			}	
			else
			{
				
			}
		}
	}




	




}
