using UnityEngine;
using System.Collections;

public class ObjectDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.x < -20)
			Destroy(this.gameObject);
	}
}
