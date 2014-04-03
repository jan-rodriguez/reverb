using UnityEngine;
using System.Collections;

/**
 * Class used to take input from the microphone
 */

[RequireComponent(typeof(AudioSource))]

public class MicrophoneInput : MonoBehaviour {

	private const float sensitivity = 10;
	private float loudness = 0;

	public void Start() {
		if (networkView.isMine) {
			audio.clip = Microphone.Start (null, true, 10, 44100);
			audio.loop = true;

			gameObject.AddComponent<AudioListener> ();

			while (!(Microphone.GetPosition(Microphone.devices[0].ToString()) > 0)) {
			} // Wait until the recording has started
			audio.Play (); // Play the audio source!
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
			loudness = GetAveragedVolume () * sensitivity;
			this.GetComponent<Light> ().intensity = loudness;
		} else {
			return;
		}
//		Debug.Log (loudness);
	}
	
}
