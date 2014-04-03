using UnityEngine;
using System;
/**
 * Class used to take input from the microphone
 */

[RequireComponent(typeof(AudioSource))]

public class MicrophoneInput : MonoBehaviour {

	private const float sensitivity = 10;
	private float loudness = 0;

	private bool playingSound = false;

	public void Start() {
		if (networkView.isMine) {
			audio.clip = Microphone.Start (null, true, 10, 44100);
			audio.loop = true;

			gameObject.AddComponent<AudioListener> ();

			while (!(Microphone.GetPosition(Microphone.devices[0].ToString()) > 0)) {
			} // Wait until the recording has started

//			audio.Play (); // Play the audio source!
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
			PlaySound ();
		}
//		Debug.Log (loudness);

	}

	[RPC]
	public void PlaySound() {
		if(networkView.isMine) {
			float[] data = new float[256];
			float a = 0;
			audio.GetOutputData(data,0);

			networkView.RPC("PlayNetworkedSound", RPCMode.Others, data);
		}

	}

	private void PlayNetworkedSound(byte[] soundBite){

		float[] data = ConvertByteToFloat (soundBite);

		audio.clip.SetData (data, 0);

	}

	private static byte[] ConvertByteToFloat(float[] floatArray1) 
	{
		// create a byte array and copy the floats into it...
		byte[] byteArray = new byte[floatArray1.Length * 4];
		Buffer.BlockCopy(floatArray1, 0, byteArray, 0, byteArray.Length);

		return byteArray;
	}

	private static float[] ConvertFloatToByte(byte[] byteArray){
		// create a second float array and copy the bytes into it...
		float[] floatArray2 = new float[byteArray.Length / 4];
		Buffer.BlockCopy(byteArray, 0, floatArray2, 0, byteArray.Length);

		return floatArray2;
	}




}
