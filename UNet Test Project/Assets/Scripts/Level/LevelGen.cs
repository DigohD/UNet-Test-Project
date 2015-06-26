using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LevelGen : NetworkBehaviour {

	public GameObject startingPlatform;
	public GameObject[] platforms;

	private int timer = 0;

	public NetworkManager network;
	
	// Use this for initialization
	void Start () {
		Instantiate (startingPlatform, new Vector3 (0, 5, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer++;

		if (!isServer) 
			return;

		if (timer % 90 == 0) {
			GameObject go = (GameObject)Instantiate(platforms[Random.Range(0, platforms.Length - 1)], 
			            new Vector3(20, Random.Range(-2, 5), 0), Quaternion.identity);
			NetworkServer.Spawn(go);
		}
	}
}
