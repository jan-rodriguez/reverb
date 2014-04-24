﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	private bool isOpen;
	
	// Use this for initialization
	void Start () {
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("e")) {
			this.OpenDoor();
		}
		if (Input.GetKeyDown("q")) {
			this.CloseDoor();
		}
	}
	
	public void OpenDoor () {
		if (!isOpen) {
			transform.Translate(0,-100,0);
			isOpen = true;
		}
	}
	
	public void CloseDoor () {
		if (isOpen) {
			transform.Translate(0,100,0);
			isOpen = false;
		}
	}
}
