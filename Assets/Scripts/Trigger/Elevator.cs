using UnityEngine;
using System.Collections;

public class Elevator : Activateable {

	// All elevators require two switches active to operate
	public ElevatorSwitch switch1;
	public ElevatorSwitch switch2;

	// Position the elevator should be when it is off - doesn't need to be set,
	// because the code in Start() set it to the object's editor position.
	private Vector3 offPosition;
	
	// Position the elevator should be when it is on, relative to off.
	public Vector3 onDeltaPosition = new Vector3(0f, 10f, 0f);
	
	// The time it takes the elevator to move its full range of motion in seconds.
	public float timeToMoveInTotal;
	
	// Used as a part of smoothly transitioning the elevator's position.
	private float moveStartTime;
	
	void Start() {
		// Set off position to the current position in the scene at the start
		offPosition = transform.position;
	}
	
	public void FixedUpdate() {

		// Activation conditions
		if (switch1.activated && switch2.activated && !activated) {
			Activate ();
		}

		// Deactivation conditions
		if ((!switch1.activated || !switch2.activated) && activated) {
			Deactivate ();
		}
		
		// Our desired position is either closed position or closed position + open delta position
		Vector3 desiredPosition = activated? offPosition + onDeltaPosition : offPosition;
		
		// t represents the current ratio (between 0 and 1) between how far we are in time opening
		// or closing
		//float t = Mathf.Min(0,(Time.time - moveStartTime) / timeToMoveInTotal);
		float t = (Time.time - moveStartTime) / timeToMoveInTotal;

		if (t >= 0) {
			// Smoothly move the door between open and closed or vice-versa
			transform.position = new Vector3(
				Mathf.SmoothStep (transform.position.x, desiredPosition.x, t),
				Mathf.SmoothStep (transform.position.y, desiredPosition.y, t),
				Mathf.SmoothStep (transform.position.z, desiredPosition.z, t)
				);
		}
	}
	
	public override void OnActivation() {
		moveStartTime = Time.time;
	}
	
	public override void OnDeactivation() {
		moveStartTime = Time.time + 1; // Wait a second before going back down.
	}



}
