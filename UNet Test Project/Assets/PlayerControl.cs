using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {

	[SyncVar]
	Vector3 syncPos;

	bool isJumping = false;
	float jumpDelay = 1f, jumpTimer = 0;

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
			if(!(GetComponent<Rigidbody>().velocity.x < -3))
				GetComponent<Rigidbody>().AddForce(new Vector3(-30, 0, 0));
		}
		
		if (isRight){
			if(!(GetComponent<Rigidbody>().velocity.x > 3))
				GetComponent<Rigidbody>().AddForce(new Vector3(30, 0, 0));
		}
		
		if (isJump && !isJumping){
			GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0));
			isJumping = true;
			jumpTimer = jumpDelay;
			GetComponentInChildren<ParticleSystem>().Play();
		}else if(isJumping){
			jumpTimer -= Time.deltaTime;
			if(jumpTimer <= 0){
				isJumping = false;
				jumpTimer = 0;
			}
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
