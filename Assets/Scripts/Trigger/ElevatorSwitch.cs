using UnityEngine;
using System.Collections;

public class ElevatorSwitch : Activateable {

	private ArrayList colliders = new ArrayList();

	public Light elevatorSwitchLight;
	public float inactiveGlowIntensity = 0.5f;
	public float activeGlowIntensity = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider collider) {
		if (colliders.Count == 0) { // If this is the first collider to enter, activate
			activated = true;
			OnActivation ();
		}
		colliders.Add (collider);
	}

	void OnTriggerExit (Collider collider) {
		if (colliders.Count == 1) { // If this is the last collider, it's leaving, so deactivate
			activated = false;
			OnDeactivation ();
		}
		colliders.Remove (collider);
	}

	public override void OnActivation() {
		if (elevatorSwitchLight != null) {
			elevatorSwitchLight.intensity = activeGlowIntensity;
		}
	}

	public override void OnDeactivation() {
		if (elevatorSwitchLight != null) {
			elevatorSwitchLight.intensity = inactiveGlowIntensity;
		}
	}
}
