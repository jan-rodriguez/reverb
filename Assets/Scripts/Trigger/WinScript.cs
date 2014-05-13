using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	public Light winLight;
	public int playersInside;

	void Start() {
	}

	void OnTriggerEnter (Collider collider) {

		//Player walked into the platform
		if (collider.transform.name == "FirstPersonController(Clone)") {
			playersInside ++;
			//If both players are in the platform activate win animations
			if ( playersInside == 1 ) {
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

}
