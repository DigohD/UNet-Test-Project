using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {

	[SyncVar]
	Vector3 syncPos;

	float lerpRate = 15;

	// Use this for initialization
	void Start () {
		syncPos = transform.position;

		if(!isServer)
			GetComponent<Rigidbody>().useGravity = false;
	}

	void FixedUpdate(){
		updatePositions();
		updateOtherClients();

		if(!isLocalPlayer)
			return;
		
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
	
	// Update is called once per frame
	void Update () {

	}

	void updateOtherClients(){
		if(!isLocalPlayer)
			transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);


	}

	[Command]
	public void Cmd_Move(Vector3 newPos){
		syncPos = newPos;
		transform.position = syncPos;
	}

	[ClientCallback]
	void updatePositions(){
		Cmd_Move(transform.position);
	}
}
