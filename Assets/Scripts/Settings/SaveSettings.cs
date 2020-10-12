using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;


public class SaveSettings : MonoBehaviour {
	public Dropdown screenSize;
	public Dropdown visualAcuity;
	public InputField otherScreenSize;
	public Slider mode;
	public Slider music;
	public Slider sound;

	// Use this for initialization
	void Start () {
		int sc = PlayerPrefs.GetInt("screenSize",0);
		int va = PlayerPrefs.GetInt("visualAcuity",0);
		int mu = PlayerPrefs.GetInt("music",1);
		int so = PlayerPrefs.GetInt("sound",1);
		int mo = PlayerPrefs.GetInt("mode",1);
		


		screenSize.value=sc;
		visualAcuity.value = va;
		music.value = mu;
		sound.value = so;
		mode.value = mo;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Save()
	{
		PlayerPrefs.SetInt("screenSize",screenSize.value);
		PlayerPrefs.SetInt("visualAcuity",visualAcuity.value);
		int m=0;
		if(music.value>0.5f)
			m=1;
		int s=0;
		if(sound.value>0.5f)
			s=1;
		int p =0;
		if(mode.value>0.5f)
			p=1;

		PlayerPrefs.SetInt("music",m);
		PlayerPrefs.SetInt("sound",s);
		PlayerPrefs.SetInt("mode",p);
		
		int va = visualAcuity.value;
		int a = 800;
		switch (va)
		{
			case 0 :
				a= 800;
				break;
			case 1 :
				a= 400;
				break;
			case 2 :
				a= 200;
				break;
			case 3 :
				a= 100;
				break;
			case 4 :
				a= 80;
				break;
			case 5 :
				a= 60;
				break;
			case 6 :
				a= 40;
				break;
			case 7 :
				a= 30;
				break;
			case 8 :
				a= 25;
				break;
			case 9 :
				a= 20;
				break;
		}
		PlayerPrefs.SetInt("currentAcuity", a);
	}

}
