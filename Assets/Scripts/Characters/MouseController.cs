using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour {

	public float gameTime;
	public AssignChar characterPrefab;
	public int target;
	public Text heading;
	public Text heading2;
	public Text gameOverText;
	public Placement placement;
	public GameObject charExplosion;
	public Text remaining;

	public int noOfTargets;
	public GameObject gameOverPanel;
	public Text gameOverScore;
	public int Score;
	public string gameId;
	public int level;
	// Use this for initialization
	void Start () {
		
		target = Random.Range(0,characterPrefab.sprites.Count);
		heading.text = "Find "+target;
		heading2.text = "Find "+target;
		heading2.gameObject.SetActive(false);
		StartCoroutine(After5Seconds());

		//get gameTime
		// gameTime = PlayerPrefs.GetFloat("charGame1Time", 30);
		//sound
		BackgroundScore.instance.playRandom();
	
		//load game settings
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		level = gameLevels[gid];		
		gameTime = gameDurations[gid]*60;
	
	
	}
	
	public float elapsedGameTime;


	IEnumerator After5Seconds()
    {
        yield return new WaitForSeconds(2);
		heading.gameObject.SetActive(false);
		heading2.gameObject.SetActive(true);
        noOfTargets =placement.place(target);
		remaining.text = "Remaining: "+ noOfTargets;

    }
	

	void gameOver()
	{
		gameOverPanel.SetActive(true);
		gameOverScore.text = "No of hits: " + Score;
		Time.timeScale = 0.0f;
		if(Score>gameTime/3)		//should take max 3 seconds to find a number
		{
			level=level+1;
		}
		ScoreSender.instance.saveScore(gameId,level,Score);
	}




	 void Update()
		{
			elapsedGameTime += Time.deltaTime;
			if(elapsedGameTime>gameTime)
			{
                if(!gameOverPanel.activeInHierarchy)
				    gameOver();
			}
			if (Input.GetMouseButtonDown(0))
			{
				
				RaycastHit hitInfo = new RaycastHit();
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
				if (hit) 
				{
		//			Debug.Log("Hit " + hitInfo.transform.gameObject.name);
					if (hitInfo.transform.gameObject.tag == "Character")
					{
		//				Debug.Log ("It's working!");
						AssignChar ac= hitInfo.transform.gameObject.GetComponent<AssignChar>();
						if(ac.value==target)
						{
							GameObject g = Instantiate(charExplosion);
							g.transform.position = hitInfo.transform.gameObject.transform.position;
							Destroy(hitInfo.transform.gameObject);
							Sounds.instance.playExplosion();
							Destroy(g,2);
							noOfTargets--;
							Score++;
							remaining.text = "Remaining: "+ noOfTargets;
							if(noOfTargets==0)
								waveOver();
						}

					} else {
		//				Debug.Log ("nopz");
					}
				} else {
		//			Debug.Log("No hit");
				}
		//		Debug.Log("Mouse is down");
			} 
		}


	void waveOver()
	{
		//Explode all 
		RandomChar[] allObjects = UnityEngine.Object.FindObjectsOfType<RandomChar>() ;
		foreach(RandomChar go in allObjects)
		{
			GameObject g = Instantiate(charExplosion);
			g.transform.position = go.gameObject.transform.position;
			Destroy(g,2);
			Destroy(go.gameObject);
			Sounds.instance.playExplosion();

		}
		elapsedGameTime = elapsedGameTime - 2.0f;
		//gameOver
		
		//ScoreSender.instance.sendScore(5,100);
		//gameOverText.gameObject.SetActive(true);

		//restart wave
		target = Random.Range(0,characterPrefab.sprites.Count);
		heading.text = "Find "+target;
		heading2.text = "Find "+target;
		heading.gameObject.SetActive(true);
		heading2.gameObject.SetActive(false);
		StartCoroutine(After5Seconds());

	}
}
