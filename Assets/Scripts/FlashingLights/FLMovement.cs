using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FLMovement : MonoBehaviour {


	public Image container;
	public bool gameCompleted;
	public float speed;
	public Text scoreText;
	public int direction;
	public float gameTime;
	public GameObject BackButton;
	public GameObject ExitButton;
	public GameObject gameOverPanel;
	public Text gameOverScore;
	float width;
	float height;
	public string gameId;
	public int level;
	// Use this for initialization
	void Start () {

		width = container.GetComponent<RectTransform>().rect.width;
		height = container.GetComponent<RectTransform>().rect.height;
		//width = Screen.width;	
		//height = Screen.height;
		direction = Random.Range (0, 4);
		Debug.Log (width);
		scoreText.text = "Score: 0";

		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int[] gameDurations = PlayerPrefsX.GetIntArray("gameDurations");
		
		level = gameLevels[gid];
		gameTime = gameDurations[gid]*60;
		BackgroundScore.instance.playRandom ();
		
		speed = 80 + 120*(level/100.0f);
		
	}

	float elapsedGameTime;
	// Update is called once per frame
	void Update () {
		if (!gameCompleted) {
			move ();
			limits ();
			input ();
		
			elapsedGameTime += Time.deltaTime;
			if (elapsedGameTime > gameTime)
				gameOver ();
		}
	}

	void changeDirection()
	{
		int newDirection = Random.Range (0, 4);
		while (newDirection == direction) {
			newDirection = Random.Range (0, 4);
		}
		direction = newDirection;
	}


	int score;
	void input()
	{
		if(Input.GetKeyUp(KeyCode.RightArrow)||ArrowKeys.instance.right)
		{
			ArrowKeys.instance.reset ();
			if (direction == 0) {
				changeDirection ();
				PlaySounds.instance.playSound (0);
				score++;
				scoreText.text = "Score: " + score;
			}
			else {

				PlaySounds.instance.playSound (1);
			}
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow)||ArrowKeys.instance.left)
		{
			ArrowKeys.instance.reset ();
			if (direction == 2) {
				changeDirection ();
				PlaySounds.instance.playSound (0);	
				score++;
				scoreText.text = "Score: " + score;
			}
			else {

				PlaySounds.instance.playSound (1);
			}
		}
		if(Input.GetKeyUp(KeyCode.UpArrow)||ArrowKeys.instance.up)
		{
			ArrowKeys.instance.reset ();
			if (direction == 1) {
				changeDirection ();
				PlaySounds.instance.playSound (0);	
				score++;
				scoreText.text = "Score: " + score;
			}
			else {

				PlaySounds.instance.playSound (1);
			}
		}
		if(Input.GetKeyUp(KeyCode.DownArrow)||ArrowKeys.instance.down)
		{
			ArrowKeys.instance.reset ();
			if (direction == 3) {
				changeDirection ();
				PlaySounds.instance.playSound (0);	
				score++;
				scoreText.text = "Score: " + score;
			}
			else {

				PlaySounds.instance.playSound (1);
			}
		}

	}


	void move()
	{
		if (direction == 0) {
			this.transform.position += speed * Vector3.right * Time.deltaTime;
		}
		if (direction == 1) {
			this.transform.position += speed * Vector3.up * Time.deltaTime;
		}
		if (direction == 2) {
			this.transform.position += speed * Vector3.left * Time.deltaTime;
		}
		if (direction == 3) {
			this.transform.position += speed * Vector3.down * Time.deltaTime;
		}
	}

	void limits()
	{
		//Debug.Log (transform.localPosition);

		//if (transform.localPosition.x > (width / 2)) {
		//	direction = 2;
		//}
		//if (transform.localPosition.x < -width / 2) {
		//	direction = 0;
		//}
		//if (transform.localPosition.y > height / 2) {
		//	direction = 3;
		//}
		//if (transform.localPosition.y < -height / 2) {
		//	direction = 1;
		//}


		if (transform.localPosition.x > (width / 2))
		{
			direction = 2;
		}
		if (transform.localPosition.x < -width / 2)
		{
			direction = 0;
		}
		if (transform.localPosition.y > height / 2)
		{
			direction = 3;
		}
		if (transform.localPosition.y < -height / 2)
		{
			direction = 1;
		}

	}

	void gameOver()
	{
		Time.timeScale = 0.0f;
		//Show game over 
		//BackButton.SetActive(false);
		//ExitButton.SetActive(true);
		gameOverPanel.SetActive(true);
		gameOverScore.text = "Score :" + score;
		gameCompleted = true;
        Debug.Log("Score is : " + score);
        Debug.Log("Game Time is : " + gameTime);
		if(score>gameTime)
		{
			level++;
		}

		ScoreSender.instance.saveScore(gameId,level,score);

	
	}

}
