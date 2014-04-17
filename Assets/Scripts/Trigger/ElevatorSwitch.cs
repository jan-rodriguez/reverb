using UnityEngine;
using System.Collections;

public class ElevatorSwitch : Activateable {

	private ArrayList colliders = new ArrayList();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider collider) {
		if (colliders.Count == 0) { // If this is the first collider to enter, activate
			activated = true;
		}
		colliders.Add (collider);
	}

	void OnTriggerExit (Collider collider) {
		if (colliders.Count == 1) { // If this is the last collider, it's leaving, so deactivate
			activated = false;
		}
		colliders.Remove (collider);
	}
}
