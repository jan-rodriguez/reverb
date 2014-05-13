using UnityEngine;
using System.Collections;

public class Turnstile : MonoBehaviour {

	// Used to make the turnstile glow as it rotates
	public Light turnstileMovementLight;
	public float turnstileMovementLightIntensityFactor = 6f;

	// The time it takes the turnstile to rotate in seconds.
	public float timeToRotate = 2f;

	// Whether or not the turnstile is moving.
	private bool isMoving = false;

	// Used as a part of smoothly rotating the turnstile.
	private float moveStartTime;

	// Used to keep track of the turnstile's direction
	private Vector3 startAngle;
	private Vector3 finishAngle;

	// 90 degrees in 1 turn
	private float degreesToRotate = 90.0f;

	void Start() {
	}

	public void FixedUpdate() {

		// Smoothly rotate the turnstile
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, finishAngle, Time.deltaTime * 2.0f);

		/*
		float turnstileYPositionAbsoluteDelta = Mathf.Abs (transform.position.y - turnstileOldPosition.y);
		// Set the intensity according to that speed and a multiplier
		turnstileMovementLight.intensity = turnstileMovementLightIntensityFactor * turnstileYPositionAbsoluteDelta;
		*/

		if (transform.eulerAngles.Equals(finishAngle)) {
			isMoving = false;
		}

	}

	public void Open() {
		moveStartTime = Time.time;
		startAngle = transform.eulerAngles;
		finishAngle = new Vector3 (startAngle.x, startAngle.y + degreesToRotate, startAngle.z);
	}

	public void Close() {
		Open ();
	}
}
