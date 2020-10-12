using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public int Level;

	public Vector3 CameraFarPosition;
	public Vector3 CameraClosePostion;
	public float checkpointSpeedMax;
	public float checkpointSpeedMin;
	public float checkpointDispMax;
	public float checkpointDispMin;
	public float ssmDepthMin;
	public float ssmDepthMax;


	public GameObject camera;
	public SpaceRaceController src;
	public SpaceShipMovement ssm;
	public string gameId;

	void Awake()
	{
		//10 Visualacuities
		//50 Levels

		int totalLevels = 100;
		//get Level
		
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		Level = gameLevels[gid];	
		//get gameTime
	
		//Based on level do the following
		//Camera distance
		Vector3 camPos = CameraClosePostion + ((float)Level/totalLevels)*(CameraFarPosition- CameraClosePostion);
		camera.transform.position = camPos;
		//Speed of the checkpoints
		src.checkpointSpeed = checkpointSpeedMin + ((float)Level/totalLevels)*(checkpointSpeedMax - checkpointSpeedMin);
		src.maxXdisplacement = checkpointDispMin + ((float)Level/totalLevels)*(checkpointDispMax-checkpointDispMin);

	
		//ssm
		ssm.depth = ssmDepthMin + ((float)Level/totalLevels)*(ssmDepthMax-ssmDepthMin);

	}


	// Use this for initialization
	void Start () {
		BackgroundScore.instance.playRandom ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	


}
