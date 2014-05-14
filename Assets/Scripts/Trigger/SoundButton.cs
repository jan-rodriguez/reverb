
using UnityEngine;
using System.Collections;

public class SoundButton : Activateable {

	public AudioClip sound;
	public AudioSource source;
	
	// variables to make light
	public Light buttonLight;
	public float activatedLightIntensity = 2f;
	public float deactivatedLightIntensity = 1f;
	public Color activeColor;
	
	private Color inactiveColor;
	
	// Use this for initialization
	void Start () {
		source.clip = sound;
//		inactiveColor = buttonLight.color;
		//activeColor = new Color(0,0.5,1);
	}
	
	public override void OnActivation() {
		if (!source.isPlaying) {
			source.Play ();
		}
		buttonLight.color = activeColor;
	}
	
	
	public override void WhileActivated() {
		
	}
	
}
