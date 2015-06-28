using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class ActionScript : NetworkBehaviour {

	public abstract void run(bool isActivatorActive, Vector3 mousePosition);
}
