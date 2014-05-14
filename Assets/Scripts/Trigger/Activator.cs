using UnityEngine;
using System.Collections;

/// <summary>
/// Attach to the player to allow the player to click objects
/// to ativate them.
/// </summary>

public class Activator : MonoBehaviour {

	public Camera referenceCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// FixedUpdate is called once per frame at a fixed interval
	void FixedUpdate () {

		// USE trigger handling
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("e")) {
			Ray ray = referenceCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 2.0f)) {
				Activateable toActivate = hit.transform.GetComponent<Activateable>();
				if (toActivate != null && toActivate.type == Activateable.TRIGGERTYPE.USE) {
					toActivate.Activate();
				}
			}
		}
		// See OnTriggerEnter() and OnTriggerExit() on individual activateables handling CONTACT activateables.
	
	}

}
