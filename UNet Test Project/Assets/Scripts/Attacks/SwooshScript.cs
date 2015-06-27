using UnityEngine;
using System.Collections;

public class SwooshScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	Vector3 lookAtVector;
	float rotOffset;

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10; // select distance = 10 units from the camera
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		mousePos.z = 0;

		Quaternion rot = Quaternion.LookRotation (mousePos - transform.position, transform.TransformDirection(Vector3.forward));
		
		transform.rotation = new Quaternion(0, 0, rot.z, rot.w);
		Vector3 currentRot = transform.eulerAngles;
		currentRot.z += rotOffset - ((executeTime / Time.deltaTime) * 8f);
		transform.eulerAngles = currentRot;

		fire();
	}

	float executeTime = 0.15f, timer = 0;
	bool isFired = false;

	public void fire(){
		if(!isFired && Input.GetMouseButtonDown(0)){
			isFired = true;
			timer = executeTime;
			GetComponentInChildren<ParticleSystem>().Play();
		}

		if(isFired){
			rotOffset += 15;
			timer -= Time.deltaTime;
			if(timer <= 0){
				isFired = false;
				rotOffset = 0;
				GetComponentInChildren<ParticleSystem>().Stop();
			}
		}
	}
}
