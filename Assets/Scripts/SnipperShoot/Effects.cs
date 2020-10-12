using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

	public GameObject effectPrefab;
	public float timeDelay;
	public float minLifeTime;
	public float maxLifeTime;

	// Use this for initialization
	void Start () {
		elapsedTime=timeDelay;
	}
	
	float elapsedTime;

	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime>timeDelay)
		{
			elapsedTime =0.0f;
			GameObject g = Instantiate(effectPrefab);
			g.transform.parent = this.transform;
			g.transform.position= this.transform.position;
			Destroy(g , Random.Range(minLifeTime,maxLifeTime));
		}
	}
}
