using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundEffects : MonoBehaviour {


	public float minRotateSpeed;
	public float maxRotateSpeed;

	public float minResizeSpeed;
	public float maxResizeSpeed;




	public bool Effect1;
	public bool Effect2;

	public bool Effect3;
	public bool Effect4;

	public float rotateSpeed1;
	public float rotateSpeed2;
	public float resizeSpeed1;
	public float resizeSpeed2;


	public List<Sprite> sprites;
	public Image img;

	// Use this for initialization
	void Start () {
		//
		RectTransform rt = GetComponent<RectTransform>();
		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.one;
		rt.sizeDelta = Vector2.zero;
		//


		img.sprite = sprites[Random.Range(0,sprites.Count)];
		Effect1 = Random.Range(0.0f,1.0f)>0.5f;
		Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 1.0f, 1f);
		color.a  = 0.8f;
		img.color = color;
		if(Effect1)
		{
				rotateSpeed1 = Random.Range(minRotateSpeed,maxRotateSpeed);
		}
		else
		{	Effect2=true;
			rotateSpeed2 = Random.Range(minRotateSpeed,maxRotateSpeed);
		}
		
		Effect3 = Random.Range(0.0f,1.0f)>0.5f;
		if(Effect3)
		{
			resizeSpeed1 = Random.Range(minResizeSpeed,maxResizeSpeed);
			transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		}
		else
		{
			Effect4 = true;
		}
		if(Effect4)
		{
			resizeSpeed2 = -Random.Range(minResizeSpeed,maxResizeSpeed);
			transform.localScale = new Vector3(1.5f,1.5f,1.5f);
		}	
	}
	
	// Update is called once per frame
	void Update () {
		if(Effect1)
		{
			transform.Rotate(Vector3.forward * Time.deltaTime*rotateSpeed1);
		}
		if(Effect2)
		{
			transform.Rotate(-Vector3.forward * Time.deltaTime*rotateSpeed2);
		}
		if(Effect3)
		{
			transform.localScale = transform.localScale + new Vector3(1,1,1)*resizeSpeed1*Time.deltaTime;
		}
		if(Effect4)
		{
			transform.localScale = transform.localScale + new Vector3(1,1,1)*resizeSpeed2*Time.deltaTime;
		}
	}
}
