using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placement : MonoBehaviour {

	//we know the height of screen is 70
	public GameObject randomCharacter;
	public string gameId;

	public Image Container;

	float scale;

	// Use this for initialization
	void Start () {
		int gid = int.Parse(gameId);
		int[] gameLevels = PlayerPrefsX.GetIntArray("gameLevels");
		int level = gameLevels[gid];		
		scale = GetScale.instance.getScaleFromLevel(level);
	}
	

	public int place(int target)
	{
		int counter =0;
		while(counter==0)
		{
			float s = scale;
			Debug.Log("scale : "+s);
			int nr = (int)(110/s);
			// if(nr>10)
			// 	nr=10;
			int numberOfRows = nr;
			int numberOfColumns = (int)(numberOfRows*1.6f);
			
			if(numberOfColumns==1)
				numberOfColumns=2;
			if(numberOfColumns%2==1)
				numberOfColumns++;

			Debug.Log("nor: "+numberOfRows);
			Debug.Log("noc: "+numberOfColumns);
			
			//
			int n = numberOfRows/2 + numberOfRows/10;
			float m = numberOfColumns/2 -0.5f ;
			
			List<GameObject> chars = new List<GameObject>();
			List<Vector3> positions = new List<Vector3> ();

			float thres = s;
			int maxHits=5;	

			for(int i=0;i<numberOfRows;i++)
			{
				for(int j=0;j<numberOfColumns;j++)
				{	
					float x = 0;
					float y = 0;
					bool overlapping = true;
					int c = 0;
					bool yo = true;

					while (overlapping) {
						overlapping = false;
						x = Random.Range (-m * s, (numberOfColumns * s) - (m * s));
						y = Random.Range (-n * s, (numberOfRows * s) - (n * s));


						Debug.Log("M is" + m*s);
						Debug.Log("N is " + n*s);
						Debug.Log("X is "+x);
						Debug.Log("Y is "+y);
						// -113 se +113 X
						//-22 se +22
						//x = Random.Range(-Container.GetComponent<RectTransform>().rect.xMin , (numberOfColumns * s) - (m * s));
						//y = Random.Range(-Container.GetComponent<RectTransform>().rect.yMin, (numberOfRows * s) - (n * s));

						for (int k = 0; k < positions.Count; k++) {
							float X = positions [k].x;
							float Y = positions [k].y;
							if (Mathf.Abs (x - X) < thres) {
								if (Mathf.Abs (y - Y) < thres) {
									overlapping = true;
									break;

								}
							}
						}

						c = c + 1;
						if (c > maxHits) {
							yo = false;
							break;
						}
					}
		
					if (yo) 
					{

						Debug.Log("im inside");

						GameObject g = Instantiate (randomCharacter);
						g.transform.position = new Vector3 (x, y, 0);
						chars.Add (g);
						if (target == g.GetComponent<AssignChar> ().value)
							counter++;
						positions.Add (g.transform.position);
						
					}
				}
			}
			
			//again if counter ==0
			if(counter==0)

			{
				Debug.Log("earea");
				for(int i=0;i<chars.Count;i++)
				{
					Destroy(chars[i]);
				}
			}
		}
		return counter;
	}

	// Update is called once per frame
	void Update () {
	}
}
