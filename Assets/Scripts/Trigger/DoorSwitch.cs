using UnityEngine;
using System.Collections;

public class DoorSwitch : Activateable {

	public Door door;

	public override void OnActivation() {
		if (door.IsOpen) {
			door.Close();
		}
		else if (door.IsClosed) {
			door.Open();
		}
	}

	public override void WhileActivated() {

	}

}
