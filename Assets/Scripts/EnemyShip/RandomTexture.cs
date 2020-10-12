using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomTexture : MonoBehaviour {

	public List<Texture> textures;
	public Material material;


	// Use this for initialization
	void Start () {
		int p = Random.Range(0,textures.Count);
		Texture tex = textures[p];
		GetComponent<Renderer>().material.mainTexture = tex;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
