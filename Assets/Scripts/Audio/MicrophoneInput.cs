using UnityEngine;
using System;
/**
 * Class used to take input from the microphone
 */

[RequireComponent(typeof(AudioSource))]

public class MicrophoneInput : MonoBehaviour {

	private const float MY_SENSITIVITY = 10;
	private const float OTHER_PLAYER_SENSITIVITY = 500;
	private const float MIN_LIGHT_DECREASE = 1.0f;
	private const float MAX_LIGHT_INTENSITY = 1.0f;
	private const float LOUD_TO_RANGE_RATIO = 20.0f;
	private float loudness = 0;
	private float otherLoudness = 0;

	//Objects needed to be updated
	private GameObject otherPlayerCam;
	private Light otherPlayerLight;
	private Light playerLight;
	
	public void Start() {
		if (networkView.isMine) {
			audio.clip = Microphone.Start (null, true, 10, 44100);
			audio.loop = true;

			this.tag = null;

			//Find the player's game objects
			playerLight = this.GetComponent<Light> ();

			gameObject.AddComponent<AudioListener> ();

			while (!(Microphone.GetPosition(Microphone.devices[0].ToString()) > 0)) {
			} // Wait until the recording has started

			audio.Play (); // Play the audio source!
			audio.mute = true;

		}

	}
	
	private float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		audio.GetOutputData(data,0);


		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}

	public void Update(){
		if (networkView.isMine) {

			SetNewLoudness();

			playerLight.intensity = loudness;
			playerLight.range = loudness * LOUD_TO_RANGE_RATIO;

			//Play the microphone's input over the network
			PlaySound ();
		}

	}

	public float SetNewLoudness() {
		//Get the new computed loudness from the microphone
		float newLoudness = GetAveragedVolume () * MY_SENSITIVITY;
		
		//Assure that the  loudness decreases stedily, and not flickering
		if((loudness - newLoudness) < MIN_LIGHT_DECREASE * Time.deltaTime){
			
			//Assure that the loudness doesn't get too high
			if(newLoudness > MAX_LIGHT_INTENSITY){
				loudness = MAX_LIGHT_INTENSITY;
			}else{
				loudness = newLoudness;
			}
			
		}else{ //Loudness changed too much and want it to decrease slightly
			loudness = (newLoudness - loudness) < 0 ? loudness - MIN_LIGHT_DECREASE * Time.deltaTime : loudness + MIN_LIGHT_DECREASE * Time.deltaTime;
		}

	}

	public void PlaySound() {
		if(networkView.isMine) {
			float[] data = new float[2048];
			audio.GetOutputData(data,0);

			float[] intensity = new float[1];
			intensity[0] = loudness];

			networkView.RPC("PlayNetworkedSound", RPCMode.Others, data);
			networkView.RPC("UpdateOtherPlayerLight", RPCMode.Others, intensity);
		}

	}

	public float GetAverage(float[] data){
		float a = 0;

		foreach (float s in data) {
			a += s;
		}

		return a / data.Length;
	}

	//Play the sound from the other player on your client
	[RPC]
	private void PlayNetworkedSound(float[] soundBite){

		AudioClip audioClip = AudioClip.Create("testSound", soundBite.Length, 1, 44100, true, false);
		audioClip.SetData (soundBite, 0);

		AudioSource.PlayClipAtPoint (audioClip, this.transform.position);

	}

	//Make the sound 
	[RPC]
	private void UpdateOtherPlayerLight(float[] intensityArr){

		float intensity = intensityArr [0];
	
		//Update the other player's light
		otherPlayerLight.intensity = MY_SENSITIVITY * intensity;
		otherPlayerLight.range = intensity * LOUD_TO_RANGE_RATIO;
		
	}

	//Set the other player's camera
	private void SetOtherPlayerCam() {
		if (otherPlayerCam == null) {
			foreach (GameObject playerCam in GameObject.FindGameObjectsWithTag ("Player")) {
				//Found the player camera and can define it
				otherPlayerCam = playerCam;
			}
			otherPlayerLight = otherPlayerCam.GetComponent<Light>();
		}
	}


}
