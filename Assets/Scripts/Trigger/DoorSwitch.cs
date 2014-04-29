using UnityEngine;
using System.Collections;

public class DoorSwitch : Activateable {

	public Door door;

	// variables to make light
	public Light doorSwitchLight;
	public float activatedLightIntensity = 2f;
	public float deactivatedLightIntensity = 1f;
	public Color activeColor;
	
	private Color inactiveColor;

	// Use this for initialization
	void Start () {
		inactiveColor = doorSwitchLight.color;
		//activeColor = new Color(0,0.5,1);
	}

	public override void OnActivation() {
		networkView.RPC ("ActivateDoor", RPCMode.All);

	}

	[RPC]
	public void ActivateDoor(){
		if (door.IsOpen) {
			door.Close();
			// switch to deactivated lighting
			if (doorSwitchLight != null) {
				doorSwitchLight.intensity = deactivatedLightIntensity;
				doorSwitchLight.color = inactiveColor;
			}
		}
		else if (door.IsClosed) {
			door.Open();
			// switch to activated lighting
			if (doorSwitchLight != null) {
				doorSwitchLight.intensity = activatedLightIntensity;
				doorSwitchLight.color = activeColor;
			}
		}
	}

	public override void WhileActivated() {

	}

}
