using UnityEngine;
using System.Collections;

public class Activateable : MonoBehaviour {

	public enum TRIGGERTYPE {
		USE, // click object to activate
		CONTACT, // touch or enter object to activate
		DOUBLE_CONTACT, // requires both players contacting, TODO: NOT YET IMPLEMENTED
		NONE // e.g. triggered by other scripts instead of anything the players do directly
	}

	public TRIGGERTYPE type = TRIGGERTYPE.USE;

	public bool activated = false;

	public float activationCooldown = 1f; // in seconds
	private float activationCooldownTimer = 0f;	

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (activationCooldownTimer > 0f) {
			activationCooldownTimer -= Time.deltaTime;
		}
		if (activationCooldownTimer < 0f) {
			activationCooldownTimer = 0f;
		}

		if (activated) {
			WhileActivated();
		}

	}

	/// <summary>
	/// Attempts to activate this instance. Returns true if activation was successful, false otherwise.
	/// </summary>
	public bool Activate() {

		if (activationCooldownTimer <= 0f) {
			// Activate!
			activated = true;
			Debug.Log ("Object activated.");
			activationCooldownTimer = activationCooldown;
			OnActivation();
			return true;
		}
		// Activation failed
		return false;

	}

	/// <summary>
	/// Deactivates the activateable.
	/// </summary>
	public void Deactivate() {
		activated = false;
		OnDeactivation ();
	}

	/// <summary>
	/// Subclasses can override this to implement specialized
	/// code to run on activation.
	/// </summary>
	public virtual void OnActivation() {

	}

	/// <summary>
	/// Subclasses can override this to implement specialized
	/// code to run on deactivation.
	/// </summary>
	public virtual void OnDeactivation() {

	}

	/// <summary>
	/// Sublcasses can override this to repeatedly run a block
	/// of code while the object is activated.
	/// </summary>
	public virtual void WhileActivated() {

	}

}
