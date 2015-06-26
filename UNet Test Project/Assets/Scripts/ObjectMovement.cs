using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {

	public float speed = 50.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-Time.smoothDeltaTime * speed, 0, 0, Space.World);
	}
}
