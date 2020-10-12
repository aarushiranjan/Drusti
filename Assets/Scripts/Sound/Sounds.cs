using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {
 	
	public AudioClip explosion;
    public AudioSource audioSource;
	public static Sounds instance;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
		instance = this;
	}

	public void playExplosion()
	{
       audioSource.PlayOneShot(explosion, 0.7F);
	}

}
