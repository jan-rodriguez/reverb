using UnityEngine;
using System;
/**
 * Class used to take input from the microphone
 */

[RequireComponent(typeof(AudioSource))]

public class MicrophoneInput : MonoBehaviour {

	private const float MY_SENSITIVITY = 10;
	private const float OTHER_PLAYER_SENSITIVITY = 500;
	private float loudness = 0;

	private GameObject otherPlayerCam;
	
	public void Start() {
		if (networkView.isMine) {
			audio.clip = Microphone.Start (null, true, 10, 44100);
			audio.loop = true;

			this.tag = null;

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
			loudness = GetAveragedVolume () * MY_SENSITIVITY;
			this.GetComponent<Light> ().intensity = loudness;
			PlaySound ();
		}

	}

	public void PlaySound() {
		if(networkView.isMine) {
			float[] data = new float[2048];
			audio.GetOutputData(data,0);

			networkView.RPC("PlayNetworkedSound", RPCMode.Others, data);
		}

	}

	public float GetAverage(float[] data){
		float a = 0;

		foreach (float s in data) {
			a += s;
		}

		return a / data.Length;
	}

	[RPC]
	private void PlayNetworkedSound(float[] soundBite){

		AudioClip audioClip = AudioClip.Create("testSound", soundBite.Length, 1, 44100, true, false);
		audioClip.SetData (soundBite, 0);

		AudioSource.PlayClipAtPoint (audioClip, this.transform.position);

		UpdateOtherPlayerLight (soundBite);

	}


	private void UpdateOtherPlayerLight(float[] sound){
		//If the other player's camera hasn't been defined
		if (otherPlayerCam != null) {
			//Update the other player's light based on sound
			otherPlayerCam.GetComponent<Light>().intensity = OTHER_PLAYER_SENSITIVITY * GetAverage(sound);
		}else{ //Define other player's cam
			foreach( GameObject playerCam in GameObject.FindGameObjectsWithTag ("Player")){
				//Found the player camera and can define it
				otherPlayerCam = playerCam;
				playerCam.GetComponent<Light>().intensity = OTHER_PLAYER_SENSITIVITY * GetAverage(sound);
			}
		
		}

		
	}

}
