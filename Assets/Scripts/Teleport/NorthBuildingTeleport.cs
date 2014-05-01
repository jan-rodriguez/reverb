using UnityEngine;
using System.Collections;

public class NorthBuildingTeleport : MonoBehaviour {

	public GameObject teleportee;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.U)) {
			teleportee = GameObject.FindGameObjectWithTag ("PlayerPrefab");
			teleportee.transform.position = this.transform.position;
		}
	}
}
