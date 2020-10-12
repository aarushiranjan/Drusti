using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScore : MonoBehaviour {


	public AudioClip[] scores;
	public AudioSource audioSource;
	public static BackgroundScore instance;



	// Use this for initialization
	void Awake () {
		instance = this;
		audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playRandom()
	{
		int r = Random.Range(1,scores.Length);
		audioSource.clip = scores[r];
		audioSource.Play();
	}

	public void playScore(int i)
	{
		audioSource.Stop ();
		audioSource.clip = scores[i];
		audioSource.Play();
	}

}
