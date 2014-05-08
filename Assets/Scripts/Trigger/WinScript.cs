using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	public Light winLight;
	public int playersInside;

	void Start() {
		DontDestroyOnLoad (this);
	}

	void OnTriggerEnter (Collider collider) {

		//Player walked into the platform
		if (collider.transform.name == "FirstPersonController(Clone)") {
			playersInside ++;
			//If both players are in the platform activate win animations
            if (playersInside == (((GameManagerC)GameObject.FindGameObjectWithTag("GameManager").GetComponent("GameManagerC")).twoPlayers ? 2 : 1)) {
				StartCoroutine("PlayWinAnimation");
			}
		}
	}

	void OnTriggerExit (Collider collider) {
		//Player left the platform
		if (collider.transform.name == "FirstPersonController(Clone)") {
			playersInside--;
		}
	}

	IEnumerator PlayWinAnimation(){
		for (int i = 0; i < 100; i++) {

			winLight.intensity += .05f;
			winLight.range += 1;

			yield return null;
		}

		StartNextLevel ();
	}

	void StartNextLevel () {
//		networkView.RPC ("LoadLevel", RPCMode.AllBuffered, "CityStage");
		Application.LoadLevel("CityStage");
	}

//	[RPC]
//	IEnumerator LoadLevel (string level)
//	{
//		
//		// There is no reason to send any more data over the network on the default channel,
//		// because we are about to load the level, thus all those objects will get deleted anyway
//		Network.SetSendingEnabled(0, false);	
//		
//		// We need to stop receiving because first the level must be loaded first.
//		// Once the level is loaded, rpc's and other state update attached to objects in the level are allowed to fire
//		Network.isMessageQueueRunning = false;
//		
//		// All network views loaded from a level will get a prefix into their NetworkViewID.
//		// This will prevent old updates from clients leaking into a newly created scene.
//		Application.LoadLevel(level);
//		yield return null;
//		yield return null;
//		
//		// Allow receiving data again
//		Network.isMessageQueueRunning = true;
//		// Now the level has been loaded and we can start sending out data to clients
//		Network.SetSendingEnabled(0, true);
//		
//		
////		foreach (GameObject go in GameObject.FindObjectsOfType(GameObject))
////			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);	
//	}

}
