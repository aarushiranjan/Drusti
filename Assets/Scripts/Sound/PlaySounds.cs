using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour {

	public AudioClip[] sounds;
    public AudioSource audioSource;
	public static PlaySounds instance;
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playSound(int i)
	{
		if(i<sounds.Length)
		{
			 audioSource.PlayOneShot(sounds[i], 1.0F);
		}
	}

}
