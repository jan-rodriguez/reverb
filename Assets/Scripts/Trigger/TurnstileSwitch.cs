using UnityEngine;
using System.Collections;

public class TurnstileSwitch : Activateable {
	
	public Turnstile turnstile;
	
	// variables to make light
	public Light turnstileSwitchLight;
	public float activatedLightIntensity = 2f;
	public float deactivatedLightIntensity = 1f;
	public Color activeColor;
	
	private Color inactiveColor;
	
	// Use this for initialization
	void Start () {
		inactiveColor = turnstileSwitchLight.color;
		//activeColor = new Color(0,0.5,1);
	}
	
	public override void OnActivation() {
		networkView.RPC ("ActivateTurnstile", RPCMode.All);
	}
	
	[RPC]
	public void ActivateTurnstile(){
			turnstile.Open();
	}
	
	public override void WhileActivated() {
		
	}
	
}
