using UnityEngine;
using System.Collections;

public class LevelGen : MonoBehaviour {

	public GameObject startingPlatform;
	public GameObject[] platforms;

	private int timer = 0;
	
	// Use this for initialization
	void Start () {
		Instantiate (startingPlatform, new Vector3 (0, 5, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer++;

		if (timer % 90 == 0) {
			Instantiate(platforms[Random.Range(0, platforms.Length - 1)], 
			            new Vector3(20, Random.Range(-2, 5), 0), Quaternion.identity);
		}
	}
}
