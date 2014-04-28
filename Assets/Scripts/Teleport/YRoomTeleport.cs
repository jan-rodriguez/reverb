using UnityEngine;
using System.Collections;

public class YRoomTeleport : MonoBehaviour {

	private GameObject teleportee;

	// Use this for initialization
	public void Initialize () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Y)) {
			teleportee = GameObject.FindGameObjectWithTag ("GameController");
			teleportee.transform.position = this.transform.position;
		}
	
	}
}
