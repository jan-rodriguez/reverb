using UnityEngine;
using System.Collections;

public class Turnstile : MonoBehaviour {
	
	// Used to make the turnstile glow as it spins
	public Light turnstileMovementLight;
	public float turnstileMovementLightIntensityFactor = 6f;

	//private Vector3 closePosition;

	//public Vector3 openDeltaPosition = new Vector3(0f, -3f, 0f);
	
	// The time it takes the turnstile to rotate in seconds.
	public float timeToOpenOrClose = 2f;

	//private bool isOpen = false;
	
	// Used as a part of smoothly rotating the turnstile
	private float moveStartTime;
	
	// Used to keep track of the turnstile's velocity
	private Vector3 turnstileOldPosition;
	
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
		
		// Set old turnstile position because we're about to change it
		turnstileOldPosition = transform.position;
		
		// Smoothly move the turnstile between open and closed or vice-versa
		transform.position = new Vector3(
			Mathf.SmoothStep (transform.position.x, desiredPosition.x, t),
			Mathf.SmoothStep (transform.position.y, desiredPosition.y, t),
			Mathf.SmoothStep (transform.position.z, desiredPosition.z, t)
			);
		
		// Get the speed the turnstile has moved in the y direction
		float turnstileYPositionAbsoluteDelta = Mathf.Abs (transform.position.y - turnstileOldPosition.y);
		
		// Set the intensity according to that speed and a multiplier
		turnstileMovementLight.intensity = turnstileMovementLightIntensityFactor * turnstileYPositionAbsoluteDelta;
		
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
