using UnityEngine;
using System.Collections;

public class RainBuildingTeleport : MonoBehaviour {

	public GameObject teleportee;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.I)) {
			teleportee = GameObject.FindGameObjectWithTag ("PlayerPrefab");
			teleportee.transform.position = this.transform.position;
		}
	}
}
