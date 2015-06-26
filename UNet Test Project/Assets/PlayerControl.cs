using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {

	[SyncVar]
	Vector3 syncPos;

	// Use this for initialization
	void Start () {
		syncPos = transform.position;
	}

	void FixedUpdate(){
		updatePositions();
		updateOtherClients();
	}
	
	// Update is called once per frame
	void Update () {
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

	void updateOtherClients(){
		if(!isLocalPlayer)
			transform.position = syncPos;
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
