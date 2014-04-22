using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	// Used to make the door glow as it opens or closes
	public Light doorMovementLight;
	public float doorMovementLightIntensityFactor = 6f;

	// Position the door should be when it is closed - doesn't need to be set,
	// because the code in Start() set it to the object's editor position.
	private Vector3 closePosition;

	// Position the door should be when it is open, relative to closed.
	public Vector3 openDeltaPosition = new Vector3(0f, -3f, 0f);

	// The time it takes the door to open or close in seconds.
	public float timeToOpenOrClose = 2f;

	// Whether or not the door is open.
	private bool isOpen = false;

	// Used as a part of smoothly transitioning the door from open to closed.
	private float moveStartTime;

	// Used to keep track of the door's velocity
	private Vector3 doorOldPosition;

	void Start() {
		// Set closed position to the current position in the scene at the start
		closePosition = transform.position;
	}

	public void FixedUpdate() {

		// Our desired position is either closed position or closed position + open delta position
		Vector3 desiredPosition = isOpen? closePosition + openDeltaPosition : closePosition;

		// t represents the current ratio (between 0 and 1) between how far we are in time opening
		// or closing
		float t = (Time.time - moveStartTime) / timeToOpenOrClose;

		// Set old door position because we're about to change it
		doorOldPosition = transform.position;

		// Smoothly move the door between open and closed or vice-versa
		transform.position = new Vector3(
			Mathf.SmoothStep (transform.position.x, desiredPosition.x, t),
			Mathf.SmoothStep (transform.position.y, desiredPosition.y, t),
			Mathf.SmoothStep (transform.position.z, desiredPosition.z, t)
			);

		// Get the speed the door has moved in the y direction
		float doorYPositionAbsoluteDelta = Mathf.Abs (transform.position.y - doorOldPosition.y);

		// Set the intensity according to that speed and a multiplier
		doorMovementLight.intensity = doorMovementLightIntensityFactor * doorYPositionAbsoluteDelta;

	}

	public void Open() {
		isOpen = true;
		moveStartTime = Time.time;
	}

	public void Close() {
		isOpen = false;
		moveStartTime = Time.time;
	}

	public bool IsOpen {
		get {
			return isOpen;
		}
	}

	public bool IsClosed {
		get {
			return !isOpen;
		}
	}

}
