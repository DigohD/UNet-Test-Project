using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {

	[SyncVar]
	Vector3 syncPos;

	ActionScript mainAction, secondaryAction;
	Vector3 mousePosition;

	bool isJumping = false;
	float jumpDelay = 1f, jumpTimer = 0;
	float inputSyncDelay = 0.03f, inputSyncTimer;

	float lerpRate = 20;

	void Start () {
		syncPos = transform.position;
		inputSyncTimer = inputSyncDelay;

		mainAction = GetComponentInChildren<ActionScript>();

		if(!isServer)
			GetComponent<Rigidbody>().useGravity = false;
	}

	public override float GetNetworkSendInterval(){
		return 0.03f;
	}

	void FixedUpdate(){

	}

	bool localIsLeft, localIsRight, localIsJump;

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

		mousePosition = Input.mousePosition;
		
		inputSyncTimer -= Time.deltaTime;
		if(inputSyncTimer <= 0){
			inputSyncTimer += inputSyncDelay;
			Cmd_Move(localIsLeft, localIsRight, localIsJump);
			Cmd_MouseEvents(Input.GetMouseButton(0), Input.GetMouseButton(1), Input.GetMouseButton(2), mousePosition);
		}
	}

	void updateOtherClients(){
		if(!isServer)
			transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
	}

	[Command]
	public void Cmd_MouseEvents(bool leftDown, bool rightDown, bool middleDown, Vector3 mousePos){

		if(mainAction != null)
			mainAction.run(leftDown, mousePos);
		if(secondaryAction != null)
			secondaryAction.run(rightDown, mousePos);
	}

	[Command]
	public void Cmd_Move(bool isLeft, bool isRight, bool isJump){
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
			jumpTimer -= inputSyncDelay;
			if(jumpTimer <= 0){
				isJumping = false;
				jumpTimer = 0;
			}
		}

		syncPos = transform.position;
	}
}
