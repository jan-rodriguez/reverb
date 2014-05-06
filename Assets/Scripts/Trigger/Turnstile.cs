using UnityEngine;
using System.Collections;

public class Turnstile : MonoBehaviour {

	// Used to make the turnstile glow as it rotates
	public Light turnstileMovementLight;
	public float turnstileMovementLightIntensityFactor = 6f;

	// Whether or not the turnstile is moving.
	//private bool isMoving = false;

	// Used as a part of smoothly rotating the turnstile.
	private float moveStartTime;
	public float timeToRotate = 1f;

	// Used to keep track of the turnstile's direction
	private Vector3 angle;

	// 90 degrees in 1 turn
	private float degreesToRotate = 90.0f;

	void Start() {
		angle = transform.eulerAngles;
	}

	public void FixedUpdate() {

		// Smoothly rotate the turnstile
		//if (transform.eulerAngles.y < angle.y) {
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, this.angle, Time.deltaTime * 2.0f);
		//float t = (Time.time - moveStartTime) / timeToRotate;
		//transform.eulerAngles = new Vector3(angle.x, Mathf.SmoothStep(transform.eulerAngles.y, angle.y, Time.deltaTime*t), angle.z);
		//}

		/*
		float turnstileYPositionAbsoluteDelta = Mathf.Abs (transform.position.y - turnstileOldPosition.y);
		// Set the intensity according to that speed and a multiplier
		turnstileMovementLight.intensity = turnstileMovementLightIntensityFactor * turnstileYPositionAbsoluteDelta;
		if (transform.eulerAngles.Equals(finishAngle)) {
			isMoving = false;
		}
		*/

		if (transform.eulerAngles.y > this.angle.y) {
			angle = transform.eulerAngles;
		}
	}

	public void Rotate() {
		moveStartTime = Time.time;
		angle.Set(angle.x, angle.y + degreesToRotate, angle.z);
		/*if (angle.y > 360) {
			transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y - 360, transform.eulerAngles.z);
			this.angle.Set(angle.x, angle.y - 360, angle.z);
		}*/
	}
}
