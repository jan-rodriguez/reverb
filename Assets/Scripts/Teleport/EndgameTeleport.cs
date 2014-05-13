using UnityEngine;
using System.Collections;

public class EndgameTeleport : MonoBehaviour {

    private GameObject teleportee;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.O)) {
            teleportee = GameObject.FindGameObjectWithTag("PlayerPrefab");
            teleportee.transform.position = this.transform.position;
        }
	
	}
}
