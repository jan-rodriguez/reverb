using UnityEngine;
using System;
/**
 * Class used to take input from the microphone
 */

[RequireComponent(typeof(AudioSource))]

public class MicrophoneInput : MonoBehaviour {

	private const float MY_SENSITIVITY = 25f;
	private const float MIN_LIGHT_DECREASE = 1.0f;
	private const float MAX_LIGHT_INTENSITY = 2.0f;
	private const float LOUD_TO_RANGE_RATIO = 20.0f;
	private float loudness = 0;

	//Objects needed to be updated
	private Light playerLight;
	
	public void Awake() {

		//Player doesn't have mic destroy game object
		if (Microphone.devices.Length == 0) {
			GameObject.Destroy(gameObject);
		}
		audio.clip = Microphone.Start (null, true, 10, 44100);
		audio.loop = true;

		//Find the player's game objects
		playerLight = this.GetComponent<Light> ();

		while (!(Microphone.GetPosition(Microphone.devices[0].ToString()) > 0)) {
		} // Wait until the recording has started

		audio.Play (); // Play the audio source!
		audio.mute = true;

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

		SetNewLoudness();

		playerLight.intensity = loudness;
		playerLight.range = loudness * LOUD_TO_RANGE_RATIO;

	}

	public void SetNewLoudness() {
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

	public float GetAverage(float[] data){
		float a = 0;

		foreach (float s in data) {
			a += s;
		}

		return a / data.Length;
	}

}
