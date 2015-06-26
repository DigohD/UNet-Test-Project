using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LobbyScript : NetworkLobbyManager {

	public Transform entryPos;
	public GameObject lobbyPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControlID){
		GameObject newLobbyPlayer = (GameObject) Instantiate(lobbyPlayer, lobbyPlayer.transform.position, Quaternion.identity);
		newLobbyPlayer.transform.SetParent(entryPos, false);
		//newLobbyPlayer.GetComponent<NetworkLobbyPlayer>().playerControllerId = playerControlID;
		//newLobbyPlayer.GetComponent<NetworkLobbyPlayer>().connectionToClient = conn;
		NetworkServer.Spawn(newLobbyPlayer);
		return null;
	}
}
