using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController1 : MonoBehaviour {

	public ColorMatchController cmc;
	public string gameId;
	public int level;
	public int duration;

	void loadSettings()
	{
		//load game settings
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		level = gameLevels[gid];		
		duration = gameDurations[gid];
	}

	//Called when game is over
	public void saveSettings()
	{	
		if(checkLevelPass())
		{
			level = level +1;
		}
		Debug.Log("score in game: "+cmc.score);
		Debug.Log("level in game: "+level);
		
		ScoreSender.instance.saveScore(gameId,level,cmc.score);
	}

	bool checkLevelPass()
	{
		int passScore = (int)(0.8f * duration *60 / cmc.maxColorDuration ); 
		if(cmc.score>=passScore)
			return true;
		return false;
	}

	// Use this for initialization
	void Awake () {
		loadSettings();
		cmc.maxColorDuration   = 5 - 4.5f*(level/100.0f);	
		cmc.gameTime = 60*duration;
	}
	
	void Start()
	{

	}
	// Update is called once per frame
	void Update () {
		
	}
}
