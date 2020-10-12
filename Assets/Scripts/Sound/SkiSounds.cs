using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiSounds : MonoBehaviour {

	public AudioClip good;
	public AudioClip bad;
	
    public AudioSource audioSource;
	public static SkiSounds instance;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		instance =this;
	}
	
	
	public void playGood()
	{
		 audioSource.PlayOneShot(good, 1.0F);
	}

	public void playBad()
	{
	 audioSource.PlayOneShot(bad, 1.0F);
	}


}
