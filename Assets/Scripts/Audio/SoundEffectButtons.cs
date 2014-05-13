using UnityEngine;
using System.Collections;

public class SoundEffectButtons : Activateable {

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// When a sound effect button is pressed, it should play 
	// a sound clip specified by the audio source.
	public override void OnActivation() {
		Debug.Log ("Activating sound button");
		audioSource.Play ();
		
	}
	

}
