using UnityEngine;
using System.Collections;

public class DoorSwitch : Activateable {

	public Door door;

	// variables to make light
	public Light doorSwitchLight;
	public float activatedLightIntensity = 2f;
	public float deactivatedLightIntensity = 1f;

	public override void OnActivation() {
		if (door.IsOpen) {
			door.Close();
			// switch to deactivated lighting
			if (doorSwitchLight != null) {
				doorSwitchLight.intensity = deactivatedLightIntensity;
			}
		}
		else if (door.IsClosed) {
			door.Open();
			// switch to activated lighting
			if (doorSwitchLight != null) {
				doorSwitchLight.intensity = activatedLightIntensity;
			}
		}
	}

	public override void WhileActivated() {

	}

}
