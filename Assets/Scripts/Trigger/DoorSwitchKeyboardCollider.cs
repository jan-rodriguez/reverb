using UnityEngine;
using System.Collections;

public class DoorSwitchKeyboardCollider : MonoBehaviour {

	// Tells whether the player is in the collider and therefore able to activate the switch.
	public bool enter;

	// Use this for initialization
	void Start () {
		enter = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void onTriggerEnter (Collider collider) {
		if (collider.transform.tag == "PlayerPrefab") {
			enter = true;
			print (enter);
		}
	}

	void onTriggerExit (Collider collider) {
		if (collider.transform.tag == "PlayerPrefab" && collider.networkView.isMine) {
			enter = false;
			print (enter);
		}
	}
}
