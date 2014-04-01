using UnityEngine;
using System.Collections;

/**
 * Class used to take input from the microphone
 */

[RequireComponent(typeof(AudioSource))]

public class MicrophoneInput : MonoBehaviour {

	public float sensitivity = 100;
	public float loudness = 0;

	public void Start() {
		audio.clip = Microphone.Start(null, true, 10, 44100);
		audio.loop = true;

		while (!(Microphone.GetPosition(Microphone.devices[0].ToString()) > 0)){} // Wait until the recording has started
		audio.Play(); // Play the audio source!

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
		loudness = GetAveragedVolume() * sensitivity;
		Debug.Log (loudness);
	}
	
}
