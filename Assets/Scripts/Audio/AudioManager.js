#pragma strict

final var LIGHT_SENSITIVITY : float = 100.0f; 

function Start () {

}

function Update () {

	var sounds : AudioSource[] = FindObjectsOfType(AudioSource) as AudioSource[];
	
	//Go through all the audio sources and update their lights
	for (var pitch : AudioSource in sounds) {
			
		if(pitch.name != "Player Cam"){
		
			var data : float[];
			var volume : float = 0;
			
			pitch.GetOutputData(data,0);
			
			var objLight : Light = pitch.GetComponent(Light);
			
			//Attach a light if it doesn't already contain one
			if( objLight == null ){
				objLight = pitch.gameObject.AddComponent(Light);
			}
				
			volume = GetAverage(data);
			
			//Update the light's intensity to the volume
			objLight.intensity = volume * LIGHT_SENSITIVITY;
			
		}

	}

}

function GetAverage(data : float[]){
	var a : float  = 0;

	for (var s : float in data) {
		a += s;
	}

	return a / data.Length;
}