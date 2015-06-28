using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SwooshScript : ActionScript {

	[SyncVar]
	Vector3 lookRotation;

	[SyncVar]
	bool isFired = false;

	float lerpRate = 15;

	Vector3 lookAtVector;
	float rotOffset, rotPermanentOffset;

	float executeAngle = 30f, swingSpeed = 5f, timer = 0;


	void Start(){

	}

	void Update(){
		updateOtherClients();
	}

	void updateOtherClients(){
		if(!isServer){
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, lookRotation, Time.deltaTime * lerpRate);
			if(isFired){
				if(!GetComponentInChildren<ParticleSystem>().isPlaying)
					GetComponentInChildren<ParticleSystem>().Play();
			}else{
				if(GetComponentInChildren<ParticleSystem>().isPlaying)
					GetComponentInChildren<ParticleSystem>().Stop();
			}
		}
	}

	public override void run(bool isActivatorActive, Vector3 mousePos){
		mousePos.z = 10; // select distance = 10 units from the camera
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		mousePos.z = 0;

		//Debug.LogWarning(lookAtVector.ToString());

		if(!isFired){
			lookAtVector = mousePos;
		}
		
		Quaternion rot = Quaternion.LookRotation (lookAtVector - transform.position, transform.TransformDirection(Vector3.forward));

		transform.rotation = new Quaternion(0, 0, rot.z, rot.w);
		Vector3 currentRot = transform.eulerAngles;
		currentRot.z += rotOffset;
		transform.eulerAngles = currentRot;
		lookRotation = currentRot;

		fire(isActivatorActive);
	}



	public void fire(bool isActivatorActive){
		if(!isFired && isActivatorActive){
			isFired = true;
			GetComponentInChildren<ParticleSystem>().Play();
			GetComponent<AudioSource>().Play();
		}

		if(isFired){
			rotOffset += swingSpeed;
			if(rotOffset >= executeAngle){
				isFired = false;
				GetComponentInChildren<ParticleSystem>().Stop();
				rotOffset = (-executeAngle / 2) - 1;
			}
		}
	}
}
