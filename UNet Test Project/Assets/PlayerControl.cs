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

	public override float GetNetworkSendInterval(){
		return 0.03f;
	}

	void FixedUpdate(){
		//updatePositions();


	}

	bool localIsLeft, localIsRight, localIsJump;

	// Update is called once per frame
	void Update () {
		updateOtherClients();

		if(!isLocalPlayer)
			return;

		if (Input.GetButtonDown("Left")){
			localIsLeft = true;
		}
		if (Input.GetButtonDown("Right")){
			localIsRight = true;
		}
		if (Input.GetButtonDown("Jump")){
			localIsJump = true;
		}

		if (Input.GetButtonUp("Left")){
			localIsLeft = false;
		}
		if (Input.GetButtonUp("Right")){
			localIsRight = false;
		}
		if (Input.GetButtonUp("Jump")){
			localIsJump = false;
		}

		Cmd_Move(localIsLeft, localIsRight, localIsJump);
	}

	void updateOtherClients(){
		if(!isServer)
			transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
	}

	[Command]
	public void Cmd_Move(bool isLeft, bool isRight, bool isJump){
		//syncPos = newPos;
		//transform.position = syncPos;

		if (isLeft){
			GetComponent<Rigidbody>().AddForce(new Vector3(-30, 0, 0));
		}
		
		if (isRight){
			GetComponent<Rigidbody>().AddForce(new Vector3(30, 0, 0));
		}
		
		if (isJump){
			GetComponent<Rigidbody>().AddForce(new Vector3(0, 40, 0));
		}

		syncPos = transform.position;
	}

	/*[Command]
	public void Cmd_Move(Vector3 newPos){
		syncPos = newPos;
		transform.position = syncPos;
	}*/

	[ClientCallback]
	void updatePositions(){

	}
}
