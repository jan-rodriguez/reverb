using UnityEngine;
using System.Collections;

public class MicSwap : MonoBehaviour {

	//Enable player lightflickering when entering
	void OnTriggerEnter(Collider col){
		if (col.transform.name == "FirstPersonController(Clone)") {
			MicrophoneInput playerInput = col.GetComponentInChildren<MicrophoneInput>();
			if(playerInput != null){
				playerInput.enabled = true;
			}
		}
	}

	//Disable player light flickering
	void OnTriggerExit(Collider col){
		if (col.transform.name == "FirstPersonController(Clone)") {
			MicrophoneInput playerInput = col.GetComponentInChildren<MicrophoneInput>();
			if(playerInput != null){
				playerInput.enabled = false;
			}

			Light playerLight = col.GetComponentInChildren<Light>();
			playerLight.intensity = 0.75f;
			playerLight.range = 25f;

		}
	}
}
