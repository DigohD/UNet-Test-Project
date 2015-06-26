using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Left")){
			GetComponent<Rigidbody>().AddForce(new Vector3(100, 0, 0));
		}

		if (Input.GetButtonDown("Right")){
			GetComponent<Rigidbody>().AddForce(new Vector3(-100, 0, 0));
		}

		if (Input.GetButtonDown("Jump")){
			GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0));
		}
	}
}
