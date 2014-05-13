using UnityEngine;
using System.Collections;

public class Turnstile : MonoBehaviour {

	// Used to make the turnstile glow as it rotates
	public Light turnstileMovementLight;
	public float turnstileMovementLightIntensityFactor = 6f;

	// Whether or not the turnstile is moving.
	//private bool isMoving = false;
	private float movement = 0f;

	// Used as a part of smoothly rotating the turnstile.
	private float moveStartTime;
	public float timeToRotate = 1f;

	// Used to keep track of the turnstile's direction
	private Vector3 desiredAngle;

	// 90 degrees in 1 turn
	private float degreesToRotate = 90.0f;

	void Start() {
		desiredAngle = transform.eulerAngles;
	}

	public void FixedUpdate() {
		float oldY = this.desiredAngle.y;

		// Smoothly rotate the turnstile
		//if (transform.eulerAngles.y < angle.y) {
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, this.desiredAngle, Time.deltaTime * 2.0f);
		//float t = (Time.time - moveStartTime) / timeToRotate;
		//transform.eulerAngles = new Vector3(angle.x, Mathf.SmoothStep(transform.eulerAngles.y, angle.y, Time.deltaTime*t), angle.z);
		//}

		float newY = this.desiredAngle.y;
		movement = Mathf.Abs(newY - oldY);

		/*
		float turnstileYPositionAbsoluteDelta = Mathf.Abs (transform.position.y - turnstileOldPosition.y);
		// Set the intensity according to that speed and a multiplier
		turnstileMovementLight.intensity = turnstileMovementLightIntensityFactor * turnstileYPositionAbsoluteDelta;
		if (transform.eulerAngles.Equals(finishAngle)) {
			isMoving = false;
		}
		*/

		print ("Desired Angle == " + this.desiredAngle.y + "\nTurnstile Angle == " + transform.eulerAngles.y);
	}

	public void Rotate() {
		if (movement < 0.01) {
			moveStartTime = Time.time;
			float newY = desiredAngle.y + degreesToRotate;
			if (newY > 360) {
				newY -= 360;
				//transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y - 360, transform.eulerAngles.z);
			}
			desiredAngle.Set(desiredAngle.x, newY, desiredAngle.z);
		/*
		 * if (this.desiredAngle.y > 360) {
			//transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y - 360, transform.eulerAngles.z);
			this.desiredAngle.Set(this.desiredAngle.x, this.desiredAngle.y - 360, this.desiredAngle.z);
		}
		*/
		}
	}
}
